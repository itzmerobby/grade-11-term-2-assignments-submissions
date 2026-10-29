#pragma warning disable IDE0005
using System;
using System.IO;

#region Actual Logic
internal static class Program
{
    internal static void Main()
    {
        OutputPath.EnsureOutputDirectoryExists();
        string fp = OutputPath.For("announcements.txt");
        string contents;

        try
        {
            contents = File.ReadAllText(fp);
        } catch(FileNotFoundException)
        {
            Logger.Error($"Please create '{fp}' first before running this program.");
            return;
        }

        Logger.Info($"File contents: {contents}");
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

// Input.* has been omitted.

internal static class Logger
{
    internal static void Info(string message) =>
        Write("INFO", ConsoleColor.Cyan, message);

    // Warn method omitted.

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