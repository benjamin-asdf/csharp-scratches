using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

public static class Programm {

    public class NoFixAvaivableException : Exception {
        public NoFixAvaivableException(string message) : base(message) {
        }
    }

    const string headerPart = @"
%YAML 1.1
%TAG !u! tag:unity3d.com,2011:
";

    const string deleteFlag = "#flagdelete#";

    public static void Main(string[] args) {
        Console.WriteLine("==== ParseGameObj ====\n");


        var inputLines = new List<string>();
        foreach (var line in File.ReadAllLines("file")) {
            inputLines.Add(line);
        }

        var gos = ParsePrefab(inputLines);
        var allGoIds = new HashSet<string>();
        var allFileIds = new HashSet<string>();

        foreach (var go in gos) {
            allGoIds.Add(go.fileId);
            allFileIds.Add(go.fileId);
            foreach (var compFileId in go.compFileIds) {
                allFileIds.Add(compFileId.id);
            }
        }

        Console.WriteLine($"go count: {gos.Count}");

        // assign new file ids to broken go and comp ids

        var compFileIdsInGo = new List<string>();
        var newFileIdsInGo = new List<string>();
        foreach (var go in gos) {
            if (String.IsNullOrEmpty(go.fileId)) {
                // replace line with a new file id of our own.
                var newFileId = newUniqueFileId();
                allGoIds.Add(newFileId);
                allFileIds.Add(newFileId);
                InsertFileId(go.lines,0,newFileId);
            }


            // newFileIdsInGo.Clear();

            compFileIdsInGo.Clear();
            var compIdsDirty = false;
            foreach (var compFileId in go.compFileIds) {

                if (String.IsNullOrEmpty(compFileId.id)) {
                    compIdsDirty = true;
                    var newFileId = newUniqueFileId();
                    var idx = go.lines.IndexOf(compFileId.origLine);
                    InsertFileId(go.lines, idx, newFileId);
                    allFileIds.Add(newFileId);
                    // newFileIdsInGo.Add(newFileId);
                    compFileIdsInGo.Add(newFileId);
                } else {
                    compFileIdsInGo.Add(compFileId.id);
                }

            }

            foreach (var prefabTransformRef in go.prefabTransformRefs) {
                if (String.IsNullOrEmpty(prefabTransformRef.item) || !allFileIds.Contains(prefabTransformRef.item)) {
                    // NOTE no guide yet because I don't know how often that happens
                    throw new NoFixAvaivableException($"{go.name} has a broken prefab transform ref.\nYou can try grep for\n{prefabTransformRef.origLine}\nIn the prefab, then find out which prefab it is and to which transform it should belong. Can poke Ben if you are stuck.");
                }
            }


            foreach (var compRef in go.compRefs) {
                if (String.IsNullOrEmpty(compRef.item) || !compFileIdsInGo.Contains(compRef.item)) {
                    compIdsDirty = true;
                }
            }

            if (compIdsDirty) {
                // do the component fix part.
                go.ReplaceCompRefs(compFileIdsInGo);
            }

        }



        var res = headerPart;
        foreach (var go in gos) {
            foreach (var line in go.lines) {
                res += $"{line}\n";
            }
        }

        Console.WriteLine(res);






        // actually, the moment we change something, we want to renew all the comp refs on the gameobject


        // any dangling prefab transfrom refs -> err out

        // we could try check wich transforms have m_father with this transfrom,
        // but for now we don't do that
        // any dangling transform child refs





        string newUniqueFileId() {
            var fileIdNum = -1337;
            var x = 0;
            while (allFileIds.Contains(fileIdNum.ToString()) && x++ < 10000) {
                fileIdNum++;
            }
            return fileIdNum.ToString();
        }






    }

    // static HashSet<string>  ();

    const string nullKey = "null";
    static int nullGoIdx = 0;
    static string NextGoNullAddr() => NullAddr(nullGoIdx++);
    static string NullAddr(int idx) => $"{nullKey}|{idx}";

    static Dictionary<string,ParsedGo> gameObjects = new Dictionary<string,ParsedGo>();

    class ParsedGo {
        public readonly string fileId;
        public readonly string name;
        public List<(string origLine, string id)> compFileIds = new List<(string origLine, string id)>();
        public List<(string origLine, string item)> compRefs = new List<(string origLine, string item)>();
        public List<(string origLine, string item)> transformChildRefs = new List<(string origLine, string item)>();
        public List<(string origLine, string item)> prefabTransformRefs = new List<(string origLine, string item)>();
        public List<(string origLine, string item)> compGoRefs = new List<(string origLine, string item)>();
        public readonly int compRefPartStart;
        public readonly int originalCompRefCount;

        public List<string> lines = new List<string>();

