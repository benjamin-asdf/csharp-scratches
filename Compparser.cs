using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;

public static class Programm {

    class PerfClock :  IDisposable {
        Stopwatch _sw;
        string name;

        public PerfClock(string name) {
            _sw = Stopwatch.StartNew();
            this.name = name;
        }

        public void Dispose() {
            Console.WriteLine($"{name} took {_sw.ElapsedMilliseconds} ms");
        }
    }

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

    public static void Main(string[] args) {
        Console.WriteLine("==== Compparser ====\n");



        Environment.CurrentDirectory = Environment.GetEnvironmentVariable("IDLEGAMEDIR");

        // CheckPrefab();

        // var path = "Assets/LoadGroups/Match3/Match3.prefab";
        // var path  = "/home/benj/idlegame/IdleGame/Assets/LoadGroups/Jackpot/Jackpot.prefab";

        var path = "/home/benj/repos/unity-empty/unity-empty/Assets/BestPrefab.prefab";

        // var path = "/home/benj/repos/unity-empty/unity-empty/Assets/Button.prefab";

        CheckPrefab(path);
    }

    class ParsedObj {
        public UnityYamlClassId type;
        public (int origLine, string id) id;
    }

    class ParsedGameObject : ParsedObj {
        public string name;
        public List<CompRef> compRefs;
    }

    // future maybe,
    // class ParsedTransform : ParsedComp {
        // children
        // father
    // }
    // ParsedPrefabInstance : ParsedObj

    class ParsedComp : ParsedObj {
        public (int origLine, string goRef) goRef;
    }

    class StrippedObj : ParsedObj {
        // prefab instance
        // file id + guid
    }

    class ParsedPrefabInstance : ParsedObj {
        // guid
        // father
        public List<(int origLine, string transformRef)> transformRefs = new List<(int origLine, string transformRef)>();
    }

    struct CompRef {
        public string id;
        public int origLine;
    }


    static List<ParsedComp> allComps;
    static List<ParsedObj> allObjs;
    static List<ParsedGameObject> allGameObjs;


    static int currLine = 0;

