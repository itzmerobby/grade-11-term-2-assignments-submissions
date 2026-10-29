#pragma warning disable IDE0005
using System;
using System.IO;

#region Actual Logic
internal static class Program
{
    internal static void Main()
    {
        OutputPath.EnsureOutputDirectoryExists();

        Console.WriteLine("""
        +======================================+
        |        STUDENT NOTES READER          |
        +======================================+
        """.Trim());
        Console.WriteLine();

        string fp = OutputPath.For("study-notes.txt");
        string[] linesOfNotes;

        try
        {
            linesOfNotes = File.ReadAllLines(fp);
        } catch (FileNotFoundException)
        {
            Logger.Error($"Please create '{fp}' first before running this program.");
            return;
        }

        Logger.Info($"Notes: \n  {string.Join("\n  ", linesOfNotes)}");
        Logger.Info($"Line count in notes: {linesOfNotes.Length}");
    }
}
#endregion

#region Shared Utilities
internal static class OutputPath
{
    internal static string OutputFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "quejada",
            "out"
        );

    internal static void EnsureOutputDirectoryExists()
    {
        Directory.CreateDirectory(OutputFolder);
    }

    internal static string For(string fileName) =>
        Path.Combine(
            OutputFolder,
            fileName
        );
}

internal static class Input
{
    internal static string Text(string label)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write(label);
        Console.ForegroundColor = ConsoleColor.Gray;
        string inp = Console.ReadLine() ?? "";
        Console.ResetColor();
        return inp;
    }

    internal static T OfType<T>(string label) where T : IParsable<T>
    {
        return T.Parse(Text(label), provider: null);
    }

    internal static T OfType<T>(string label, Func<string, T> convert)
    {
        return convert(Text(label));
    }
}


internal static class Logger
{
    internal static void Info(string message) =>
        Write("INFO", ConsoleColor.Cyan, message);

    internal static void Warn(string message) =>
        Write("WARN", ConsoleColor.Yellow, message);

    internal static void Error(string message) =>
        Write("ERROR", ConsoleColor.Red, message);

    private static void Write(string label, ConsoleColor color, string message)
    {
        Console.ForegroundColor = color;
        Console.Write($"[{label}] ");
        Console.ResetColor();
        Console.WriteLine(message);
    }
}
#endregion