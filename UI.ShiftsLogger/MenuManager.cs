using System.Threading.Tasks;
using UI.ShiftsLogger.Data;
using UI.ShiftsLogger.Utilities;

namespace UI.ShiftsLogger;

public static class MenuManager
{
    public static async void CreateShift()
    {
        while(true)
        {
            PromptForShiftDetails(out DateTime clockInTime, out DateTime clockOutTime);

            Shift newShift = new()
            {
                ClockInTime = clockInTime,
                ClockOutTime = clockOutTime,
                DurationInHours = (float)(clockOutTime - clockInTime).TotalHours
            };

            DisplayShiftDetails(newShift);
            if (DisplayUtils.PromptUserForYesOrNoSelection("Are the details above correct?"))
            {
                DisplayUtils.ClearScreen();
                await RequestHandler.CreateNewShift(newShift);
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
        DisplayUtils.ClearScreen();

        if (allShifts != null)
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
        else
        {
            DisplayUtils.DisplayMessageToUser("No shifts to display.");
        }

        DisplayUtils.PressAnyKeyToContinue();
        DisplayUtils.ClearScreen();
    }

    public static void UpdateShift()
    {

    }

    public static void DeleteShift()
    {

    }

    private static void DisplayShiftDetails(Shift shift)
    {
        DisplayUtils.DisplayMessageToUser($"Clock-In Time: {shift.ClockInTime}\nClock-Out Time: {shift.ClockOutTime}\nShift Duration: {shift.DurationInHours} Hours");
    }
}
