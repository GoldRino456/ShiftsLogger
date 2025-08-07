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
        var shifts = _shiftsService.GetAllShifts();

        if(shifts == null || shifts.Count <= 0)
        {
            return NoContent();
        }

        return Ok(shifts);
    }

    [HttpGet("{id}")]
    public ActionResult<Shift> GetShiftById(int id)
    {
        var selectedShift = _shiftsService.GetShiftById(id);

        if(selectedShift == null)
        {
            return NotFound();
        }

        return Ok(selectedShift);
    }

    [HttpPost]
    public ActionResult<Shift> CreateShift(ShiftDTO dto)
    {
        return Ok(_shiftsService.CreateShift(dto));
    }

    [HttpPut("{id}")]
    public ActionResult<Shift> UpdateShift(int id, ShiftDTO dto)
    {
        var selectedShift = _shiftsService.GetShiftById(id);

        if (selectedShift == null)
        {
            return NotFound();
        }

        return Ok(_shiftsService.UpdateShift(id, dto));
    }

    [HttpDelete("{id}")]
    public ActionResult<string> DeleteShift(int id)
    {
        var selectedShift = _shiftsService.GetShiftById(id);

        if (selectedShift == null)
        {
            return NotFound();
        }

        return Ok(_shiftsService.DeleteShift(id));
    }
}
