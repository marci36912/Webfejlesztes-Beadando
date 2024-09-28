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
        await _printerService.AddAsync(printer);
        
        return Ok();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PrinterDto>> Get(Guid id)
    {
        var result = await _printerService.GetAsync(id);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<List<PrinterDto>>> GetAllAsync()
    {
        var result = await _printerService.GetAllAsync();
        
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] PrinterDto printer)
    {
        await _printerService.UpdateAsync(printer);

        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _printerService.DeleteAsync(id);
        
        return Ok();
    }
}