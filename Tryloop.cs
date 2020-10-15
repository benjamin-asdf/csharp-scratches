# if false
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

public static class Programm {

    public static async Task Main(string[] args) {
        Console.WriteLine("==== TryLoop ====\n");
        await Task.Run(ReadCmds);
    }


    static async Task ReadCmds() {
        while (true) {
            try {
                Console.WriteLine("working.");
                await Task.Delay(200);
                throw new Exception("lul");
            } catch {
                // many reasons why this could fail
                // ignore
            }
            await Task.Delay(100);
        }
    }

}
# endif