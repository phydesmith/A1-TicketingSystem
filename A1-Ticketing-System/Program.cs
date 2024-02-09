namespace A1_Ticketing_System;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("**** Ticketing System ****");


        bool promptForInput = true;
        do
        {
            Console.WriteLine("");
            Console.WriteLine("1. Display File Data");
            Console.WriteLine("2. Enter Data to file");
            Console.WriteLine("3. Quit");
            Console.Write("\t enter choice: ");
            String response = Console.ReadLine();
            if (response.Equals("1"))
            {
                Console.Write("Which file to open: " );
                ReadAndDisplayFile(Console.ReadLine());
            }
            else if (response.Equals("2"))
            {
                String fileName = PromptForFileName();
                CreateFileIfDoesNotExist(fileName);
                HandleFileInput(fileName);
            }
            else if (response.Equals("3") || response.ToLower().Equals("q"))
            {
                promptForInput = false;
            }
        } while (promptForInput);


        Console.WriteLine("\n**** Closing Ticketing System ****\n");
    }

    private static void ReadAndDisplayFile(string path)
    {
        if (File.Exists(path))
        {
            Console.WriteLine(Record.DISPLAY_HEADER);
            List<Record> records = ReadFile(path);
            foreach (var record in records)
            {
                Console.WriteLine(record.ToDisplay());
            }
        }
        else
        {
            Console.WriteLine("\t ********** No file to read. **********");
        }
    }

    private static List<Record> ReadFile(string file)
    {
        List<Record> records = new List<Record>();
        using (StreamReader sr = new StreamReader(file))
        {
            sr.ReadLine(); // skip first line (i.e. headers in file)
            while (!sr.EndOfStream)
            {
                records.Add(
                    new Record(
                        sr.ReadLine().Split(",")));
            }
        }
        return records;
    }

    private static void HandleFileInput(String fileName)
    {
        String response = "";
        bool promptForRecord = false;
        using (StreamWriter sw = new StreamWriter(fileName, true))
        {
            do
            {
                Record record = new Record(
                    promptForRecordPart("TicketId"),
                    promptForRecordPart("Summary"),
                    promptForRecordPart("Status"),
                    promptForRecordPart("Priority"),
                    promptForRecordPart("Submitter"),
                    promptForRecordPart("Assigned"),
                    handleWatchers()
                );
                sw.WriteLine(record.ToWrite());
                Console.Write("\t Add Another Record? (y/n): ");
                promptForRecord = Console.ReadLine().ToLower().Equals("y");
            } while (promptForRecord);
        }
    }

    private static String PromptForFileName()
    {
        String response = "";
        Console.Write("Enter file to write to: ");
        response = Console.ReadLine();
        if (String.IsNullOrWhiteSpace(response))
        {
            response = "tickets.csv";
        }
        return response;
    }

    private static void CreateFileIfDoesNotExist(String filename)
    {
        if (!File.Exists(filename))
        {
            using (StreamWriter sw = new StreamWriter(filename))
            {
                sw.WriteLine(Record.WRITE_HEADER);
            }
        }
    }

    private static String promptForRecordPart(String part)
    {
        Console.Write($"Enter {part}: ");
        return Console.ReadLine();
    }

    private static String handleWatchers()
    {
        List<String> watchers = new List<string>();

        bool enteringWatchers = true;
        do
        {
            watchers.Add(promptForRecordPart("Watcher"));
            Console.Write("Enter another? (Y/n): ");
            if (Console.ReadLine().ToLower().Equals("n")) enteringWatchers = false;
        } while (enteringWatchers);


        return String.Join("|", watchers);
    }
}