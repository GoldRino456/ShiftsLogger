namespace WebAPI.ShiftsLogger.Data;

public class Shift
{
    public int Id { get; set; }
    public DateTime clockInTime { get; set; }
    public DateTime clockOutTime { get; set; }
    public float durationInHours { get; set; }
}
