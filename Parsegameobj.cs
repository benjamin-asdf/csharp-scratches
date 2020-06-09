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

    const string deleteFlag = "#flagdelete#";

    public static void Main(string[] args) {
        Console.WriteLine("==== ParseGameObj ====\n");

        var lines = new List<string>();
        foreach (var line in File.ReadAllLines("file")) {
            lines.Add(line);
        }

        var gos = ParsePrefab(lines);

        var allGoIds = new HashSet<string>();
        var allFileIds = new HashSet<string>();
        var addedFileIds = new HashSet<string>();

        foreach (var go in gos) {
            allGoIds.Add(go.fileId);
            allFileIds.Add(go.fileId);
            foreach (var compFileId in go.compFileIds) {
                allFileIds.Add(compFileId.id);
            }
        }

        // assign new file ids to broken go and comp ids

        var compFileIdsInGo = new HashSet<string>();
        var newFileIdsInGo = new List<string>();
        foreach (var go in gos) {
            Console.WriteLine($"go: {go.fileId}");
            if (String.IsNullOrEmpty(go.fileId)) {
                // replace line with a new file id of our own.
                var newFileId = newUniqueFileId();
                addFileIdToLookups(newFileId);
                InsertFileId(lines,go.idLine,newFileId);
            }

            compFileIdsInGo.Clear();
            newFileIdsInGo.Clear();
            foreach (var compFileId in go.compFileIds) {
                compFileIdsInGo.Add(compFileId.id);
                if (String.IsNullOrEmpty(compFileId.id)) {
                    var newFileId = newUniqueFileId();
                    addFileIdToLookups(newFileId);
                    InsertFileId(lines,compFileId.globalLine,newFileId);
                    newFileIdsInGo.Add(newFileId);
                }
            }

            var brokenCompRefs = new List<(int line,string id)>();
            foreach (var compRef in go.compRefs) {
                if (String.IsNullOrEmpty(compRef.id) || !compFileIdsInGo.Contains(compRef.id)) {
                    brokenCompRefs.Add(compRef);
                }
            }

            // 0 == 0 most of the time
            if (brokenCompRefs.Count == newFileIdsInGo.Count) {
                var idx = 0;
                foreach (var brokenCompRef in brokenCompRefs) {
                    var replaceId = newFileIdsInGo[idx];
                    // ReplaceLine(lines, , string newStr);
                    ReplaceLine(lines,brokenCompRef.line,$"  - component: {{fileID: {replaceId}}}");
                    idx++;
                }
            }


            // if (newFileIdsInGo.Count > 1) {
            //     throw new NoFixAvaivableException($"{go.name} has at least one broken component reference and multiple broken component file ids. No easy fix available.");
            // }

            // if (newFileIdsInGo.Count == 1) {

            // }

        }





        foreach (var l in lines) {
            Console.WriteLine(l);
        }


        return;
        foreach (var go in gos) {
            Console.WriteLine($"go: {go.fileId}");

            if (String.IsNullOrEmpty(go.fileId)) {
                // replace line with a new file id of our own.

            }



            foreach (var compRef in go.compRefs) {

                if (String.IsNullOrEmpty(compRef.id)) {

                }
            }

            // any dangling prefab transfrom refs -> err out

            // we could try check wich transforms have m_father with this transfrom,
            // but for now we don't do that
            // any dangling transform child refs
        }




        void addFileIdToLookups(string newFileId) {
            allGoIds.Add(newFileId);
            allFileIds.Add(newFileId);
            addedFileIds.Add(newFileId);
        }


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
        public string fileId;
        public int idLine;
        public string name;
        public List<(int globalLine,string id)> compFileIds = new List<(int globalLine,string id)>();
        public List<(int globalLine,string id)> compRefs = new List<(int globalLine,string id)>();
        public List<(int globalLine,string id)> transformChildRefs = new List<(int globalLine,string id)>();
        public List<(int globalLine,string id)> prefabTransformRefs = new List<(int globalLine,string id)>();
        public List<(int globalLine,string id)> compGoRefs = new List<(int globalLine,string id)>();
    }

    // static Regex fileIdRegex =

    static Regex compRefRegex = new Regex(@"  - component: {fileID: (-?\d+)?}");
    static Regex gameObjRefRegex = new Regex(@"  m_GameObject: {fileID: (-?\d+)?}");
    static Regex compBeginningRegex = new Regex(@"--- !u!\d+ &");
    static Regex transformChildRegex = new Regex(@"  - {fileID: (-?\d+)?}");
    static Regex prefabTransformRegex = new Regex(@"    m_TransformParent: {fileID: (-?\d+)?}");
    static Regex goNameRegex = new Regex(@"  m_Name: (\w+)");



    static List<ParsedGo> ParsePrefab(List<string> lines) {
        ParsedGo currGo = null;
        List<ParsedGo> gos = new List<ParsedGo>();
        var inFrontOfComps = false;

        var x = 0;
        foreach (var line in lines) {

            if (line.StartsWith("--- !u!1 &")) {
                if (currGo != null) {
                    gos.Add(currGo);
                }
                currGo = new ParsedGo();
                inFrontOfComps = true;
                var fileId = line.Substring(line.IndexOf("&") + 1);
                Console.WriteLine($"new go, fileId: {fileId}");
                currGo.fileId = fileId;
                currGo.idLine = x;
            } else if (inFrontOfComps && goNameRegex.Match(line).Success) {
                currGo.name = goNameRegex.Match(line).Groups[1].ToString();

            } else if (inFrontOfComps && compBeginningRegex.Match(line).Success) {
                inFrontOfComps = false;
                currGo.compFileIds.Add((x,fileIdForLine(line)));

            } else if (compRefRegex.Match(line).Success) {
                currGo.compRefs.Add((x,compRefRegex.Match(line).Groups[1].ToString()));
            }
            else if (transformChildRegex.Match(line).Success) {
                currGo.transformChildRefs.Add((x,transformChildRegex.Match(line).Groups[1].ToString()));

            } else if (prefabTransformRegex.Match(line).Success) {
                currGo.prefabTransformRefs.Add((x,prefabTransformRegex.Match(line).Groups[1].ToString()));

            }

            x++;
        }

        // add the last go
        if (currGo != null) {
            gos.Add(currGo);
        }
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
