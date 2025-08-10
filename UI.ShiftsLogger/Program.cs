using System.Text.RegularExpressions;
using UI.ShiftsLogger;
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
    static async Task Main()
    {
        bool isAppRunning = true;
        RequestHandler.InitializeClient(); 

        while(isAppRunning)
        {
            var choice = DisplayMainMenu();
            DisplayUtils.ClearScreen();

            switch(choice)
            {
                case MenuOptions.CreateShift:
                    await MenuManager.CreateShift();
                    break;
                
                case MenuOptions.ViewShifts:
                    await MenuManager.ViewShifts();
                    break;
                    
                case MenuOptions.UpdateShift:
                    await MenuManager.UpdateShift();
                    break;
                    
                case MenuOptions.DeleteShift:
                    await MenuManager.DeleteShift();
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