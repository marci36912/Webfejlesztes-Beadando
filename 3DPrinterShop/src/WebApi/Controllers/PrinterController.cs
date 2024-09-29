using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PrinterShop.Core.Application.Interfaces.Services;
using PrinterShop.Shared.Dtos;

namespace PrinterShop.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PrinterController : ControllerBase
{
    private readonly IPrinterService _printerService;

    public PrinterController(IPrinterService printerService)
    {
        _printerService = printerService;
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] PrinterDto printer)
    {
        try
        {
            await _printerService.AddAsync(printer);
        }
        catch (ArgumentException e)
        {
            return Conflict(e.Message);
        }
        catch (ValidationException e)
        {
            return Conflict(e.Message);
        }
        
        return Ok();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PrinterDto>> GetAsync(Guid id)
    {
        var result = await _printerService.GetAsync(id);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PrinterDto>>> GetAllAsync()
    {
        var result = await _printerService.GetAllAsync();
        
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] PrinterDto printer)
    {
        try
        {
            await _printerService.UpdateAsync(printer);
        }
        catch (ArgumentNullException e)
        {
            await AddAsync(printer);
            
            return Created();
        }
        catch (ValidationException e)
        {
            return Conflict(e.Message);
        }

        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        try
        {
            await _printerService.DeleteAsync(id);
        }
        catch (ArgumentNullException e)
        {
            return NotFound();
        }
        
        return Ok();
    }
}