# if false
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;


public static class Programm {

    public static void Main() {
        Console.WriteLine("==== ParsefbJson ====\n");

        var json = File.ReadAllText("/tmp/The Naughty Cult Ltd_/Clash of Streamers/best-response-1b06fddc-4049-4ab8-b6a6-5f5368e25f59");

        // Console.WriteLine(json);
        var value = JsonConvert.DeserializeObject(json, typeof(Example));

        var data = value as Example;
        Console.WriteLine(data.data[0].Id);
        Console.WriteLine(data.paging.next);
        // Console.WriteLine(data.paging.cursors.next);


    }



    public class Friend {
        public string name { get; set; }
        public string id { get; set; }
    }

    public class Cursors {
        public string before { get; set; }
        public string after { get; set; }
    }

    public class Paging {
        public Cursors cursors { get; set; }
        public string next { get; set; }
    }

    public class Summary {
        public int total_count { get; set; }
    }

    public class Example {
        public IList<User> data { get; set; }
        public Paging paging { get; set; }
        public Summary summary { get; set; }
    }


}



public partial class User {
    public string Id { get; set; }

    public string Birthday { get; set; }

    public string Email { get; set; }

    public string FirstName { get; set; }

    public string MiddleName { get; set; }

    public string LastName { get; set; }

    public string Name { get; set; }

    public string ShortName { get; set; }

    public string Gender { get; set; }

    public Uri Link { get; set; }

    public Uri ProfilePic { get; set; }

    public string PublicKey { get; set; }

    // public Page Location { get; set; }
}

// public partial class Experience {
//     public string Id { get; set; }

//     public string Description { get; set; }

//     public User From { get; set; }

//     public string Name { get; set; }

// }

// public partial class Page {
//     public string Name { get; set; }

//     public string Id { get; set; }

//     public uint Checkins { get; set; }

//     public string Description { get; set; }

//     public string Link { get; set; }
// }
# endif