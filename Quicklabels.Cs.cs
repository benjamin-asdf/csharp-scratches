# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== QuickLabels.cs ====\n");

        foreach (var l in QuickLabels("/home/benj/idlegame/IdleGame/Assets/LoadGroups/WeeklyEvents/WeeklyEvents.prefab.meta")) {
            Console.WriteLine(l);
        }

    }

    public static HashSet<string> QuickLabels(string fileOrMeta) {
        var res = new HashSet<string>();
        var metaFile = Path.GetExtension(fileOrMeta) == ".meta" ? fileOrMeta : $"{fileOrMeta}.meta";
        if (!File.Exists(metaFile)) {
            return res;
        }

        using (var sr = File.OpenText(metaFile)) {
            bool insideLabels = false;
            var line = sr.ReadLine();

            while (line != null) {
                if (line == "labels:") {
                    insideLabels = true;
                } else if (insideLabels) {
                    if (line.IndexOf("- ") == 0) {
                        res.Add(line.Substring(2));
                    } else {
                        break;
                    }
                }
                line = sr.ReadLine();
            }
        }
        return res;
    }

}
# endif