    public static void CheckPrefab(string inputPath) {

        allComps = new List<ParsedComp>();

        var isDirty = false;
        var inputLines = new List<string>();
        foreach (var line in File.ReadAllLines(inputPath)) {
            inputLines.Add(line);
        }

        allObjs = _ParsePrefab(inputLines);


        foreach (var item in allObjs) {
            Console.WriteLine($"{item.GetType().Name} - {item.id.id}");
        }


        return;


        var goLookup = new Dictionary<string,ParsedGameObject>();

        // these are the known comps that are subscribing to Key gameobject id
        // var goIdRefs = new Dictionary<string,List<ParsedComp>>();
        var goIdRefs = new Dictionary<string,List<string>>();

        foreach (var go in allGameObjs) {
            goLookup.Add(go.id.id,go);
        }


        // collect all obj ids

        // includes the override game objects, yeye

        // foreach com

        foreach (var comp in allComps) {

            var goRefBroken = String.IsNullOrEmpty(comp.goRef.goRef);
            var idBroken = String.IsNullOrEmpty(comp.id.id);

            if (goRefBroken && idBroken) {
                throw new NoFixAvaivableException($"Broken component doesn't have id nor a go ref. Line: {comp.id.origLine}");
            }

            if (!goRefBroken) {

                if (!goIdRefs.TryGetValue(comp.goRef.goRef, out var list)) {
                    goIdRefs.Add(comp.goRef.goRef, new List<string>());
                }
                goIdRefs[comp.goRef.goRef].Add(comp.id.id);

            }


            if (idBroken) {
                if (comp.type == UnityYamlClassId.Transform || comp.type == UnityYamlClassId.RectTransform) {
                    throw new NoFixAvaivableException("Encountered broken file id on a transform. Fix would be complex.");
                }

                // broken comp ref

                // check the obj
                // so we have a dictionary with comp id => comp ?

                if (goLookup.TryGetValue(comp.goRef.goRef, out ParsedGameObject foundGo)) {

                    // fix available.

                    isDirty = true;
                    InsertFileId(inputLines,comp.id.origLine,foundGo.id.id);

                }

            }

            // if id broken
            // we can enlist something a seperate lookup that says 'rewrite comp refs'
            // could be on the go?

            // if comp file id broken



        }

        // foreach comp ref

        // if the id is on no component at all

        // a gameobject ref might also be broken

        // if we have a dangling comp ref, we can bring them together,
        // else we throw



        // if a compref is broken
        // get all the comps that are subscribing to this gameobject
        // already have a list with dangling ones?
        // I want to get the non intersecting elements
        // ()



























































        return;


        List<ParsedGo> gos;
        gos = ParsePrefab(inputLines);
        var allFileIds = new HashSet<string>();

        foreach (var go in gos) {
            allFileIds.Add(go.fileId);
            foreach (var compFileId in go.compFileIds) {
                allFileIds.Add(compFileId.id);
            }
        }

        foreach (var prefabTransformRef in prefabTransformRefs) {
            if (prefabTransformRef != "0" &&
                (String.IsNullOrEmpty(prefabTransformRef) || !allFileIds.Contains(prefabTransformRef))) {
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

                if (String.IsNullOrEmpty(compFileId.id)) {
                    compIdsDirty = true;
                    var newFileId = newUniqueFileId();
                    var idx = go.lines.IndexOf(compFileId.origLine);
                    InsertFileId(go.lines, idx, newFileId);
                    allFileIds.Add(newFileId);
                    compFileIdsInGo.Add(newFileId);
                    Console.WriteLine("a comp file id is null. {compFileId.origLine}");
                } else {
                    compFileIdsInGo.Add(compFileId.id);
                }

            }

            foreach (var compRef in go.compRefs) {
                if (String.IsNullOrEmpty(compRef.item) || !compFileIdsInGo.Contains(compRef.item)) {
                    compIdsDirty = true;
                    Console.WriteLine("some comp ref is not there, or null {compRef.origLine}");
                }
            }

            if (go.compRefs.Count != compFileIdsInGo.Count) {
                Console.WriteLine($"{go.name} - {go.lines[0]}, comp id count is off.");
            }

            if (compIdsDirty || go.compRefs.Count != compFileIdsInGo.Count) {
                // do the component fix part.
                go.ReplaceCompRefs(compFileIdsInGo);
                isDirty = true;
                Console.WriteLine("replacing comp refs");
            }

        }

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
            // // Csole.WriteLine("-------------- woule rewrite: --------------");
            // // Csole.WriteLine(content);
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
                    // gameobject line
                    if ((UnityYamlClassId)typeNum == UnityYamlClassId.GameObject) {
                        fileId = id;
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
        typeNum = default;
        id = default;
        if (!objBeginningRegex.IsMatch(line)) return false;
        var match = objBeginningRegex.Match(line);
        id = match.Groups[2].ToString();
        typeNum = default;
        if (Int32.TryParse(match.Groups[1].ToString(), out var result)) {
            typeNum = (UnityYamlClassId)result;
            return true;
        }
        return false;
    }


    static HashSet<string> prefabTransformRefs;
    static List<string> prefabInstancePart;

    static Regex gameObjStartReg = new Regex(@"--- !u!1 &");
    static Regex compRefRegex = new Regex(@"  - component: {fileID: (-?\d+)?}");
    static Regex gameObjRefRegex = new Regex(@"  m_GameObject: {fileID: (-?\d+)?}");
    static Regex objBeginningRegex = new Regex(@"--- !u!(\d+) &(-?\d+)?");
    static Regex strippedObjBeginningRegex = new Regex(@"--- !u!(\d+) &(-?\d+)? stripped");

    static Regex transformChildRegex = new Regex(@"  - {fileID: (-?\d+)?}");
    static Regex prefabTransformRegex = new Regex(@"    m_TransformParent: {fileID: (-?\d+)?}");
    static Regex goNameRegex = new Regex(@"  m_Name: (\w+)");
    static Regex prefabReg = new Regex(@"--- !u!1001 &(-?\d+)?");

    static List<ParsedGo> ParsePrefab(List<string> lines) {
        List<ParsedGo> gos = new List<ParsedGo>();
        List<string> goLines = new List<string>();
        prefabTransformRefs = new HashSet<string>();


        currLine++;

        var insideHeader = true;
        var insidePrefabInstancePart = false;

        foreach (var line in lines) {
            // var str = ((float)x++ / (float)lines.Count).ToString("P");
            // Csole.WriteLine(str);
            // Csole.WriteLine(line);

            if (!insidePrefabInstancePart && gameObjStartReg.Match(line).Success) {
                if (!insideHeader) {
                    gos.Add(new ParsedGo(goLines));
                    goLines.Clear();
                }
                insideHeader = false;
                goLines.Clear();
            } else if (prefabReg.Match(line).Success) {
                var match = objBeginningRegex.Match(line);
                if (String.IsNullOrEmpty(match.Groups[2].ToString())) {
                    throw new NoFixAvaivableException("The file Id of a prefab instance was missing.\nLet Ben know because we can support a fix.");
                }
                gos.Add(new ParsedGo(goLines));
                goLines.Clear();
                insidePrefabInstancePart = true;
            } else if (insidePrefabInstancePart && prefabTransformRegex.Match(line).Success) {
                prefabTransformRefs.Add(prefabTransformRegex.Match(line).Groups[1].ToString());
                // Console.WriteLine($"{currLine}: {prefabTransformRegex.Match(line).Groups[1].ToString()}");

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
























    static List<ParsedObj> _ParsePrefab(List<string> lines) {
        var objs = new List<ParsedObj>();
        var comps = new List<ParsedComp>();
        var insideHeader = true;
        var currLine = 0;


        ParsedObj currObj = null;

        foreach (var line in lines) {

            if (objBeginningRegex.Match(line).Success) {
                if (currObj != null) {
                    objs.Add(currObj);
                }

                if (TryParseCompBeginningLine(line, out var typeNum, out var id)) {
                    if (strippedObjBeginningRegex.Match(line).Success) {
                        currObj = new StrippedObj();

                    } else if (typeNum == UnityYamlClassId.GameObject) {

                        currObj = new ParsedGameObject();
                    } else if (typeNum == UnityYamlClassId.PrefabInstance) {
                        currObj = new ParsedPrefabInstance();
                    } else {
                        currObj = new ParsedComp();
                    }
                    currObj.id = (currLine,id);


                }

            } else if (currObj is ParsedGameObject gameObject && compRefRegex.Match(line).Success) {

                if (gameObject.compRefs == null) {
                    gameObject.compRefs = new List<CompRef>();
                }
                gameObject.compRefs.Add(new CompRef() { id = compRefRegex.Match(line).Groups[1].ToString(), origLine = currLine });

            } else if (currObj is ParsedComp comp && gameObjRefRegex.Match(line).Success) {

                comp.goRef = (currLine,gameObjRefRegex.Match(line).Groups[1].ToString());

            } else if (currObj is ParsedPrefabInstance prefabInstance && prefabTransformRegex.Match(line).Success) {

                prefabInstance.transformRefs.Add((currLine,prefabTransformRegex.Match(line).Groups[1].ToString()));

            }



            currLine++;


        }

        if (currObj != null) {
            objs.Add(currObj);
        }

        return objs;
    }






























}
