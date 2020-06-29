# if false
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using System.Diagnostics;


public static class Programm {

    public class NoFixAvaivableException : Exception {
        public NoFixAvaivableException(string message) : base(message) { }
    }

    public class PrefabParseException : Exception {
        public PrefabParseException(string message) : base(message) { }
    }


    const string headerPart =
        @"%YAML 1.1
%TAG !u! tag:unity3d.com,2011:
";

    const string deleteFlag = "#flagdelete#";

    public static void Main(string[] args) {
        Console.WriteLine("==== ParseGameObj ====\n");
        Environment.CurrentDirectory = Environment.GetEnvironmentVariable("IDLEGAMEDIR");

        CheckPrefab("Assets/LoadGroups/Match3/Match3.prefab");
        var sw = Stopwatch.StartNew();







        // foreach (var prefab in File.ReadAllLines("biggest-prefabs")) {
        //     Console.WriteLine(prefab);
        //     CheckPrefab(prefab);
        //     break;
        // }
        Console.WriteLine($"ellapsed: {sw.ElapsedMilliseconds}");

    }

    public static void CheckPrefab(string inputPath) {
        var isDirty = false;
        var inputLines = new List<string>();
        foreach (var line in File.ReadAllLines(inputPath)) {
            inputLines.Add(line);
        }

        var gos = ParsePrefab(inputLines);
        var allFileIds = new HashSet<string>();

        foreach (var go in gos) {
            allFileIds.Add(go.fileId);
            foreach (var compFileId in go.compFileIds) {
                allFileIds.Add(compFileId.id);
            }
        }

        // Console.WriteLine($"go count: {gos.Count}");

        foreach (var prefabTransformRef in prefabTransformRefs) {
            Console.WriteLine($"prefab t ref: {prefabTransformRef}");
            if (String.IsNullOrEmpty(prefabTransformRef) || !allFileIds.Contains(prefabTransformRef)) {
                // NOTE no guide yet because I don't know how often that happens
                throw new NoFixAvaivableException($"{inputPath} has a broken prefab transform ref.\nYou can try grep for\n{prefabTransformRef}\nIn the prefab, then find out which prefab it is and to which transform it should belong. Can poke Ben if you are stuck.");
            }
        }


        var compFileIdsInGo = new HashSet<string>();
        foreach (var go in gos) {
            if (String.IsNullOrEmpty(go.fileId)) {
                // replace line with a new file id of our own.
                var newFileId = newUniqueFileId();
                allFileIds.Add(newFileId);
                InsertFileId(go.lines,0,newFileId);
                isDirty = true;
            }


            compFileIdsInGo.Clear();
            var compIdsDirty = false;
            foreach (var compFileId in go.compFileIds) {
                // Console.WriteLine($"compFileId: {compFileId} ");

                if (String.IsNullOrEmpty(compFileId.id)) {
                    Console.WriteLine($"have null comp file id.");
                    compIdsDirty = true;
                    var newFileId = newUniqueFileId();
                    var idx = go.lines.IndexOf(compFileId.origLine);
                    InsertFileId(go.lines, idx, newFileId);
                    allFileIds.Add(newFileId);
                    compFileIdsInGo.Add(newFileId);
                } else {
                    compFileIdsInGo.Add(compFileId.id);
                }

            }

            foreach (var compRef in go.compRefs) {
                if (String.IsNullOrEmpty(compRef.item) || !compFileIdsInGo.Contains(compRef.item)) {
                    compIdsDirty = true;
                }
            }

            if (compIdsDirty || go.compRefs.Count != compFileIdsInGo.Count) {
                // do the component fix part.
                go.ReplaceCompRefs(compFileIdsInGo);
                isDirty = true;
            }

        }
        Console.WriteLine($"dirty {isDirty}");

        if (isDirty) {
            var sb = new StringBuilder();
            sb.Append(headerPart);

            foreach (var go in gos) {
                foreach (var line in go.lines) {
                    sb.AppendLine(line);
                }

                if (prefabInstancePart != null) {
                    foreach (var line in prefabInstancePart) {
                        sb.AppendLine(line);
                    }
                }
            }

            var content = sb.ToString();
            if (content == headerPart) {
                throw new PrefabParseException("{inputPath} shrank a lot. Something went wrong during parsing.");
            }
            var outputPath = $"{inputPath}-out";
            File.WriteAllText(outputPath, content);
            // Console.WriteLine("-------------- woule rewrite: --------------");
            // Console.WriteLine(content);
        }

        string newUniqueFileId() {var fileIdNum = -1337;
            var x = 0;
            while (allFileIds.Contains(fileIdNum.ToString()) && x++ < 10000) {
                fileIdNum++;
            }
            return fileIdNum.ToString();
        }

    }


    // NOTE we are assuming that prefab instance syntax always appears at the bottom of the prefab, 
    // and that no regular game objects are interspersed


    class ParsedGo {
        public readonly string fileId;
        public readonly string name;
        public List<(string origLine, string id)> compFileIds = new List<(string origLine, string id)>();
        public List<(string origLine, string item)> compRefs = new List<(string origLine, string item)>();
        public List<(string origLine, string item)> transformChildRefs = new List<(string origLine, string item)>();
        public List<(string origLine, string item)> compGoRefs = new List<(string origLine, string item)>();
        public readonly int compRefPartStart;
        public readonly int originalCompRefCount;

        public List<string> lines = new List<string>();

