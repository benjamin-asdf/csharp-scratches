# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;
using Microsoft.Extensions.Configuration;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== ConfigBuilder ====\n");

        var configBuilder = new Microsoft.Extensions.Configuration.ConfigurationBuilder()
              .AddJsonFile("/home/benj/repos/csharp/csharp-scratches/bestconfig.json");
jf
        // Console.WriteLine(configBuilder.);

        var config = configBuilder.Build();

        foreach (var c in config.AsEnumerable()) {
            // Console.WriteLine(c);
            Console.WriteLine($"key: {c.Key}, value: {c.Value}");
        }

        Console.WriteLine(config.GetDebugView());



    }
}
# endif