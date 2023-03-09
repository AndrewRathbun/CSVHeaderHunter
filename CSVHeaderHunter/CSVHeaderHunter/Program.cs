using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string folderPath;
        string outputPath;

        // Check if the user has specified a folder path as a command-line argument
        if (args.Length > 0)
        {
            folderPath = args[0];
        }
        else
        {
            // Prompt the user to enter the folder path
            Console.WriteLine("Enter the path of the folder:");
            folderPath = Console.ReadLine();
        }

        // Check if the user has specified an output path as a command-line argument
        if (args.Length > 1)
        {
            outputPath = args[1];
        }
        else
        {
            // Use the input folder as the default output path
            outputPath = folderPath;
        }

        // Get all CSV files in the folder and its subfolders
        var csvFiles = Directory.GetFiles(folderPath, "*.csv", SearchOption.AllDirectories);

        // Create a new text file to write the first lines of the CSV files
        string outputFile = Path.Combine(outputPath, "output.txt");
        using (StreamWriter writer = new StreamWriter(outputFile))
        {
            // Write the first line of each CSV file to the text file
            foreach (string csvFile in csvFiles)
            {
                using (StreamReader reader = new StreamReader(csvFile))
                {
                    // Read the first line of each csvFile
                    string firstLine = reader.ReadLine();
                    // Output the filename (csvFile) and first row (firstLine) of each CSV that resides in the folder provider by the user 
                    writer.WriteLine("{0} | {1}", csvFile, firstLine);
                }
            }
        }

        Console.WriteLine("Done! Check the output file at: " + outputFile);
        Console.ReadKey();
    }
}