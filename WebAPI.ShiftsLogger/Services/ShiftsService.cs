using WebAPI.ShiftsLogger.Data;

namespace WebAPI.ShiftsLogger.Services;

public interface IShiftsService
{
    public List<Shift> GetAllShifts();
    public Shift? GetShiftById(int id);
    public Shift CreateShift(Shift shift);
    public Shift UpdateShift(int id, Shift shift);
    public string? DeleteShift(int id);
}

public class ShiftsService : IShiftsService
{
    private readonly ShiftsDbContext _dbContext;

    public ShiftsService(ShiftsDbContext context)
    {
        _dbContext = context;
    }

    public Shift CreateShift(Shift shift)
    {
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

    public Shift UpdateShift(int id, Shift shift)
    {
        Shift savedShift = _dbContext.Shifts.Find(id);

        if(savedShift == null)
        {
            return null;
        }

        _dbContext.Entry(savedShift).CurrentValues.SetValues(shift);
        _dbContext.SaveChanges();

        return savedShift;
    }
}
