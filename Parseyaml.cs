# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;

public static class Programm {

    static string withoutLabel = "/home/benj/repos/unity-sample/UnitySample/Assets/LoadGroups/Directional Light.prefab.meta";
    static string hasLabel = "/home/benj/repos/unity-sample/UnitySample/Assets/Prefabs/UnusedPrefab.prefab.meta";

    public static void Main(string[] args) {
        Console.WriteLine("==== ParseYaml ====\n");

        foreach (var label in QuickLabels(hasLabel)) {
            Console.WriteLine(label);
        }

    }

    static HashSet<string> QuickLabels(string metaFile) {
        if (Path.GetExtension(metaFile) != ".meta") {
            // warning
            return null;
        }

        var res = new HashSet<string>();
        using (var sr = File.OpenText(hasLabel)) {
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