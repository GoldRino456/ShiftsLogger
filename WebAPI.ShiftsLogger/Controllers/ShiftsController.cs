using Microsoft.AspNetCore.Mvc;
using WebAPI.ShiftsLogger.Data;
using WebAPI.ShiftsLogger.Services;

namespace WebAPI.ShiftsLogger.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShiftsController : ControllerBase
{
    private readonly IShiftsService _shiftsService;
    public ShiftsController(IShiftsService shiftsService)
    {
        _shiftsService = shiftsService;
    }

    [HttpGet]
    public ActionResult<List<Shift>> GetAllShifts()
    {
        return Ok(_shiftsService.GetAllShifts());
    }

    [HttpGet("{id}")]
    public ActionResult<Shift> GetShiftById(int id)
    {
        return Ok(_shiftsService.GetShiftById(id));
    }

    [HttpPost]
    public ActionResult<Shift> CreateShift(Shift shift)
    {
        return Ok(_shiftsService.CreateShift(shift));
    }

    [HttpPut("{id}")]
    public ActionResult<Shift> UpdateShift(int id, Shift shift)
    {
        return Ok(_shiftsService.UpdateShift(id, shift));
    }

    [HttpDelete("{id}")]
    public ActionResult<string> DeleteShift(int id)
    {
        return Ok(_shiftsService.DeleteShift(id));
    }
}
