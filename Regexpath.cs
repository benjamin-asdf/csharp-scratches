# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== RegexPath ====\n");

        var p = "/he/fu.el";
        // var reg = @"[/\\]fu.el";

        // var i = Regex.IsMatch(p,reg);
        // Console.WriteLine(i);
        // Console.WriteLine(BuildFileNameRegex("fu.el"));
        // Console.WriteLine(BuildFileNameRegex("fu", "fa"));

        // Console.WriteLine(Regex.IsMatch(p,BuildFileNameRegex("fu")));

        var s = "[/\\\\](fu.el)$";

        Console.WriteLine(s == BuildFileNameRegex("fu.el"));
        Console.WriteLine(Regex.IsMatch(p,s));

    }

    public static string BuildFileNameRegex(params string[] names) {
        return $"[/\\\\]({string.Join("|",names)})$";
    }

}
# endif