# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== enumtostring ====\n");

        var st = BestEnum.SkinTrading;
        // Console.WriteLine((string)st);
        Console.WriteLine(st.ToString());
        // Console.WriteLine((string)st == st.ToString());

    }


    public enum BestEnum {
        SkinTrading,
        LUl
    }

}
# endif