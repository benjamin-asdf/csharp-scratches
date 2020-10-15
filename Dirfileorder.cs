# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== DirFileOrder ====\n");

        // /tmp/team-cmds-handles/IdleGame/
        Console.WriteLine(Path.GetTempPath());
        var handlePath = Path.Combine(Path.GetTempPath(),"team-cmds-handles","IdleGame");
        Console.WriteLine(Directory.Exists(handlePath));

        foreach (var f in Directory.GetFiles(handlePath)) {
            Console.WriteLine(f);

        }

    }
}
# endif