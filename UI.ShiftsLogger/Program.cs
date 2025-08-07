using System.Text.RegularExpressions;
using UI.ShiftsLogger.Utilities;

public enum MenuOptions
{
    CreateShift,
    ViewShifts,
    UpdateShift,
    DeleteShift,
    Quit
}

class ShiftsLoggerUI
{
    static void Main()
    {
        bool isAppRunning = true;

        while(isAppRunning)
        {
            var choice = DisplayMainMenu();

            switch(choice)
            {
                case MenuOptions.CreateShift:
                    break;
                
                case MenuOptions.ViewShifts:
                    break;
                    
                case MenuOptions.UpdateShift:
                    break;
                    
                case MenuOptions.DeleteShift:
                    break;
                    
                case MenuOptions.Quit:
                    isAppRunning = false;
                    DisplayUtils.DisplayMessageToUser("Closing application...");
                    break;
            }
        }
    }

    private static MenuOptions DisplayMainMenu()
    {
        Dictionary<string, int> menuOptionPairs = new();
        foreach (var option in Enum.GetValues(typeof(MenuOptions)))
        {
            var displayText = Regex.Replace(option.ToString(), "(\\B[A-Z])", " $1"); //https://stackoverflow.com/questions/5796383/insert-spaces-between-words-on-a-camel-cased-token
            menuOptionPairs.Add(displayText, (int)option);
        }

        return (MenuOptions)DisplayUtils.PromptUserForIndexSelection("What Would You Like To Do?", menuOptionPairs);
    }
}