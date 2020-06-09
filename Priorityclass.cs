# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;


public static class Programm {

    public static void Main(string[] _args) {
        Console.WriteLine("==== PriorityClass ====\n");

        var args = "/home/benj/idlegame/IdleGame/Tools/RoslynAnalyzers/EntityClosureCLI/EntityClosureCLI.exe -s /home/benj/idlegame/IdleGame/IdleGame.sln -x (Test)|(^Unity.)|(WIP)|(Editor)|(Plugins)|(TMPro)|(Assembly)|(Monkeys) -i .*Assets.* --no-git -w -v -m 4";
        Environment.SetEnvironmentVariable("CUSTOM_MSBUILD_PATH","/usr/lib/mono/msbuild/Current/bin/");

        var proc = Process.Start("mono",args);
        proc.PriorityClass = ProcessPriorityClass.Idle;
        proc.WaitForExit();


    }
}
# endif