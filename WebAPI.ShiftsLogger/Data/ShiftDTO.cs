namespace WebAPI.ShiftsLogger.Data
{
    public class ShiftDTO
    {
        public DateTime clockInTime { get; set; }
        public DateTime clockOutTime { get; set; }
        public float durationInHours { get; set; }
    }
}
