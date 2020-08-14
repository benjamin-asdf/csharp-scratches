# if false
using System.Collections.Immutable;
using System;
using System.Linq;
using System.Collections.Generic;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== ImmutableHashSetWithComparer ====\n");
        // var first = ImmutableHashSet.Create(MyComparer.Instance,
        //                                                 new T1()
        //                                                 // new T2(),
        //                                                 // new T2()

        //     );

        // var second = ImmutableHashSet.Create(MyComparer.Instance,
        //                                     new T1());

        // foreach (var e in first.Union(second)) {
        //     Console.WriteLine(e.Name());
        // }

        // var first = new HashSet<ITypeSymbol> (MyComparer.Instance) {
        //     new T1()
        // };

        // var second= new HashSet<ITypeSymbol> (MyComparer.Instance) {
        //     new T1()
        // };

        var first = new HashSet<ITypeSymbol> (MyComparer.Instance);

        first.Add(null);
        // first.Add(new T1());
        first.Add(null);
        // first.Add(new T1());
        // first.Add(null);
        // first.Add(new T1());
        // first.Add(null);
        // first.Add(null);
        // first.Add(null);
        // first.Add(null);
        // first.Add(new T1());
        // first.Add(new T2());

        foreach (var e in first) {
            if (e == null) Console.WriteLine("item in hashset null.");
            else Console.WriteLine(e.Name());
        }

    }

    public class MyComparer : IEqualityComparer<ITypeSymbol> {
        public static readonly IEqualityComparer<ITypeSymbol> Instance = new MyComparer();
        MyComparer() { }

        public bool Equals(ITypeSymbol x, ITypeSymbol y)  {
            // Console.WriteLine($"is {x.Name()} equal to {y.Name()} ? {x.Name() == y.Name()}");
            // Console.WriteLine($"equals x null: {x == null}, y null: {y == null}");
            // if (x == null && y == null) return true;
            // else return x?.Name() == y?.Name();
            return x?.Name() == y?.Name();
        }

        // for inserting into a hashset, it first checks the hashcode
        // if that is already not equal, it will assume the items are not equal.
        // this doesn't lead to the behavior we need for symbols
        // public int GetHashCode(ITypeSymbol obj) => obj?.Name().GetHashCode(StringComparison.Ordinal) ?? 0;

        public int GetHashCode(ITypeSymbol? obj) {
            return 0;
            // return obj?.Name().GetHashCode() ?? 0;

            // Console.WriteLine(obj.GetHashCode());
            // return obj?.GetHashCode() ?? 0;
        }

        // int counter = 0;
        // public int GetHashCode(ITypeSymbol obj) => counter++;
    }

    public interface ITypeSymbol {
        string Name();
    }

    public class T1 : ITypeSymbol {
        public string Name() => "FrozenMap`2";
    }

    public class T2 : ITypeSymbol {
        public string Name() => "FrozenMap`2";
    }



}
# endif