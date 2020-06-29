# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;


public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== ManipulateArr ====\n");

        var list = new List<int> { 1, 2 , 3};

        var res = new int[list.Count];
        for (var i = 0; i < list.Count; i++) {
            Console.WriteLine(i);
            res[i] = list[i];
        }

        Console.WriteLine("new arr:");
        foreach (var item in res) {
            Console.WriteLine(item);

        }



        return;

        // const int menuOffset = 1200;
        // const int occupiedLayer = 1250;

        // var arr = new [] { 1200, 1210, 1300 };


        // var layersBelowOffset = 0;
        // foreach (var i in arr) {
        //     if (i < menuOffset) {
        //         layersBelowOffset++;
        //     }
        // }

        // Console.WriteLine($"layers below offset: {layersBelowOffset}");


        // var list = arr.ToList();
        // for (var i = menuOffset; i <= occupiedLayer - layersBelowOffset; i++) {
        //     if (Array.IndexOf(arr,i) < 0)  {
        //         var idx = i - menuOffset;
        //         list.Insert(layersBelowOffset + idx,i);
        //     }
        // }


        // var list = arr.ToList();
        // for (var i = 0; i <= occupiedLayer - menuOffset - layersBelowOffset; i++) {
        //     if (Array.IndexOf(arr,i) < 0)  {
        //         list.Insert(i + layersBelowOffset,i + menuOffset);
        //     }
        // }






    }
}
# endif