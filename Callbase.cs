# if false






















using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== Callbase ====\n");

        var hi = new Inherits();
        hi.Metho();

    }

    public class Inherits : InheritsInterm {
        public override void Metho() {
            Console.WriteLine("super");
            base.Metho();
        }

    }

    public class InheritsInterm : Thebase {
        public override void Metho() {
            Console.WriteLine("interm");
            base.Metho();
        }
    }


    public class Thebase {

        public virtual void Metho() {
            Console.WriteLine("base");
        }

    }

}
# endif