        public ParsedGo(List<string> inputLines) {
            var x = 0;

            foreach (var line in inputLines) {
                lines.Add(line);
                if (TryParseCompBeginningLine(line, out var typeNum, out var id)) {
                    // Console.WriteLine($"found {typeNum}, --  {id}");
                    // gameobject line
                    if ((UnityYamlClassId)typeNum == UnityYamlClassId.GameObject) {
                        fileId = id;
                    } else if (typeNum == UnityYamlClassId.PrefabInstance) {
                        Console.WriteLine(fileId);

                        throw new PrefabParseException("Did not expect to find prefab instance syntax in go part.");
                    } else if ((typeNum == UnityYamlClassId.Transform || typeNum == UnityYamlClassId.RectTransform) && String.IsNullOrEmpty(id)) {
                        throw new NoFixAvaivableException("Encountered broken file id on a transform. Fix would be complex.");
                    } else {
                        // component line
                        compFileIds.Add((line,id));
                    }
                } else if (goNameRegex.Match(line).Success) {
                    name = goNameRegex.Match(line).Groups[1].ToString();
                } else if (compRefRegex.Match(line).Success) {
                    if (compRefPartStart == 0) {
                        compRefPartStart = x;
                    }
                    compRefs.Add((line,compRefRegex.Match(line).Groups[1].ToString()));
                }
                else if (transformChildRegex.Match(line).Success) {
                    transformChildRefs.Add((line,transformChildRegex.Match(line).Groups[1].ToString()));
                }
                x++;
            }
            originalCompRefCount = compRefs.Count;
        }

        public void ReplaceCompRefs(IEnumerable<string> newRefs) {
            lines.RemoveRange(compRefPartStart,originalCompRefCount);
            foreach (var elem in newRefs) {
                lines.Insert(compRefPartStart,$"  - component: {{fileID: {elem}}}");
            }
        }

    }

    static bool TryParseCompBeginningLine(string line, out UnityYamlClassId typeNum, out string id) {
        var match = compBeginningRegex.Match(line);
        id = match.Groups[2].ToString();
        typeNum = default;
        if (Int32.TryParse(match.Groups[1].ToString(), out var result)) {
            typeNum = (UnityYamlClassId)result;
            return true;
        }
        return false;
    }


    static HashSet<string> prefabTransformRefs = new HashSet<string>();
    static List<string> prefabInstancePart;

    static Regex compRefRegex = new Regex(@"  - component: {fileID: (-?\d+)?}");
    static Regex gameObjRefRegex = new Regex(@"  m_GameObject: {fileID: (-?\d+)?}");
    static Regex compBeginningRegex = new Regex(@"--- !u!(\d+) &(-?\d+)?");
    static Regex transformChildRegex = new Regex(@"  - {fileID: (-?\d+)?}");
    static Regex prefabTransformRegex = new Regex(@"    m_TransformParent: {fileID: (-?\d+)?}");
    static Regex goNameRegex = new Regex(@"  m_Name: (\w+)");

    static List<ParsedGo> ParsePrefab(List<string> lines) {
        List<ParsedGo> gos = new List<ParsedGo>();
        List<string> goLines = new List<string>();

        var insideHeader = true;
        var insidePrefabInstancePart = false;

        var x = 0;
        foreach (var line in lines) {
            var str = ((float)x++ / (float)lines.Count).ToString("P");
            Console.WriteLine(str);
            Console.WriteLine(line);

            if (line.StartsWith("--- !u!1 &")) {
                if (!insideHeader) {
                    Console.WriteLine("parse first go.");
                    gos.Add(new ParsedGo(goLines));
                    goLines.Clear();
                }
                insideHeader = false;
                goLines.Clear();
                Console.WriteLine("new go.");
            } else if (line.StartsWith("--- !u!1001 &")) {
                Console.WriteLine("prefab start.");
                var match = compBeginningRegex.Match(line);
                if (String.IsNullOrEmpty(match.Groups[2].ToString())) {
                    throw new NoFixAvaivableException("The file Id of a prefab instance was missing.\nLet Ben know because we can support a fix.");
                }
                gos.Add(new ParsedGo(goLines));
                goLines.Clear();
                insidePrefabInstancePart = true;
            } else if (insidePrefabInstancePart && prefabTransformRegex.Match(line).Success) {
                prefabTransformRefs.Add(prefabTransformRegex.Match(line).Groups[1].ToString());
            }


            goLines.Add(line);
        }

        // aka parse the last go of a prefab that doesn't have prefab instances
        if (!insidePrefabInstancePart) {
            gos.Add(new ParsedGo(goLines));
        } else {
            prefabInstancePart = goLines;
        }
        return gos;
    }

    static string fileIdForLine(string line) {
        return line.Substring(line.IndexOf("&") + 1);
    }

    static void InsertFileId(List<string> lines, int index, string fileId) {
        var line = lines[index];
        if (line.IndexOf("&") < 0) {
            throw new PrefabParseException($"Expected an & in line {index}");
        }

        lines[index] = line.Insert(line.IndexOf("&") + 1,fileId);
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



    public enum UnityYamlClassId {
        Object = 0,
        GameObject = 1,
        Component = 2,
        LevelGameManager = 3,
        Transform = 4,
        TimeManager = 5,
        GlobalGameManager = 6,
        Behaviour = 8,
        GameManager = 9,
        AudioManager = 11,
        InputManager = 13,

        RectTransform = 224,

        PrefabInstance = 1001,
    }
}
# endif
