# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== NonReadonly ====\n");


        PurchaseConst.BestData.Add(10);
        foreach (var i in PurchaseConst.BestData) {
            Console.WriteLine(i);
        }

    }

    public static class PurchaseConst {
        public static readonly List<int> BestData = new List<int> { 1, 2 , 3};

    }



}
# endif