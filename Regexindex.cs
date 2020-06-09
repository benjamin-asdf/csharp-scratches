# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Text.RegularExpressions;


public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== RegexIndex ====\n");

        var input = @"
  m_StaticEditorFlags: 0
  m_IsActive: 1
--- !u!1 &7423736368644821740 stripped
  m_LocalEulerAnglesHint: {x: 0, y: 0, z: 0}
--- !u!114 &
MonoBehaviour:
";

        var pattern = @"--- !u!\d+ &((-?\d+)|(\n))";


        // var input = "   b\nstbe\nrino";
        // var pattern = @"b((e)|(\n))";

        // var input = "hi\n";
        // var pattern = "((i)|(\n))";

        foreach (Match match in Regex.Matches(input, pattern)) {
            Console.WriteLine($"match: {match}");
            foreach (Group g in match.Groups) {
                Console.WriteLine($"group: {g} index: {g.Index}");
            }

            var catchedEmpty = match.Groups[3].Index != 0;
            if (catchedEmpty) {
                Console.WriteLine($"empty catch: index: {match.Groups[3].Index}");
                Console.WriteLine($"rest substring:");
                Console.WriteLine(input.Substring(match.Groups[3].Index));
            }

            var catchedFull = match.Groups[2].Index != 0;
            if (catchedFull) {
                Console.WriteLine($"catched full {match.Groups[2]} index: {match.Groups[2].Index}");
            }

        }

    }











    static void Example() {

        var input = "hi";
        var pattern = "((a)|(i))";

        // match: i
        // group: i index: 1 - whole match
        // group: i index: 1 - first group ((a)|(i))
        // group:  index: 0 - the empty group that didn't caputure
        // group: i index: 1 the group that captured

        foreach (Match match in Regex.Matches(input, pattern)) {
            Console.WriteLine($"match: {match}");
            foreach (Group g in match.Groups) {
                Console.WriteLine($"group: {g} index: {g.Index}");
            }

        }

    }














    // var input = "hello mofo diego &\n diego &10\n";
    // var pattern = @"diego &(\d+)|(\n)";

    // foreach (Match match in Regex.Matches(input,pattern)) {
    //     // if (match.Groups.Count == 2) {
    //     Console.WriteLine($"-- match with {match.Groups.Count} groups");
    //     Console.WriteLine(match);
    //     foreach (var g in match.Groups) {
    //         Console.WriteLine("group:");
    //         Console.WriteLine(g);

    //         // }


    //         // var g = match.Groups[1];
    //         // Console.WriteLine(g);

    //         // if (string.IsNullOrEmpty(g.ToString())) {
    //         //     Console.WriteLine("adding empty string to lookup");
    //         // }
    //     }
    // }



}
# endif
