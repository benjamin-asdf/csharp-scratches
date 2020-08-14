# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== SwitchNullable ====\n");

        // var hi = new Best();
        Best hi = null;

        foreach (var i in new [] {1,2}) {

            switch (hi?.day) {
                case DayOfWeek.Thursday: {
                    Console.WriteLine("thursday");
                    break;
               }
                default:
                    Console.WriteLine("default");
                    break;
            }

        }

    }


    class Best {
        public DayOfWeek day => DayOfWeek.Thursday;
    }

}
# endif