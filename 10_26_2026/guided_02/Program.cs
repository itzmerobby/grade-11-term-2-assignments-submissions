#pragma warning disable IDE0005

using System;
using System.IO;

#region Actual Logic
internal static class Program
{
    internal static void Main()
    {
        OutputPath.EnsureOutputDirectoryExists();

        string name;
        while (true)
        {
            name = Input.Text("Enter student name: ").Trim();

            if (name == "")
            {
                Logger.Error("Student name cannot be empty.");
                continue;
            }

            break;
        }
        
        
        string activity;
        while (true)
        {
            activity = Input.Text("Enter activity: ").Trim();

            if (activity == "")
            {
                Logger.Error("Activity cannot be empty.");
                continue;
            }

            break;
        }

        string ts = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string fp = OutputPath.For("activities.txt");
        
        File.AppendAllText(fp, $"{ts} - {name}: {activity}.{Environment.NewLine}");
        Logger.Info("Activity added successfully.");
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

    // OfType omitted.
}

internal static class Logger
{
    internal static void Info(string message) =>
        Write("INFO", ConsoleColor.Cyan, message);

    internal static void Error(string message) =>
    Write("ERROR", ConsoleColor.Red, message);

    // Warn omitted.

    private static void Write(string label, ConsoleColor color, string message)
    {
        Console.ForegroundColor = color;
        Console.Write($"[{label}] ");
        Console.ResetColor();
        Console.WriteLine(message);
    }
}
#endregion