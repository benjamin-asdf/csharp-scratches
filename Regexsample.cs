# if false
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Text.RegularExpressions;


public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== regexsample ====\n");

        // var str = @"* use ""%programs%\Meld\meld\meld.exe"" ""%a""";

        // foreach (Match item in Regex.Matches(str,@"^\* use ""(.+?)""")) {
        //     foreach (Capture g in item.Captures) {
        //         Console.WriteLine(g.ToString());
        //     }
        // }


        var str = offsetLabel(10);
        // foreach (Match item in Regex.Matches(str,$"^{offsetlabelPrefix}:(\\d+)")) {
        //     foreach (var g in item.Groups) {
        //         Console.WriteLine(g.ToString());
        //     }
        // }

        if (TryParseOffsetLabel(str, out int offset)) {
            Console.WriteLine(offset);
        }

    }

    static bool TryParseOffsetLabel(string label, out int offset) {
        offset = -1;
        var matches = Regex.Matches(label,$"^{offsetlabelPrefix}:(\\d+)");
        if (matches.Count == 1 && matches[0].Groups.Count == 2) {
            return int.TryParse(matches[0].Groups[1].ToString(), out offset);
        }
        return false;
    }


    static string offsetLabel(int offset) => $"{offsetlabelPrefix}:{offset}";
    const string offsetlabelPrefix = "rewrite_offset";

}
# endif