////using System.CommandLine;
////using System.ComponentModel.Design;


////List<string> alllanguage = new List<string>{ "html", "c#", "pyton", "js", "ts", "java" };
////var bandelOption = new Option<FileInfo>("--outpot", "File path and name");
////var bandlerCommand = new Command("bundle", "bundle code files into a single file");
////var languageOption = new Option<List<string>>("--language", "the language that you whant");
////bandlerCommand.SetHandler((outpot) =>
////{
////    try { File.Create(outpot.FullName);
////        Console.WriteLine("create file");
////    }
////    catch(DirectoryNotFoundException )
////    {
////        Console.WriteLine("bundle command executed");
////    }


////}, bandelOption);
////bandlerCommand.SetHandler((language) =>
////{
////if (language.All((item) => { return alllanguage.Contains(item); }))
////    {
////        Console.WriteLine("ok");
////    }
////    else{
////        Console.WriteLine("html", "c#", "pyton", "js", "ts", "java");
////    }


////}, languageOption);
////bandlerCommand.AddOption(bandelOption);
////bandlerCommand.AddOption(languageOption);
////var rootCommand = new RootCommand("root command for file bundler");
////rootCommand.AddCommand(bandlerCommand);

////// הפעל את הפקודה
////rootCommand.InvokeAsync(args);
//using System.CommandLine;
//using System.CommandLine.Invocation;
//using System.IO;
//using System.Collections.Generic;
//using System.Linq;

//var alllanguage = new List<string> { "html", "c#", "python", "js", "ts", "java" };

//var bandelOption = new Option<FileInfo>("--output", "File path and name");
//var bandlerCommand = new Command("bundle", "bundle code files into a single file");

//var languageCommand = new Option<List<string>>("--language", "the languages you want")
//{
//    Arity = ArgumentArity.OneOrMore // מאפשר לפחות ערך אחד
//};

//bandlerCommand.SetHandler((outpot, language) =>
//{
//    try
//    {
//        File.Create(outpot.FullName).Dispose(); // סגור את הזרם מיד לאחר יצירת הקובץ
//        Console.WriteLine("File created: " + outpot.FullName);
//    }
//    catch (DirectoryNotFoundException)
//    {
//        Console.WriteLine("Directory not found.");
//    }

//    if (language.All(item => alllanguage.Contains(item)))
//    {
//        Console.WriteLine("Languages are valid: " + string.Join(", ", language));
//    }
//    else
//    {
//        Console.WriteLine("Invalid languages. Available options are: " + string.Join(", ", alllanguage));
//    }
//}, bandelOption, languageCommand);

//bandlerCommand.AddOption(bandelOption);
//bandlerCommand.AddOption(languageCommand);

//var rootCommand = new RootCommand("root command for file bundler");
//rootCommand.AddCommand(bandlerCommand);

//// הפעל את הפקודה
//await rootCommand.InvokeAsync(args);

using System.CommandLine;
using System.CommandLine;
using System.CommandLine.Invocation;
List<string> alllanguage = new List<string> { ".txt", ".pdf", "docx", "xlsx", "js", "h", "cpp",".cs","all" };
string[] excludedDirs = { "bin", "debug", ".csproj", ".sln" };
var bundleOptionOutput = new Option<FileInfo>("--output", "File path");
bundleOptionOutput.AddAlias("-o");
var bundleOptionLanguage = new Option<string>("--language", "File path");
bundleOptionLanguage.AddAlias("-l");
var bundleOptionSort = new Option<bool>("--sort", "to sort by AB eter true ");
bundleOptionSort.AddAlias("-s");
var bundleOptionAutor = new Option<string>("--autor", "to write autor ");
bundleOptionSort.AddAlias("-a");
var bundleOptionRemoveEmpty = new Option<bool>("--remove-empty-lines", "to remove-empty-lines in the code enter true ");
var bundleCommand = new Command("bundle", "bundle files to a single file");
var bundleCommandRsp = new Command("create-rsp", "create rsp file");
bundleCommand.AddOption(bundleOptionLanguage);

