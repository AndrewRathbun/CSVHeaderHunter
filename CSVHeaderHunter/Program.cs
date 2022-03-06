class Program
{
    static void Main(string[] args)
    {
        //.CSV: comma seperated values
        AppendToCSV();
        ReadCSVFile();
        Console.ReadLine();
    }
    
    // https://crudzone.wordpress.com/wpf/
    static void AppendToCSV()
    {
        var list = Contacts.GetContacts();
        foreach (var c in list)
        {
            File.AppendAllText("contacts.csv", $"{c.Name},{c.Phone}\n");
        }
    }
    static void ReadCSVFile()
    {
        var lines = File.ReadAllLines("C:\\Users\\CFUser\\OneDrive - Kroll\\CyberVM\\EDrive\\Training\\Directory Opus Demo\\Flat View Filtering Demo - Canon KAPE Output, also ctrl f demo\\KapeTriage_L-114213\\EventLogs\\20200919142304_EvtxECmd_Output.csv");
        var list = new List<Contact>();
        foreach (var line in lines)
        {
            var values = line.Split(',');
            if (values.Length==2)
            {
                var contact = new Contact() { Name = values[0], Phone = values[1] };
                list.Add(contact); 
            }
        }
        list.ForEach(x => Console.WriteLine($"{x.Name}\t{x.Phone}"));
    }
} 
public class Contacts
{
    public static List<CsvHeaders> GetContacts()
    {
        return new List<CsvHeaders>()
        {
            new CsvHeaders(){Name="Jill", Phone="333-444-5555"},
            new CsvHeaders(){Name="Jane", Phone="669-444-7777"},
            new CsvHeaders(){Name="Hill", Phone="222-444-8888"},
        };
    }
}
public class CsvHeaders
{
    public string FileName { get; set; }
    public string Headers { get; set; }
}