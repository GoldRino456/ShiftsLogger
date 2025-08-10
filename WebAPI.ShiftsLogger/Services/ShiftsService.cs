using WebAPI.ShiftsLogger.Data;

namespace WebAPI.ShiftsLogger.Services;

public interface IShiftsService
{
    public List<Shift> GetAllShifts();
    public Shift? GetShiftById(int id);
    public Shift CreateShift(ShiftDTO dto);
    public Shift UpdateShift(int id, ShiftDTO dto);
    public string? DeleteShift(int id);
}

public class ShiftsService : IShiftsService
{
    private readonly ShiftsDbContext _dbContext;

    public ShiftsService(ShiftsDbContext context)
    {
        _dbContext = context;
    }

    public Shift CreateShift(ShiftDTO dto)
    {
        var shift = ConvertDtoToShift(dto);
        var savedShift = _dbContext.Shifts.Add(shift);
        _dbContext.SaveChanges();
        return savedShift.Entity;
    }

    public string? DeleteShift(int id)
    {
        var savedShift = _dbContext.Shifts.Find(id);

        if(savedShift == null)
        {
            return null;
        }

        _dbContext.Shifts.Remove(savedShift);
        _dbContext.SaveChanges();

        return $"Successfully deleted shift with id: {id}";
    }

    public List<Shift> GetAllShifts()
    {
        return _dbContext.Shifts.ToList();
    }

    public Shift? GetShiftById(int id)
    {
        Shift savedShift = _dbContext.Shifts.Find(id);
        return savedShift == null ? null : savedShift;
    }

    public Shift UpdateShift(int id, ShiftDTO dto)
    {
        Shift savedShift = _dbContext.Shifts.Find(id);

        if(savedShift == null)
        {
            return null;
        }

        var shift = ConvertDtoToShift(dto);

        shift.Id = savedShift.Id;
        _dbContext.Entry(savedShift).CurrentValues.SetValues(shift);
        _dbContext.SaveChanges();

        return savedShift;
    }

    private Shift ConvertDtoToShift(ShiftDTO dto)
    {
        Shift newShift = new();

        newShift.ClockInTime = dto.ClockInTime;
        newShift.ClockOutTime = dto.ClockOutTime;
        newShift.DurationInHours = dto.DurationInHours;

        return newShift;
    }
}
