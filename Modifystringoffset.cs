# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Text.RegularExpressions;


public static class Programm {



    public static void Main(string[] args) {

        var offset = 0;

        Console.WriteLine("==== ModifyStringOffset ====\n");


        var input = "best lul hello lul.";

        var lulIndices = Regex.Matches(input,"lul").Select(match => match.Groups[0].Index);



        foreach (var item in lulIndices) {
            var insertion = "World";
            input = input.Insert(offset + item,insertion);
            offset += insertion.Length;
            Console.WriteLine(item);
        }
        Console.WriteLine(input);





    }
}
# endif
