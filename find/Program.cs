using System;
using System.Collections.Generic;
using System.IO;

namespace find
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
                return;

            var toFind = args[0];
            if (String.IsNullOrEmpty(toFind))
                return;

            var fileExtensionsToSearch = new List<string>();
            for (var i = 1; i < args.Length; i++)
                fileExtensionsToSearch.Add(args[i]);

            RecoursiveFindText(toFind, new DirectoryInfo(Directory.GetCurrentDirectory()), fileExtensionsToSearch); ;
        }

        private static void RecoursiveFindText(string toFind, DirectoryInfo dir, List<string> fileExtensionsToSearch)
        {
            foreach (var file in dir.GetFiles())
            {
                if (fileExtensionsToSearch.Count == 0
                    || fileExtensionsToSearch.Contains(Path.GetExtension(file.Name)))
                {
                    var txt = File.ReadAllText(file.FullName);
                    var matches = "";
                    var lines = txt.Split("\n");
                    for (var i = 0; i < lines.Length; i++)
                        if (lines[i].Contains(toFind))
                            matches += ("-- At line: " + (i + 1) + "\n");

                    if (!String.IsNullOrEmpty(matches))
                    {
                        Console.Write($"\nFound in file: '");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write(file.Name);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("'\nFilepath: '");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write(file.FullName);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("'\n" + matches);
                    }
                }
            }

                foreach (var subDir in dir.GetDirectories())
                    RecoursiveFindText(toFind, subDir, fileExtensionsToSearch);
        }
    }
}
