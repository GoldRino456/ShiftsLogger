namespace UI.ShiftsLogger.Utilities;

public static class ValidationUtils
{
    /// <summary>
    /// Compares two DateTime values and returns True if "a" occurs before "b". If "b" is equal to or earlier than "a", False is returned instead.
    /// </summary>
    /// <param name="a">Start of Time Range</param>
    /// <param name="b">End of Time Range</param>
    /// <returns></returns>
    public static bool ValidateTimeRange(DateTime a, DateTime b)
    {
        return a < b;
    }
}
