# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== DictIndex.cs ====\n");

        Console.WriteLine(ProductLookups.ProductIndices["f"]);

    }
}

public static class ProductLookups {

    public static Dictionary<string,int> _productIndices;
    public static Dictionary<string,int> ProductIndices = _productIndices ??= InitProductIndices();

    static Dictionary<string,int> InitProductIndices() {
        _productIndices = new Dictionary<string,int>();
        var x = 0;
        foreach (var kvp in PurchaseConst.PRODUCT_DATA_LOOKUP) {
            _productIndices.Add(kvp.Key,x++);

        }
        return _productIndices;
    }

}

public static partial class PurchaseConst {

    public static Dictionary<string, string> PRODUCT_DATA_LOOKUP = new Dictionary<string,string>() {
        { "fu", "fa" },
        { "f", "fa" }
    };

}
# endif