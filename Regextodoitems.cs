# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

public static class Programm {
















    static Regex? _todoItemRegex;
    // static Regex todoItemRegex = _todoItemRegex ??= new Regex("(((?:\\[\\[\\[cog(?s:.+))+?todo(?s:.+)(https?://\\w+|disable BEST043))|(todo))", RegexOptions.IgnoreCase | RegexOptions.Multiline);

    const string whitelisterParts = "(?:(?:https?://\\w+|disable BEST043))";
    static Regex todoItemRegex = _todoItemRegex ??= new Regex($"((((?:\\[\\[\\[cog)|{whitelisterParts}(?s:.+)todo))|todo(?s:.+){whitelisterParts}|(todo))", RegexOptions.IgnoreCase | RegexOptions.Multiline);

    // static Regex todoItemRegex = new Regex("\\[\\[\\[cog(?s:.+)todo", RegexOptions.IgnoreCase | RegexOptions.Multiline);



    public static void Main(string[] args) {
        Console.WriteLine("==== RegexTodoItems ====\n");



        // var ok = "//todo  \n e he https://";
        var ok = "//todo \r\n  e he https://lul";
        var alsoOk = "//todo \r\n  e \r\n he http://lul";
        var anotherOK = "// disable BEST043 //todo \r\n  e ";
        var secondOK = "//todo \r\n  e disable BEST043";
        var notOk = "// todo no ok.,";
        var notOk2 = "// todo \n also no ok.,";
        var cog = @"
        class c {
            int a;
            /*[[[cog
            if Platforms.VLatest != 1:
                cog.outl(TODO PLATFORMS)
            ]]]*/
            //[[[end]]]
        }
        ";

        var example = "// TODO check if URL redirects to hotstories https://trello.com/c/Ymn0Sz8D";

        // LogMatches(todoItemRegex,ok);
        // LogMatches(todoItemRegex,secondOK);
        // LogMatches(todoItemRegex,notOk);

        var s = @"
/*
                  todo
                  with multiple lines
                  and a a web link http://heeh
                */

";
        // report(s);

        // LogMatches(todoItemRegex,s);
        // LogMatches(todoItemRegex,cog);
        // LogMatches(todoItemRegex,secondOK);

        // LogMatches(todoItemRegex,notOk);

        report(s);
        report(ok);
        report(alsoOk);
        report(secondOK);
        report(notOk);
        report(notOk2);
        report(example);
        report(cog);
        report(anotherOK);

        // Console.WriteLine(Regex.IsMatch("hotstories https://trello.com/c/Ymn0Sz8D","https?://\\w+"));
        // Console.WriteLine(Regex.IsMatch("hotstories https://trello.com/c/Ymn0Sz8D","(https?://\\w+|disable BEST0043)"));
        // Console.WriteLine(Regex.IsMatch("TODO hotstories https://trello.com/c/Ymn0Sz8D","(todo(.*[\r\n].*)+?(https?://\\w+|disable BEST0043))",RegexOptions.IgnoreCase | RegexOptions.Multiline));
        // Console.WriteLine(Regex.IsMatch("todo hotstories https://trello.com/c/Ymn0Sz8D","(todo(.*[\r\n].*)+?(https?://\\w+|disable BEST0043))",RegexOptions.IgnoreCase | RegexOptions.Multiline));
        // Console.WriteLine(Regex.IsMatch("todo \n\r hotstories https://trello.com/c/Ymn0Sz8D","(todo(?s:.+)(https?://\\w+|disable BEST0043))",RegexOptions.IgnoreCase | RegexOptions.Multiline));
        // Console.WriteLine(Regex.IsMatch("todo \n hotstories https://trello.com/c/Ymn0Sz8D","(todo(?s:.+)(https?://\\w+|disable BEST0043))",RegexOptions.IgnoreCase | RegexOptions.Multiline));


        void report(string s) {
            var match = todoItemRegex.Match(s);
            if (match.Success) {
                if (match.Groups[4].Success) {
                    Console.WriteLine("report!");
                    Console.WriteLine(s);
                }
            }
        }

        static void LogMatches(Regex reg, string s) {
            Console.WriteLine($"--- matching: {s}..");
            var x = 0;
            foreach (var g in reg.Match(s).Groups) {
                Console.WriteLine($"group: {x++}");
                Console.WriteLine(g.ToString());
            }
        }

    }



}
# endif