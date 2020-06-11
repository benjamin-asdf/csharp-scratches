
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;


public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== ManipulateArr ====\n");

        const int menuOffset = 100;
        const int occupiedLayer = 105;

        var arr = new [] { 1, 40 , 200 };


        var layersBelowOffset = 0;
        foreach (var i in arr) {
            if (i < menuOffset) {
                layersBelowOffset++;
            }
        }

        Console.WriteLine($"layers below offset: {layersBelowOffset}");


        var list = arr.ToList();
        for (var i = menuOffset; i <= occupiedLayer - layersBelowOffset; i++) {
            if (Array.IndexOf(arr,i) < 0)  {
                var idx = i - menuOffset;
                list.Insert(layersBelowOffset + idx,i);
            }
        }


        // var list = arr.ToList();
        // for (var i = 0; i <= occupiedLayer - menuOffset - layersBelowOffset; i++) {
        //     if (Array.IndexOf(arr,i) < 0)  {
        //         list.Insert(i + layersBelowOffset,i + menuOffset);
        //     }
        // }




        Console.WriteLine("new arr:");
        foreach (var e in list) {
            Console.WriteLine(e);

        }






    }
}
