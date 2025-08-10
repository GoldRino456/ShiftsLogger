namespace WebAPI.ShiftsLogger.Data
{
    public class ShiftDTO
    {
        public DateTime ClockInTime { get; set; }
        public DateTime ClockOutTime { get; set; }
        public float DurationInHours { get; set; }
    }
}
