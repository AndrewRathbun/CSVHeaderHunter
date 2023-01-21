using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        // Get the path of the folder from the user
        Console.WriteLine("Enter the path of the folder:");
        string folderPath = Console.ReadLine();

        // Get all CSV files in the folder and its subfolders
        var csvFiles = Directory.GetFiles(folderPath, "*.csv", SearchOption.AllDirectories);

        // Create a new text file to write the first lines of the CSV files
        string outputFile = Path.Combine(folderPath, "output.txt");
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