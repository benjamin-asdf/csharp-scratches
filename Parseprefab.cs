# if false
using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

public static class Programm {

    // var res = Regex.Match("best", "est").Result("ammm");
    // Console.WriteLine(res);

    const string nullKey = "null";

    public static void Main(string[] args) {
        Console.WriteLine("==== ParsePrefab ====\n");
        string res = null;

        var objIdsToLocation = new Dictionary<string,List<int>>();
        var gameObjRefsToLocations = new Dictionary<string,List<int>>();
        var compRefsToLocations = new Dictionary<string,List<int>>();
        var prefabParentRefsToLocations = new Dictionary<string,List<int>>();

        var prefabPath = "/home/benj/repos/unity-empty/unity-empty/Assets/BestPrefab.prefab";

        // get rid of the 'stripped' words that the merge tool puts in files
        // var prefabContents = Regex.Replace(File.ReadAllText(prefabPath),@"(--- !u!\d+ &.*) stripped\n","$1\n");

        // get rid of the 'stripped' words that the merge tool puts in files
        var prefabContents = File.ReadAllText(prefabPath);

        fillMatchLookup(prefabContents,objIdsToLocation,@"--- !u!\d+ &((-?\d+)|(\n))");
        fillMatchLookup(prefabContents,gameObjRefsToLocations,@"  m_GameObject: {fileID: ((-?\d+)|(\n))}");
        fillMatchLookup(prefabContents,compRefsToLocations,@"  - component: {fileID: ((-?\d+)|(\n))}");
        fillMatchLookup(prefabContents,prefabParentRefsToLocations,@"    m_TransformParent: {fileID: ((-?\d+)|(\n))}");

        // prefab transform parents, atm not supported

        foreach (var kvp in prefabParentRefsToLocations) {
            if (!objIdsToLocation.ContainsKey(kvp.Key) || kvp.Key == nullKey) {
                Console.WriteLine("Warning! There is a prefab instance in this prefab and the reference to the transform parent is broken. Auto fix is not supported yet.");
                return;

            }
        }



        // get all the comprefs that are not pointing towards something in the file
        var danglingCompRefs = new Dictionary<string,List<int>>();
        var danglingGameObjRefs = new Dictionary<string,List<int>>();

        foreach (var kvp in compRefsToLocations) {
            if (!objIdsToLocation.ContainsKey(kvp.Key) || kvp.Key == nullKey) {
                danglingCompRefs.Add(kvp.Key,kvp.Value);
            }
        }

        // all the gameobject refs that are supposed to point to something in the file but dont
        foreach (var kvp in gameObjRefsToLocations) {
            if (!objIdsToLocation.ContainsKey(kvp.Key) || kvp.Key == nullKey) {
                danglingCompRefs.Add(kvp.Key,kvp.Value);
            }
        }


        // var brokenObjIds = objIdsToLocation.TryGetValue(nullKey, out List<int> value) [nullKey].

        if (objIdsToLocation.TryGetValue(nullKey, out List<int> brokenObjLocations)) {

            // we have multiple broken objIds and are not able to handle danglingCompRefs with confidence, err out
            if (danglingCompRefs.Count > 0 && brokenObjLocations.Count > 1) {
                Console.WriteLine("Warning! - broken component refs on gameboject(s) and no easy fix available!");
                Console.WriteLine("You need to revert this prefab to a working version and fix manually.");
                return;
            }


            if (danglingCompRefs.Count == 1 && brokenObjLocations.Count == 1) {
                // if we find a single broken comp ref
                // + we find a single empty pointer
                // -> we can combinerino them
                res = prefabContents.Insert(brokenObjLocations[0],danglingCompRefs.Keys.First());


            }

            if (danglingCompRefs.Count > 0) {


            }



        } else {

            // no brokenObjLocations
            // delete danglingCompRefs but log a warning with the gameObject name

            // foreach (var )


        }




        // if we find a single broken comp ref,
        // we are a bit concerned, but will delete it
        // maybe log warning

        // if we find a single broken gameobject ref
        // we search upwarts for the containing gameboject and assume it's the thing

        Console.WriteLine("-- res: ---");
        Console.WriteLine(res);



    }

    static string MaybeSurroundingGameObjName(int location) {

    }

    static void fillMatchLookup(string input, Dictionary<string,List<int>> lookup, string pattern) {
        foreach (Match match in Regex.Matches(input,pattern)) {
            // group[0] -> whole match part
            // group[1] -> (()|()) part
            // group[2] -> (\d+) part
            // group[3] -> (\n) part
            var key = match.Groups[2].Index != 0
                ? match.Groups[2].ToString()
                : nullKey;

            var index = match.Groups[2].Index + match.Groups[3].Index;

            if (!lookup.TryGetValue(key, out var list)) {
                lookup.Add(key,new List<int>());
            }
            lookup[key].Add(index);
        }
    }




    // static bool DoFilePointerCheck(string prefabContents, ) {

    // }






    // NOTE that this breaks should people have custom merge markers
    static bool ContainsMergeMarkers(string content) => content.Contains("<<<<<<<");


}

// Console.WriteLine("--");
// Console.WriteLine(match.ToString());
// foreach (Group g in match.Groups) {
//     objIdsToLocation.Add(g.ToString(),g.Index);
//     // Console.WriteLine($"group: {g.ToString()}, index: {g.Index}");
// }
# endif
