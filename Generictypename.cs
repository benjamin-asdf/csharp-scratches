# if false

using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Reflection;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== GenericTypeName ====\n");

        // var type = typeof(BestGeneric<int,int>);
        // Console.WriteLine(type.Name);

        var t = typeof(Programm);
        var memberInfos = t.GetMember("CreateDelayedAction");
        var m = memberInfos[0];
        if (m is MethodInfo method) {
            foreach (var p in method.GetParameters()) {
                Console.WriteLine(p.Name);
                Console.WriteLine(p.ParameterType);

            }
        }


    }

    public class BestGeneric<T,T2> {

    }

    public static void M(Func<int> func) {

    }


    public static IEntity CreateDelayedAction(Action<int> action) {
        return null;
    }

}
# endif