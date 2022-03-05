using System;

namespace CSVHeaderHunter // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // https://www.c-sharpcorner.com/UploadFile/mahesh/how-to-get-a-file-extension-in-C-Sharp/#:~:text=C%23%20Get%20File%20Extension.%20The%20Extension%20property%20of,extn%20%3D%20fi.Extension%3B%20Console.WriteLine%20%28%22File%20Extension%3A%20%7B0%7D%22%2C%20extn%29%3B
            // https://docs.microsoft.com/en-us/dotnet/api/system.io.directory.getfiles?view=net-6.0
            // https://docs.microsoft.com/en-us/dotnet/api/system.io.directory.enumeratefiles?view=net-6.0

            using System ;
            using System.IO ;
            using System.Linq ;

            class Program
            {
                static void Main(string[] args)
                {
                    try

                    // The following example recursively enumerates all files that have a .txt extension, reads each line of the file, and displays the line if it contains the string "Microsoft".


                    // Set a variable to the My Documents path.
                    string docPath =
                        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                    var files = from file in Directory.EnumerateFiles(docPath, "*.txt", SearchOption.AllDirectories)
                        from line in File.ReadLines(file)
                        where line.Contains("Microsoft")
                        select new
                        {
                            File = file,
                            Line = line
                        };

                    foreach (var f in files)
                    {
                        Console.WriteLine($"{f.File}\t{f.Line}");
                    }

                    Console.WriteLine($"{files.Count().ToString()} files found.");
                }
                catch

                (UnauthorizedAccessException uAEx)
                {
                    Console.WriteLine(uAEx.Message);
                }
                catch (PathTooLongException pathEx)
                {
                    Console.WriteLine(pathEx.Message);
                }
            }
        }
    }
}

}