using System.CommandLine;
using System.CommandLine.NamingConventionBinder;
using System.Globalization;
using System.Reflection;
using Serilog;
using Serilog.Core;
using Serilog.Events;


namespace CsvHeaderHunter;

internal class Program
{
    private static string _activeDateTimeFormat;
    private static RootCommand _rootCommand;
    private static readonly string BaseDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

    private static readonly string Header =
        $"CSVHeaderHunter version {Assembly.GetExecutingAssembly().GetName().Version}" +
        "\r\n\r\nAuthor: Andrew Rathbun (andrew.d.rathbun@gmail.com)" +
        "\r\nhttps://github.com/AndrewRathbun/CSVHeaderHunter";

    private static readonly string Footer =
        @"Examples: CSVHeaderHunter.exe -d ""C:\"" " +
        "\r\n\t " +
        "    Short options (single letter) are prefixed with a single dash. Long commands are prefixed with two dashes";

    private static async Task Main(string[] args)
    {
        _rootCommand = new RootCommand
        {
            new Option<string>(
                "-d",
                "Directory to process recursively that contains CSV files."),
            /*new Option<string>(
                "--csv",
                "Directory to save CSV formatted results to"),
            new Option<bool>(
                "--debug",
                () => false,
                "Show debug information during processing"),
            new Option<bool>(
                "--trace",
                () => false,
                "Show trace information during processing")*/
        };

        _rootCommand.Description = Header + "\r\n\r\n" + Footer;

        _rootCommand.Handler = CommandHandler.Create(DoWork);

        await _rootCommand.InvokeAsync(args);

        Log.CloseAndFlush();
    }


    /*
    /*
    static void Main(string[] args)
    {
        //.CSV: comma seperated values
        ReadCSVFileHeaders();


        // AppendToCSV();


        Console.ReadLine();
    }
    #1#
    // https://crudzone.wordpress.com/wpf/

    /*static void AppendToCSV()
    {
        var list = Contacts.GetContacts();
        foreach (var c in list)
        {
            File.AppendAllText("contacts.csv", $"{c.Name},{c.Phone}\n");
        }
    }#1#
    static void ReadCSVFileHeaders()
    {
        var lines = File.ReadAllLines(
            "d", d;
        var list = new List<CsvFiles>();
        foreach (var line in lines)
        {
            var values = line.Split(',');
            if (values.Length == 2)
            {
                var csvFiles = new CsvFiles();
                list.Add(csvFiles);
            }
        }
    }
    */

    private static void DoWork(string f, string d, string csv, bool debug, bool trace)
    {
        /*
        var levelSwitch = new LoggingLevelSwitch();

        _activeDateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        var formatter =
            new DateTimeOffsetFormatter(CultureInfo.CurrentCulture);

        var template = "{Message:lj}{NewLine}{Exception}";

        
        if (debug)
        {
            levelSwitch.MinimumLevel = LogEventLevel.Debug;
            template = "[{Timestamp:HH:mm:ss.fff} {Level:u3}] {Message:lj}{NewLine}{Exception}";
        }

        if (trace)
        {
            levelSwitch.MinimumLevel = LogEventLevel.Verbose;
            template = "[{Timestamp:HH:mm:ss.fff} {Level:u3}] {Message:lj}{NewLine}{Exception}";
        }

        if (Directory.Exists(csv) == false)
        {
            Log.Information("Path to {Csv} doesn't exist. Creating...", csv);

            try
            {
                Directory.CreateDirectory(csv);
            }
            catch (Exception e)
            {
                Log.Fatal(e, "Unable to create directory {Csv}. Does a file with the same name exist? Exiting",
                    csv);
                Console.WriteLine();
                return;
            }
            */

        if (string.IsNullOrEmpty(d) == false)
        {
            if (Directory.Exists(d) == false)
            {
                Log.Information("Looking for CSV files in {D}", d);
                Console.WriteLine();
            }

            ProcessFile(Path.GetFullPath(d), csv);
        }
        else
        {
            //Directories

            var files = new List<string>();

            var enumerationOptions = new EnumerationOptions
            {
                IgnoreInaccessible = true,
                MatchCasing = MatchCasing.CaseSensitive,
                RecurseSubdirectories = true,
                AttributesToSkip = 0x0
            };

            IEnumerable<string> files2;

            files2 = Directory.EnumerateFileSystemEntries(d, "*.csv", enumerationOptions);
            
            foreach (var file in files)
            {
                try
                {
                    ProcessFile(file, csv);
                }
                catch (Exception e)
                {
                    Log.Error(e, "Error processing {File}: {Message}", file, e.Message);
                }
            }
        }
    }
    private class DateTimeOffsetFormatter : IFormatProvider, ICustomFormatter
    {
        private readonly IFormatProvider _innerFormatProvider;

        public DateTimeOffsetFormatter(IFormatProvider innerFormatProvider)
        {
            _innerFormatProvider = innerFormatProvider;
        }

        public object? GetFormat(Type? formatType)
        {
            throw new NotImplementedException();
        }

        public string Format(string? format, object? arg, IFormatProvider? formatProvider)
        {
            throw new NotImplementedException();
        }
    }

    private static void ProcessFile(string getFullPath, string csv)
    {
        Console.WriteLine("test");
        throw new NotImplementedException();
    }
}




internal class ApplicationArguments
{
    public string File { get; set;}
    public string Directory { get; set;}
    /*
    public string CsvDirectory { get; set; }
    public bool Debug { get; set; }
    public bool Trace { get; set; }
*/
}