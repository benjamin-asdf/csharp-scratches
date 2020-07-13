# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== InKeyWord ====\n");
        var iobj = new Best<object>();
        IContravariant<string> istr;
        istr = iobj;


    }

    class Best<A> : IContravariant<A> {}

    interface IContravariant<in A> { }

    interface IExtContravariant<in A> : IContravariant<A> { }

}
# endif