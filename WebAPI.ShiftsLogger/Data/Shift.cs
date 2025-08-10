namespace WebAPI.ShiftsLogger.Data;

public class Shift
{
    public int Id { get; set; }
    public DateTime ClockInTime { get; set; }
    public DateTime ClockOutTime { get; set; }
    public float DurationInHours { get; set; }
}
