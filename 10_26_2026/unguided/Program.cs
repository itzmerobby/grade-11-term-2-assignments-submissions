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

            if (string.IsNullOrWhiteSpace(name))
            {
                Logger.Warn("Student name must not be empty.");
                continue;
            }

            break;
        }

        string status;
        while (true)
        {
            status = Input.Text("Enter attendance status (Present/Absent): ").Trim();

            if (status.Equals("present", StringComparison.InvariantCultureIgnoreCase) ||
                status.Equals("absent", StringComparison.InvariantCultureIgnoreCase))
            {
                status = $"{char.ToUpperInvariant(status[0])}{status[1..].ToLowerInvariant()}";
                break;
            }

            Logger.Warn("Invalid status. Enter only 'Present' or 'Absent'.");
        }

        string date = DateTime.Today.ToString("yyyy-MM-dd");
        string fp = OutputPath.For("attendance.txt");

        File.AppendAllText(
            fp,
            $"{date} - {name}: {status}{Environment.NewLine}"
        );
        Logger.Info("Attendance recorded successfully.");
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

/// <summary>
/// Standardizes console output.
/// </summary>
internal static class Logger
{
    internal static void Info(string message) =>
        Write("INFO", ConsoleColor.Cyan, message);

    internal static void Warn(string message) =>
        Write("WARN", ConsoleColor.Yellow, message);

    private static void Write(string label, ConsoleColor color, string message)
    {
        Console.ForegroundColor = color;
        Console.Write($"[{label}] ");
        Console.ResetColor();
        Console.WriteLine(message);
    }
}
#endregion