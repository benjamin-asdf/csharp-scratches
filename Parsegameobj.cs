using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

public static class Programm {

    const string deleteFlag = "#flagdelete#";

    public static void Main(string[] args) {
        Console.WriteLine("==== ParseGameObj ====\n");

        var lines = new List<string>();
        foreach (var line in File.ReadAllLines("file")) {
            lines.Add(line);
        }

        ParsePrefab(lines);

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
        public List<(int globalLine,string id)> compRefs = new List<(int globalLine,string id)>();

        // public ParsedGo(string fileId) {
        //     this.fileId = fileId;
        // }

    }

    class ParsedComp {
        ///the file id for this comp
        public string fileId;
        public int idLine;
        ///the go this comp is attached to
        public string goFileId;

    }

    // static Regex fileIdRegex =

    static Regex compRefRegex = new Regex(@"  - component: {fileID: ((-?\d+)|(\n))}");
    static Regex gameObjRefRegex = new Regex(@"  m_GameObject: {fileID: ((-?\d+)|(\n))}");


    static void ParsePrefab(List<string> lines) {
        ParsedGo currGo = null;

        var x = 0;
        foreach (var line in lines) {

            if (line.StartsWith("--- !u!1")) {
                currGo = new ParsedGo();
                if (line.IndexOf("&") < 0) {
                    Console.WriteLine($"Cannot find expected & on line {x}");
                    return;
                }

                var fileId = line.Substring(line.IndexOf("&") + 1);
                currGo.fileId = fileId;
                currGo.idLine = x;
            }

            if (line.StartsWith("  - component: {fileID: ")) {




            }




            x++;
        }


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


}
