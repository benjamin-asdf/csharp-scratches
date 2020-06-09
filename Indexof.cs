# if false
using System;


public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== IndexOf ====\n");
        log("fa");
        log("t");
        log("Tru");
        log("true");
        log("e");

        void log(string s) {
            Console.WriteLine("True".IndexOf(s) == 0 || "true".IndexOf(s) == 0);
        }
    }
}
# endif