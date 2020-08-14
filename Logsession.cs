# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;

public static class Programm {

    static Log.TraceSession traceSes;

    public static void Main(string[] args) {
        Console.WriteLine("==== LogSession ====\n");


        using (traceSes = new Log.TraceSession(Path.GetTempFileName())) {
            traceSes.Log("... trace log 1");
            traceSes.FlushOnDispose = true;
        }

        Console.WriteLine(traceSes.Valid);




    }
}
# endif