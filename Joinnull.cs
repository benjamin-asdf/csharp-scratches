# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== JoinNull ====\n");
        Console.WriteLine(GetKey("helo", null));
    }

    const string KEY_SEPARATOR = "|";
    public static string GetKey(params object[] parts) {
        return String.Join(KEY_SEPARATOR, parts);
    }
}
# endif