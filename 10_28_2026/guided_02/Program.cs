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
        string[] lines;

        try
        {
            lines = File.ReadAllLines(fp);
        }
        catch (FileNotFoundException)
        {
            Logger.Error($"Please create '{fp}' first before running this program.");
            return;
        }

        for (int i = 0; i < lines.Length; i++)
        {
            Logger.Info($"[Line {i + 1}]: {lines[i]}");
        }

        Logger.Info($"Total number of lines: {lines.Length}.");
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

internal static class Logger
{
    internal static void Info(string message) =>
        Write("INFO", ConsoleColor.Cyan, message);

    // Warn omitted.

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