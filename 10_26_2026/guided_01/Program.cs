#pragma warning disable IDE0005
using System;
using System.IO;

#region Actual Logic
internal static class Program
{
    internal static void Main()
    {
        OutputPath.EnsureOutputDirectoryExists();
        string fp = OutputPath.For("classlog.txt");
        string date = DateTime.Today.ToString("yyyy-MM-dd");

        File.AppendAllText(fp, $"{date} - Class started" + Environment.NewLine);
        Logger.Info("Successfully updated class logs.");
        Logger.Info($"Hint: Check '{fp}'.");
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

// Input.* omitted.

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