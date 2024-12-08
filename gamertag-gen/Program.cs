using System.IO;
using System.Net;

int maxLength = 16;
int nameCount = 1;
int wordCount = 2;
bool cringeify = false;
bool useAllCaps = false;
string wordlistPath = $"{Environment.CurrentDirectory}/wordlist.txt";

char[] blacklistChars = new char[]
{ '<', ',', '>', '.', '?', '/', '"', '\'', ':', ';', '{', '[', '}', '}', '\\', '|', '+', '=', ')', '(', '*', '&', '^', '%', '$', '#', '@', '!', '`', '~', '£' };

Random random = new Random();

void Entry()
{
    HandleCommandLineArgs();

    if (!File.Exists(wordlistPath))
    {
        LogInfo(LogType.Error, "Please make sure you have a wordlist!");
        return;
    }

    LogInfo(LogType.Info, "Simple name generator, written by BLVCKROSE (No I did not use this program to come up with my name, that's why it sucks lul)");
    LogInfo(LogType.Info, $"Name max length: {maxLength}");
    LogInfo(LogType.Info, $"Amount of names to generate: {nameCount}");
    LogInfo(LogType.Info, $"Max amout of words to use in a single name: {wordCount}");
    LogInfo(LogType.Info, $"Use X's (Or cringe-ify it lol): {cringeify}");
    LogInfo(LogType.Info, $"ALL CAPS MODE: {useAllCaps}");
    LogInfo(LogType.Info, $"Wordlist path: {wordlistPath}");

    for (int i = 0; i < nameCount; i++)
    {
        string name = GenerateName();

        Console.Write(name);

        if (name.Length > 1)
            Console.Write($" (Name is {name.Length} characters long)");
        else
            Console.Write($" (Name is 1 character long, bruh...)");

        Console.WriteLine();
    }
}

void HandleCommandLineArgs()
{
    string[] args = Environment.GetCommandLineArgs();

    for (int i = 0; i < args.Length; i++)
    {
        switch (args[i])
        {
            default:
                continue;

            case "--maxLength":
                i++;
                maxLength = int.Parse(args[i]);
                break;

            case "--nameCount":
                i++;
                nameCount = int.Parse(args[i]);
                break;

            case "--wordCount":
                i++;
                wordCount = int.Parse(args[i]);
                break;

            case "--cringeify":
                i++;
                cringeify = bool.Parse(args[i]);
                break;

            case "--useAllCaps":
                i++;
                useAllCaps = bool.Parse(args[i]);
                break;

            case "--wordlistPath":
                i++;
                wordlistPath = args[i];
                break;

            case "--help":
                Help();
                break;
        }
    }
}


void Help()
{
    Console.WriteLine("--maxLength\t\tUsed to determine the length of the generated names.");
    Console.WriteLine("--nameCount\t\tHow many names will be generated.");
    Console.WriteLine("--wordCount\t\tHow many words will be used to generate a single name.");
    Console.WriteLine("--cringeify\t\tAdds X's to the name (xXExampleNameXx, Personally I fucking hated these but you do you...)");
    Console.WriteLine("--useAllCaps\t\tMAKES YOUR NAME LOOK LIKE IT WAS TYPED WITH CAPS LOCK ON.");
    Console.WriteLine("--wordlistPath\t\tSet's the path to a required wordlist.");
    Console.WriteLine("--help\t\t\tShows this help message.");

    Environment.Exit(0);
    
}

string GenerateName()
{
    string[] words = File.ReadAllLines(wordlistPath);
    string combo = "";

    for (int i = 0; i < wordCount; i++)
    {
        string word = words[random.Next(0, words.Length)];

        List<char> wordCharArray = word.ToCharArray().ToList();

        if (wordCharArray.Count > 0) wordCharArray[0] = char.ToUpper(wordCharArray[0]);

        string text = new string(wordCharArray.ToArray());

        foreach (char c in blacklistChars)
            text = text.Replace($"{c}", "");

        combo += text;
    }
    string result = combo;

    // God I hated these names XD, but if you want here...
    if (cringeify)
    {
        if (useAllCaps)
            result = $"xX{combo.ToUpper()}Xx";
        else
            result = $"xX{combo}Xx";
    }
    else
    {
        if (useAllCaps)
            result = combo.ToUpper();
        else
            result = combo;
    }

    if (result.Length > maxLength)
        return GenerateName(); // Try again if we get a name that is good sized...
    else
        return result;
}

void DownloadTextFile()
{
    string url = "https://www.cs.cmu.edu/~biglou/resources/bad-words.txt";
    LogInfo(LogType.Info, $"Downloading base text file...");

    using (WebClient client = new WebClient())
    {
        client.DownloadFile(url, wordlistPath);
    }

    LogInfo(LogType.Info, $"Download complete!");
}

void LogInfo(LogType logType, object data)
{
    var currentColour = Console.ForegroundColor;
    switch (logType)
    {
        case LogType.Info:
            Console.ForegroundColor = ConsoleColor.Cyan;
            break;

        case LogType.Warning:
            Console.ForegroundColor = ConsoleColor.Magenta;
            break;

        case LogType.Error:
            Console.ForegroundColor = ConsoleColor.Red;
            break;
    }

    Console.WriteLine($"[{DateTime.Now.ToString()}] {data}");
    Console.ForegroundColor = currentColour;
}

Entry();

enum LogType
{
    Info,
    Warning,
    Error
}