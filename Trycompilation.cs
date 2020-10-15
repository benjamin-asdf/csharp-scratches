# if false
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.CSharp.Symbols;
// using Microsoft.CodeAnalysis.CSharp.Symbols.Metadata.PE;
// using Microsoft.CodeAnalysis.CSharp.Test.Utilities;
using Microsoft.CodeAnalysis.Emit;


# if false


public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== TryCompilation ====\n");


        // Check that Compilation.Emit actually produces compilation errors.

        string source = @"
class X
{
    public void Main()
    {
        const int x = 5;
        x = x; // error; assigning to const.
    }
}";
        var compilation = CreateCompilation(source);

        EmitResult emitResult;
        using (var output = new MemoryStream())
        {
            emitResult = compilation.Emit(output, pdbStream: null, xmlDocumentationStream: null, win32Resources: null);
        }

        foreach (var d in emitResult.Diagnostics) {
            Console.WriteLine(d);
        }

    }
}
# endif

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== TryCompilation ====\n");
        var source = File.ReadAllText("test-tree");
        var tree = CSharpSyntaxTree.ParseText(source);



        // //We first have to choose what kind of output we're creating: DLL, .exe etc.
        // var options = new CSharpCompilationOptions(OutputKind.ConsoleApplication);
        // options = options.WithAllowUnsafe(true);                                //Allow unsafe code;
        // options = options.WithOptimizationLevel(OptimizationLevel.Release);     //Set optimization level
        // options = options.WithPlatform(Platform.X64);                           //Set platform
        // // options = options.

        var path =  "/usr/share/dotnet/sdk/2.2.402/ref/mscorlib.dll";
        // var mscorlib = MetadataReference.CreateFromFile(typeof(object).Assembly.Location);
        var mscorlib = MetadataReference.CreateFromFile(path);

        // var compilation = CSharpCompilation.Create("MyCompilation",
        //                                           syntaxTrees: new[] { tree },
        //                                           references: new[] { mscorlib },
        //     options: options);                                            //Pass options to compilation


        // foreach (var d in compilation.GetDiagnostics()) {
        //     Console.WriteLine(d);
        // }

        var ass = Assembly.LoadFrom(mscorlib.FilePath);

        Console.WriteLine(ass.GetTypes().Count());



        var compilation = CSharpCompilation.Create("MyCompilation")
            .AddReferences(mscorlib)
            .AddSyntaxTrees(tree);

        EmitResult emitResult;
        using (var pdbStream = new MemoryStream())
        using (var output = new MemoryStream()) {
            //emitResult = compilation.Emit(output, pdbStream: pdbStream, xmlDocumentationStream: null, win32Resources: null);
            // emit options -> portable pdb
            // debug info format
            emitResult = compilation.Emit(output, pdbStream: null, xmlDocumentationStream: null, win32Resources: null);
            
            // using (var fs = File.OpenWrite("out.dll")) {
            //     output.Seek(0, SeekOrigin.Begin);
            //     output.CopyTo(fs);
            //     fs.Flush();
            // }
        }

        // var assss = Assembly.Load("out.dll");
        // Console.WriteLine(assss.CodeBase);

        // foreach (var d in emitResult.Diagnostics) {
        //     Console.WriteLine(d);
        // }
    }
}
# endif