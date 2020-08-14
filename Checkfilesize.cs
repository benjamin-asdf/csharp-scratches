# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== CheckFileSize ====\n");
        var AnalzyerErrorLogFile = "/tmp/best-analyzer-log";
        if (File.Exists(AnalzyerErrorLogFile)) {
            var fileInfo = new FileInfo(AnalzyerErrorLogFile);
            Console.WriteLine(fileInfo.Length);
            Console.WriteLine(fileInfo.Length / 1024);

            if (fileInfo.Length > (1024 * 2)) {
                File.Delete(AnalzyerErrorLogFile);
            }
        }

    }
}
# endif