# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== NestedClassState ====\n");

        var c = new Contexts();
        c.state = "hello";
        var best = new Contexts.NestedClass();

    }



}

public class Contexts {
    public string state;

    class NestedClass {

        public void SayHi() {
            Console.WriteLine(state);
        }

    }

}
# endif