bundleCommand.AddOption(bundleOptionSort);
bundleCommand.AddOption(bundleOptionRemoveEmpty);
bundleCommand.AddOption(bundleOptionAutor);
bundleCommand.AddOption(bundleOptionOutput);
bundleCommand.SetHandler((output, language, isSort, removeEmptyLines, autor) =>
{
    try
    {
        // קבל את הנתיב לתיקיה הנוכחית
        string currentDirectory = Directory.GetCurrentDirectory();
        string sourceDirectory = Directory.GetCurrentDirectory();
        // קבל את כל הקבצים בתיקיה הנוכחית
        string[] files = Directory.GetFiles(currentDirectory);
        files = Directory.GetFiles(sourceDirectory, "*.*", SearchOption.AllDirectories)
               .Where(file => !excludedDirs.Any(dir => file.Contains(Path.Combine(sourceDirectory, dir))))
               .ToArray();
        Console.WriteLine("Languages are valid: " + string.Join(", ", language));
        var languageList = language.Split(',').Select(l => l.Trim()).ToList();
        if (languageList.All(item => alllanguage.Contains(item)))
        {
        }
        else
        {
            Console.WriteLine("Invalid languages. Available options are: " + string.Join(", ", alllanguage));
        }

        // הצג את רשימת הקבצים
        Console.WriteLine("Files in the current directory:");
        foreach (string file in files)
        {
            Console.WriteLine(Path.GetFileName(file)); // הצג רק את שם הקובץ
        }
        if(isSort)
        Array.Sort(files);

        using (StreamWriter writer = new StreamWriter(output.FullName))
        {
            writer.WriteLine(autor);
            foreach (string file in files)
            {
                if (languageList.Exists(ext => file.EndsWith(ext)))
                {
                    string content = File.ReadAllText(file);
            // חלק את התוכן לשורות
            string[] lines = content.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            foreach (var line in lines)
            {
                // בדוק אם השורה אינה ריקה ואינה מכילה רווחים
                if (!string.IsNullOrWhiteSpace(line) && !line.Contains(" "))
                {
                    writer.WriteLine(line); // כתוב את השורה לקובץ החדש
                }
            }
                }
            }
        }

   
        Console.WriteLine($"Sort: {isSort}");
        Console.WriteLine($"Remove empty lines: {removeEmptyLines}");
        Console.WriteLine($"Author: {autor}");
        Console.WriteLine("File created: " + output.FullName);
    }
    catch (DirectoryNotFoundException)
    {
        Console.WriteLine("Directory not found.");
    }

}, bundleOptionOutput, bundleOptionLanguage, bundleOptionSort, bundleOptionRemoveEmpty, bundleOptionAutor);
bundleCommandRsp.SetHandler(() =>
{
    Console.WriteLine("nitov");
    string p = Console.ReadLine();
    Console.WriteLine("language \"html\", \"c#\", \"pyton\", \"js\", \"ts\", \"java\" if you whant ");
    string l = Console.ReadLine();
    Console.WriteLine("if you whant remove enter y");
    char s = Console.ReadLine()[0];
    Console.WriteLine("if you whant sort by AB enter y");
char a = Console.ReadLine()[0];
Console.WriteLine("name");
string n = Console.ReadLine();
using (StreamWriter writer = new StreamWriter("filleName.rsp"))
{
    writer.WriteLine("bundle ");
    writer.WriteLine("--output" );
        writer.WriteLine(p);
   writer.WriteLine("--language ");
        writer.WriteLine(l);
   bool b = s == 'y' ? true : false;
        writer.WriteLine("--remove-empty-lines ");
         writer.WriteLine( b);
    b = a == 'y' ? true : false;
        writer.WriteLine("--sort ");
        writer.WriteLine(b);
        writer.WriteLine("--autor ");
         writer.WriteLine( n);
}

});
//bundleCommandRsp.SetHandler(() =>
//{
//    try
//    {
//        Console.WriteLine("Enter output path for the response file:");
//        string outputPath = Console.ReadLine();

//        // בדוק אם התיקייה קיימת ואם לא, צור אותה
//        string directoryPath = Path.GetDirectoryName(outputPath);
//        if (!Directory.Exists(directoryPath))
//        {
//            Directory.CreateDirectory(directoryPath);
//        }

//        Console.WriteLine("Enter languages (\"html\", \"c#\", \"python\", \"js\", \"ts\", \"java\"): ");
//        string languages = Console.ReadLine();

//        Console.WriteLine("If you want to remove empty lines, enter 'y': ");
//        char removeEmptyLinesInput = Console.ReadLine()[0];

//        Console.WriteLine("If you want to sort by AB, enter 'y': ");
//        char sortInput = Console.ReadLine()[0];

//        Console.WriteLine("Enter author name: ");
//        string authorName = Console.ReadLine();

//        // יצירת הקובץ עם StreamWriter
//        using (StreamWriter writer = new StreamWriter(outputPath))
//        {
//            writer.WriteLine("fb bundle ");
//            writer.WriteLine("--output " + outputPath);
//            writer.WriteLine("--language " + languages);
//            bool removeEmptyLines = removeEmptyLinesInput == 'y';
//            writer.WriteLine("--remove-empty-lines " + removeEmptyLines);
//            bool sort = sortInput == 'y';
//            writer.WriteLine("--sort " + sort);
//            writer.WriteLine("--autor " + authorName);
//        }

//        Console.WriteLine("Response file created successfully at: " + outputPath);
//    }
//    catch (DirectoryNotFoundException)
//    {
//        Console.WriteLine("Directory not found. Please check the output path.");
//    }
//    catch (IOException ex)
//    {
//        Console.WriteLine($"Error creating file: {ex.Message}");
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine($"An unexpected error occurred: {ex.Message}");
//    }
//});

var rootCommand = new RootCommand("root command for file bundle cli");
rootCommand.AddCommand(bundleCommand);
rootCommand.AddCommand(bundleCommandRsp);
rootCommand.InvokeAsync(args);

