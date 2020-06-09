# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using Newtonsoft.Json;


public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== JsonDeserialize ====\n");

        var str = @"{""data"":[{""permission"":""user_friends"",""status"":""granted""},{""permission"":""email"",""status"":""granted""},{""permission"":""pages_show_list"",""status"":""granted""},{""permission"":""public_profile"",""status"":""granted""}]}""";

        var parsed = JsonConvert.DeserializeObject<Dictionary<string,string>>(str);

        foreach (var e in parsed.Keys) {
            Console.WriteLine(e);
        }

    }
}
# endif