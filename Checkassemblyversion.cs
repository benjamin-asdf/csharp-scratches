# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Reflection;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== CheckAssemblyVersion ====\n");

        // var path = "/home/benj/idlegame/RoslynTools/output/Microsoft.CodeAnalysis.CSharp.dll";
        var path = "/usr/share/dotnet/shared/Microsoft.NETCore.App/3.1.5/System.Runtime.dll";
        // var path = "/usr/share/dotnet/shared/Microsoft.NETCore.App/2.2.8/System.dll";
        // var path = "/usr/share/dotnet/shared/Microsoft.NETCore.App/2.2.8/System.dll";
        // var path = "/usr/share/dotnet/shared/Microsoft.NETCore.App/3.1.5/System.Buffers.dll";
        // var path = "/usr/share/dotnet/shared/Microsoft.NETCore.App/3.1.3/mscorlib.dll";
        // var path = "/usr/share/dotnet/shared/Microsoft.NETCore.App/3.1.5/mscorlib.dll";
        // var path = "/usr/share/dotnet/shared/Microsoft.NETCore.App/3.0.3/System.Runtime.dll";

        // var path = "/home/benj/repos/csharp/Il-pack-sample/bin/Debug/netcoreapp3.0/Il-pack-sample.dll";
        // var dlls = Directory.GetFiles(Environment.CurrentDirectory);
        // var dlls = Directory.GetFiles("/usr/share/dotnet/shared/Microsoft.NETCore.App", ".dll" , SearchOption.AllDirectories);


        // foreach (var dll in dlls) {
        //     Console.WriteLine(dll);
        // }

        // var path = typeof(object).Assembly.Location;
        Console.WriteLine(path);

        Assembly assembly = Assembly.LoadFrom(path);
        Version ver = assembly.GetName().Version;
        Console.WriteLine(ver);


    }
}
# endif