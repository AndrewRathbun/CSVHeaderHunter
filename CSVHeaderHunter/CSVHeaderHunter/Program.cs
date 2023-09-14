using System;
using System.IO;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        string folderPath;
        string outputPath;

        // Improved argument parsing
        ParseArguments(args, out folderPath, out outputPath);

        // Validate folder path
        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine("Invalid folder path. Exiting.");
            return;
        }

        try
        {
            // Get CSV files and write the output
            await ProcessCsvFiles(folderPath, outputPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    private static void ParseArguments(string[] args, out string folderPath, out string outputPath)
    {
        // Prompt the user to enter the folder path if not specified
        folderPath = args.Length > 0 ? args[0] : GetUserInput("Enter the path of the folder:");
        outputPath = args.Length > 1 ? args[1] : folderPath;
    }

    private static string GetUserInput(string prompt)
    {
        Console.WriteLine(prompt);
        return Console.ReadLine();
    }

    private static async Task ProcessCsvFiles(string folderPath, string outputPath)
    {
        // Asynchronously get CSV files
        var csvFiles = Directory.GetFiles(folderPath, "*.csv", SearchOption.AllDirectories);

        // Change the output extension to .csv
        string outputFile = Path.Combine(outputPath, "output.csv");

        try
        {
            // Asynchronously write output
            using (StreamWriter writer = new StreamWriter(outputFile))
            {
                // Write the header for the CSV file
                await writer.WriteLineAsync("CSV File,First Line");

                foreach (string csvFile in csvFiles)
                {
                    // Gather and print file information
                    FileInfo fileInfo = new FileInfo(csvFile);
                    long fileSizeInBytes = fileInfo.Length; // File size in bytes
                    double fileSizeInMegabytes = fileSizeInBytes / Math.Pow(1024, 2); // File size in megabytes
                    DateTime creationTime = fileInfo.CreationTime; // File creation time

                    Console.WriteLine($"Ingesting file: {csvFile}");
                    Console.WriteLine($"File Size: {fileSizeInBytes} Bytes | {fileSizeInMegabytes:F2} MB");
                    Console.WriteLine($"Created On: {creationTime}");

                    using (StreamReader reader = new StreamReader(csvFile))
                    {
                        string firstLine = await reader.ReadLineAsync();

                        // Quote the csvFile and firstLine variables in case they contain commas or quotes
                        string sanitizedCsvFile = SanitizeCsvField(csvFile);
                        string sanitizedFirstLine = SanitizeCsvField(firstLine);

                        // Write the output in CSV format
                        await writer.WriteLineAsync($"{sanitizedCsvFile},{sanitizedFirstLine}");
                    }
                }
            }

            Console.WriteLine($"Done! Check the output CSV file at: {outputFile}");
        }
        catch (IOException ioEx)
        {
            Console.WriteLine($"An IOException occurred: {ioEx.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    // Method to sanitize each field for CSV
    private static string SanitizeCsvField(string field)
    {
        return $"\"{field.Replace("\"", "\"\"")}\"";
    }
}