        public ParsedGo(List<string> inputLines) {
            var x = 0;

            foreach (var line in inputLines) {
                lines.Add(line);
                if (line.StartsWith("--- !u!1 &")) {
                    fileId = line.Substring(line.IndexOf("&") + 1);
                } else if (goNameRegex.Match(line).Success) {
                    name = goNameRegex.Match(line).Groups[1].ToString();
                } else if (compBeginningRegex.Match(line).Success) {
                    compFileIds.Add((line,fileIdForLine(line)));

                } else if (compRefRegex.Match(line).Success) {
                    if (compRefPartStart == 0) {
                        compRefPartStart = x;
                    }
                    compRefs.Add((line,compRefRegex.Match(line).Groups[1].ToString()));
                }
                else if (transformChildRegex.Match(line).Success) {
                    transformChildRefs.Add((line,transformChildRegex.Match(line).Groups[1].ToString()));

                } else if (prefabTransformRegex.Match(line).Success) {
                    prefabTransformRefs.Add((line,prefabTransformRegex.Match(line).Groups[1].ToString()));

                }
                x++;
            }
            originalCompRefCount = compRefs.Count;
        }



        public void ReplaceCompRefs(List<string> newRefs) {
            var newComps = new List<string>();
            lines.RemoveRange(compRefPartStart,originalCompRefCount);
            foreach (var elem in newRefs) {
                // newComps.Add($"- component: {{fileID: {elem}}}");
                lines.Insert(compRefPartStart,$"- component: {{fileID: {elem}}}");
            }
        }

    }

    // static Regex fileIdRegex =

    static Regex compRefRegex = new Regex(@"  - component: {fileID: (-?\d+)?}");
    static Regex gameObjRefRegex = new Regex(@"  m_GameObject: {fileID: (-?\d+)?}");
    static Regex compBeginningRegex = new Regex(@"--- !u!\d+ &");
    static Regex transformChildRegex = new Regex(@"  - {fileID: (-?\d+)?}");
    static Regex prefabTransformRegex = new Regex(@"    m_TransformParent: {fileID: (-?\d+)?}");
    static Regex goNameRegex = new Regex(@"  m_Name: (\w+)");

    static List<ParsedGo> ParsePrefab(List<string> lines) {
        List<ParsedGo> gos = new List<ParsedGo>();
        List<string> goLines = new List<string>();

        var insideHeader = true;
        foreach (var line in lines) {
            if (line.StartsWith("--- !u!1 &")) {
                if (!insideHeader) {
                    gos.Add(new ParsedGo(goLines));
                }
                insideHeader = false;
                goLines.Clear();}
            goLines.Add(line);
        }

        gos.Add(new ParsedGo(goLines));
        return gos;
    }

    static string fileIdForLine(string line) {
        return line.Substring(line.IndexOf("&") + 1);
    }

    static void InsertFileId(List<string> lines, int index, string fileId) {
        var line = lines[index];
        if (line.IndexOf("&") < 0) {
            throw new Exception($"Expected an & in line {index}");
        }
        ReplaceLine(lines,index,line.Insert(line.IndexOf("&") + 1,fileId));
    }

    static void ReplaceLine(List<string> lines, int index, string newStr) {
        lines.RemoveAt(index);
        lines.Insert(index,newStr);
    }

    static void FlagRemoveLine(List<string> lines, int index) {
        lines.RemoveAt(index);
        lines.Insert(index,deleteFlag);
    }

    static string GetModifiedContent(List<string> lines) {
        var res = "";
        foreach (var line in lines) {
            if (line != deleteFlag) {
                res += $"{line}\n";
            }
        }
        return res;
    }

    // var lines = new List<string>();
    // foreach (var line in File.ReadAllLines("file")) {
    //     lines.Add(line);
    // }

    // var content = new DynamicPrefabContent(lines);

    // class PrefabLines {
    //     readonly int indexOffset = 0;
    //     readonly List<string> lines;
    //     readonly List<int> linesForDeletion = new List<int>();
    //     readonly List<(int,string)> modifications = new List<(int,string)>();

    //     public PrefabLines(List<string> lines) {
    //         this.lines = lines;
    //     }

    //     public void RemoveAt(int index) {

    //     }

    //     public string GetModified() {

    //         // first apply line by line modifications,
    //         //



    //     }


    // }












































































    class ParsedComp {
        ///the file id for this comp
        public string fileId;
        public int idLine;
        ///the go this comp is attached to
        public string goFileId;
        // currComp = new ParsedComp();
        // currComp.fileId = fileIdForLine(line);
        // currComp.idLine = x;
    }

}

// var input = "lul 1";
// var pattern = @"lul (\d)?";

// Console.WriteLine(Regex.Match(input,pattern).Groups.Count > 0);
// foreach (var g in Regex.Match(input,pattern).Groups) {
//     Console.WriteLine(g);
// }


// Console.WriteLine(Regex.Matches(input,pattern).First().Success);
// Console.WriteLine(Regex.Matches(input,pattern).Count > 0);

// string insertTag(string insertion) => $"~Insert:{insertion}:";
// var input = "best\n lul\n hi\n";
// input = input.Replace(" lul", " lul" + insertTag("component 1"));
// input = input.Replace(" lul", " lul" + insertTag("component 2"));


// input = Regex.Replace(input,@"(.*)~Insert:(.*):(.*)","$1,$2,$3");
// Console.WriteLine(input);
// return;
