using Spectre.Console;

namespace UI.ShiftsLogger.Utilities;

public static class DisplayUtils
{
    public static void ClearScreen()
    {
        AnsiConsole.Clear();
    }

    public static int PromptUserForIndexSelection(string promptText, Dictionary<string, int> choices)
    {
        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title(promptText)
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to see additional options)[/]")
            .AddChoices(choices.Keys.ToArray()));

        return choices[selection];
    }

    public static bool PromptUserForYesOrNoSelection(string promptText)
    {
        Dictionary<string, bool> choices = new Dictionary<string, bool>()
        {
            {"Yes", true},
            {"No", false}
        };

        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title(promptText)
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to see additional options)[/]")
            .AddChoices(choices.Keys.ToArray()));

        return choices[selection];
    }

    public static DateTime PromptUserForDateTime(string promptText)
    {
        DateTime input = DateTime.MinValue;

        AnsiConsole.Prompt(
            new TextPrompt<string>(promptText)
            .Validate(n =>
            {
                if (DateTime.TryParse(n, out input))
                {
                    return ValidationResult.Success();
                }
                else
                {
                    DisplayMessageToUser("Input is not valid. Please enter a date and time (Ex. May 1 2024 7pm).");
                    return ValidationResult.Error();
                }
            }));

        return input;
    }

    public static string PromptUserForStringInput(string promptText)
    {
        var input = AnsiConsole.Prompt(
            new TextPrompt<string>(promptText)
            .Validate(n =>
            {
                if (string.IsNullOrEmpty(n))
                {
                    return ValidationResult.Error();
                }
                else
                {
                    return ValidationResult.Success();
                }
            }));

        return input;
    }

    public static void DisplayMessageToUser(string message)
    {
        AnsiConsole.WriteLine(message);
    }

    public static void PressAnyKeyToContinue()
    {
        Console.Write("Press any key to continue...");
        Console.ReadKey();
    }
}
