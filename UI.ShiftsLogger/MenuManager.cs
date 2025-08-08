using UI.ShiftsLogger.Data;
using UI.ShiftsLogger.Utilities;

namespace UI.ShiftsLogger;

public static class MenuManager
{
    public static async Task CreateShift()
    {
        while(true)
        {
            var newShift = PromptUserForNewShift();

            DisplayShiftDetails(newShift);
            if (DisplayUtils.PromptUserForYesOrNoSelection("Are the details above correct?"))
            {
                DisplayUtils.ClearScreen();
                
                if(!await RequestHandler.CreateNewShift(newShift))
                {
                    DisplayUtils.PressAnyKeyToContinue();
                    DisplayUtils.ClearScreen();
                }

                return;
            }
            else
            {
                DisplayUtils.ClearScreen();

                if (DisplayUtils.PromptUserForYesOrNoSelection("Would you like to continue creating a new shift? (Select \"No\" to return to the Main Menu)."))
                {
                    continue;
                }
                else
                {
                    return;
                }
            }
        }
    }

    private static void PromptForShiftDetails(out DateTime clockInTime, out DateTime clockOutTime)
    {
        while (true)
        {
            clockInTime = DisplayUtils.PromptUserForDateTime("Please enter the date and time you clocked in: ");
            clockOutTime = DisplayUtils.PromptUserForDateTime("Please enter the date and time you clocked out: ");

            if (ValidationUtils.ValidateTimeRange(clockInTime, clockOutTime))
            {
                break;
            }
            else
            {
                DisplayUtils.ClearScreen();
                DisplayUtils.DisplayMessageToUser("Clock out time cannot be before or the same as clock in time!");
            }
        }
    }

    public static async Task ViewShifts()
    {
        DisplayUtils.DisplayMessageToUser("Fetching records from database. Please wait...");
        List<Shift>? allShifts = await RequestHandler.ViewAllShifts(); 

        if (allShifts != null)
        {
            DisplayUtils.ClearScreen();
            DisplayShiftsList(allShifts);
        }
        else
        {
            DisplayUtils.DisplayMessageToUser("No shifts to display.");
        }

        DisplayUtils.PressAnyKeyToContinue();
        DisplayUtils.ClearScreen();
    }

    private static void DisplayShiftsList(List<Shift> allShifts)
    {
        int idx = 1;
        foreach (Shift shift in allShifts)
        {
            DisplayUtils.DisplayMessageToUser($"{idx}:"
                + $"\n\tClock-In: {shift.ClockInTime.ToShortDateString()} {shift.ClockInTime.ToShortTimeString()}"
                + $"\n\tClock-Out: {shift.ClockOutTime.ToShortDateString()} {shift.ClockOutTime.ToShortTimeString()}"
                + $"\n\tTotal Hours Worked: {shift.DurationInHours} hrs");

            idx++;
        }
    }

    public static async Task UpdateShift()
    {
        DisplayUtils.DisplayMessageToUser("Fetching records from database. Please wait...");
        List<Shift>? allShifts = await RequestHandler.ViewAllShifts();

        if (allShifts != null)
        {
            DisplayUtils.ClearScreen();
            var selectedShiftIndex = SelectShiftFromList(allShifts, "Please enter a number for the record above you wish to update: ");
            var selectedShift = allShifts[selectedShiftIndex];

            while (true)
            {
                var updatedShift = PromptUserForNewShift();

                DisplayUtils.ClearScreen();
                DisplayUtils.DisplayMessageToUser("Current Shift Details: ");
                DisplayShiftDetails(selectedShift);
                DisplayUtils.DisplayMessageToUser("\n");
                DisplayUtils.DisplayMessageToUser("New Shift Details: ");
                DisplayShiftDetails(updatedShift);
                DisplayUtils.DisplayMessageToUser("\n");

                if (DisplayUtils.PromptUserForYesOrNoSelection("Are the changes shown above correct?"))
                {
                    DisplayUtils.ClearScreen();
                    await RequestHandler.UpdateShift(selectedShift.Id, updatedShift);
                    return;
                }
                else
                {
                    DisplayUtils.ClearScreen();

                    if (DisplayUtils.PromptUserForYesOrNoSelection("Would you like to continue updating this shift? (Select \"No\" to return to the Main Menu)."))
                    {
                        DisplayUtils.ClearScreen();
                        continue;
                    }
                    else
                    {
                        DisplayUtils.ClearScreen();
                        return;
                    }
                }
            }

        }
        else
        {
            DisplayUtils.DisplayMessageToUser("No shifts to update.");
        }

        DisplayUtils.PressAnyKeyToContinue();
        DisplayUtils.ClearScreen();
    }

    private static Shift PromptUserForNewShift()
    {
        PromptForShiftDetails(out DateTime clockInTime, out DateTime clockOutTime);

        Shift newShift = new()
        {
            ClockInTime = clockInTime,
            ClockOutTime = clockOutTime,
            DurationInHours = (float)(clockOutTime - clockInTime).TotalHours
        };

        return newShift;
    }

    private static int SelectShiftFromList(List<Shift> allShifts, string promptMessage)
    {
        while (true)
        {
            DisplayShiftsList(allShifts);
            var selection = DisplayUtils.PromptUserForStringInput(promptMessage);

            //Is valid number within range?
            if (Int32.TryParse(selection, out var indexNum) && indexNum > 0 && indexNum <= allShifts.Count)
            {
                return indexNum - 1;
            }
            else
            {
                DisplayUtils.DisplayMessageToUser("Number entered isn't a valid selection. Please only enter a number from the list above.");
                DisplayUtils.PressAnyKeyToContinue();
                DisplayUtils.ClearScreen();
            }
        }
    }

    public static async Task DeleteShift()
    {
        DisplayUtils.DisplayMessageToUser("Fetching records from database. Please wait...");
        List<Shift>? allShifts = await RequestHandler.ViewAllShifts();

        if (allShifts != null)
        {
            DisplayUtils.ClearScreen();

            while (true)
            {
                var selectedShiftIndex = SelectShiftFromList(allShifts, "Please enter a number for the record above you wish to delete: ");
                var selectedShift = allShifts[selectedShiftIndex];

                DisplayUtils.ClearScreen();
                DisplayUtils.DisplayMessageToUser("Shift Details: ");
                DisplayShiftDetails(selectedShift);
                DisplayUtils.DisplayMessageToUser("\n");

                if (DisplayUtils.PromptUserForYesOrNoSelection("Are you sure you want to delete this shift? (This cannot be undone!)"))
                {
                    DisplayUtils.ClearScreen();
                    await RequestHandler.DeleteShift(selectedShift.Id);
                    return;
                }
                else
                {
                    DisplayUtils.ClearScreen();

                    if (DisplayUtils.PromptUserForYesOrNoSelection("Would you like to select a different shift to delete? (Select \"No\" to return to the Main Menu)."))
                    {
                        DisplayUtils.ClearScreen();
                        continue;
                    }
                    else
                    {
                        DisplayUtils.ClearScreen();
                        return;
                    }
                }
            }

        }
        else
        {
            DisplayUtils.DisplayMessageToUser("No shifts to delete!");
        }

        DisplayUtils.PressAnyKeyToContinue();
        DisplayUtils.ClearScreen();
    }

    private static void DisplayShiftDetails(Shift shift)
    {
        DisplayUtils.DisplayMessageToUser($"Clock-In Time: {shift.ClockInTime}\nClock-Out Time: {shift.ClockOutTime}\nShift Duration: {shift.DurationInHours} Hours");
    }
}
