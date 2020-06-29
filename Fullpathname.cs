# if false
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;


public static class Programm {

    static string[] fileExtensionsOfInterest;
    static string[] FileExtensionsOfInterest => fileExtensionsOfInterest ?? (fileExtensionsOfInterest = new string[] {".prefab", ".meta"});

    public static void Main(string[] args) {
        Console.WriteLine("==== FullPathName ====\n");
        var origDir = Path.GetFullPath(".");
        Environment.CurrentDirectory = "/tmp/merge-sampleqfhGiE/";

        var bestPrefabs = new HashSet<string>();
        var stagedFiles = GetStagedFiles();
        var stagedFilesOfInterest = new HashSet<string>();
        var filesToAddBack = new HashSet<string>();
        var filesToRestoreConflicts = new HashSet<string>();

        Console.WriteLine("staged files of interest follow:");
        foreach (var p in stagedFiles) {
            if (Array.IndexOf(FileExtensionsOfInterest,Path.GetExtension(p)) != -1) {
                Console.WriteLine(p);
                stagedFilesOfInterest.Add(p);
            }
        }

        // unresolve all files of interest

        UnresolveFiles(stagedFilesOfInterest);

        // // now we can get the actual conflicted ones

        Console.WriteLine("unmergedPrefabs:");
        var unmergedPrefabs = GetUnmergedFiles();
        foreach (var p in unmergedPrefabs) {
            Console.WriteLine(p);
        }

        // // check
        // // ok if not conflicted || it passes the check
        // // we need to add the ones that are fine
        foreach (var path in stagedFilesOfInterest) {
            Console.WriteLine($"staged file of interest: {path}");
            if (!unmergedPrefabs.Contains(path) || FileIsIntegrit(path)) {
                filesToAddBack.Add(path);
                Console.WriteLine($"add file to add back {path}");
            } else {
                filesToRestoreConflicts.Add(path);
                Console.WriteLine($"add file to restore conflict {path}");
            }

        }

        if (filesToAddBack.Count > 0) {
            Git.runGitCommandSync("add", $"-- {PathsAsArg(filesToAddBack)}", false);
            Console.WriteLine($"do git add -- {PathsAsArg(filesToAddBack)}");
        }

        if (filesToRestoreConflicts.Count > 0) {
            Git.Checkout($"--merge -- {PathsAsArg(filesToRestoreConflicts)}");
            Console.WriteLine($"do git checkout merge -- {PathsAsArg(filesToRestoreConflicts)}");
        }


    }

    static bool FileIsIntegrit(string file) {
        try {
            return DoFileIntegrityCheckNoCatch(file);
        } catch (Exception e) {

            Console.WriteLine("{file} was not integrit:");
            Console.WriteLine(e);
            Console.WriteLine(e.Message);
            // if (e is IntegrityCheckFailException IntegrityException) {
            //     UnityEngine.Debug.LogWarning($"!!!!! Something is wrong with one of your staged files, more info follows!");
            //     var path = IntegrityException.filePath;
            //     UnityEngine.Debug.Log($"!! Restoring conflict for {path}");
            //     UnityEngine.Debug.Log($"If you want to refresh without this check, leave the file unstaged until you fixed it.");
            //     Git.Checkout($"--merge -- \"{path}\"");
            // } else {
            //     UnityEngine.Debug.Log("Error during file Integrity check. Exception follows.");
            // we don't want to restore conflicts in this case
            // return true
            // }

            // Debug.LogException(e);
            return false;
        }

    }

    static bool DoFileIntegrityCheckNoCatch(string file) {
        var isPrefab = file.EndsWith(".prefab");
        if (isPrefab || file.EndsWith(".meta")) {
            if (HasMergeMarkers(file)) {
                // throw new IntegrityCheckFailException(file,"File has merge markers left.");
                // throw new Exception();
                Console.WriteLine("would throw merge markers");
            }

            if (isPrefab) {
                var res = !TryApplyPrefabFix(file);
                Console.WriteLine($"DoFileIntegrityCheckNoCatch res: {res}");
                return res;
            }
        }

        Console.WriteLine($"DoFileIntegrityCheckNoCatch res: true, no prefab or meta");
        return true;
    }

    static bool HasMergeMarkers(string path) {
        var content = File.ReadAllText(path);
        return content.Contains("<<<<<<<");
    }

    static bool TryApplyPrefabFix(string path) {
        // try get out var content
        if (new Random((int)DateTime.Now.Ticks).Next() % 2 == 0) {
            Console.WriteLine($"imagine {path} is broken");
            File.WriteAllText(path, "Fixed content.");
            return true;
        }

        return false;

    }



    static Regex _unmergedFilesRegex;
    static Regex unmergedFilesRegex = _unmergedFilesRegex ?? (_unmergedFilesRegex = new Regex(@"^(\w\w) (.*)$"));
    static HashSet<string> GetUnmergedFiles() {
        return MatchGitFiles(unmergedFilesRegex,2);
    }
    static Regex _stagedModifiedFilesRegex;
    static Regex stagedModifiedFilesRegex = _stagedModifiedFilesRegex ?? (_stagedModifiedFilesRegex = new Regex(@"^M  (.*)$"));
    static HashSet<string> GetStagedFiles() {
        return MatchGitFiles(stagedModifiedFilesRegex,1);
    }

    static HashSet<string> MatchGitFiles(Regex regex, int groupIndex) {
        var res = new HashSet<string>();
        foreach (var line in ProcUtil.OutputAsLines("git","status --porcelain")) {
            var path = regex.Match(line).Groups[groupIndex].ToString();
            if (!String.IsNullOrEmpty(path)) {
                res.Add(path);
            }
        }
        return res;
    }


    // static void (IEnumerable ) {

    // }

    static HashSet<string> GetConflictedFiles(params string[] patterns) {
        var currentUnmerged = GetUnmergedFiles();
        var files = new HashSet<string>();
        foreach (var p in Directory.GetFiles(".",".",SearchOption.AllDirectories)) {
            var ex = Path.GetExtension(p);
            if (Array.IndexOf(patterns,p) > 0) {
                files.Add(p);
            }
        }

        var sb = new StringBuilder();
        sb.Append("--unresolve");
        foreach (var path in files) {
            if (!currentUnmerged.Contains(path)) {
                Console.WriteLine($"add {path}");
                sb.Append($" \"{path}\"");
            }
        }

        Console.WriteLine(sb);

        try {
            Git.runGitCommandSync("update-index", sb.ToString(), false);
        } catch (Exception e) {
            Console.Error.WriteLine(e);
        } finally {
            // Environment.CurrentDirectory = origDir;
        }

        return GetUnmergedFiles();
    }

    static void UnresolveFiles(IEnumerable files) {
        try {
            Git.runGitCommandSync("update-index", $"--unresolve {PathsAsArg(files)}", false);
        } catch (Exception e) {
            Console.Error.WriteLine(e);
        } finally {
            // Environment.CurrentDirectory = origDir;
        }
    }

    static string PathsAsArg(IEnumerable paths) {
        var sb = new StringBuilder();
        foreach (var path in paths) {
            sb.Append($" \"{path}\"");
        }
        return sb.ToString();
    }


}
# endif