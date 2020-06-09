# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;


public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== CycleEnum ====\n");

        var curr = Stake.Rare;

        for (var i = 0; i < 10; i++) {
            curr++;
            curr = (int)curr >= Enum.GetValues(typeof(Stake)).Length ? 0 : curr;
            Console.WriteLine(curr);

        }



    }
}

public enum Stake {
    Rare = 0,
    Epic = 1,
    Legendary = 2,
    ShinyLegendary = 3,
}
# endif