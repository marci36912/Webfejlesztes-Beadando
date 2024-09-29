using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PrinterShop.Core.Application.Interfaces.Services;
using PrinterShop.Shared.Dtos;

namespace PrinterShop.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] OrderDto order)
    {
        try
        {
            await _orderService.AddAsync(order);
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
    public async Task<ActionResult<OrderDto>> GetAsync(Guid id)
    {
        var result = await _orderService.GetAsync(id);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<List<OrderDto>>> GetAllAsync()
    {
        var result = await _orderService.GetAllAsync();
        
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] OrderDto order)
    {
        try
        {
            await _orderService.UpdateAsync(order);
        }
        catch (ArgumentNullException e)
        {
            await AddAsync(order);
            
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
            await _orderService.DeleteAsync(id);
        }
        catch (ArgumentNullException e)
        {
            return NotFound();
        }
        
        return Ok();
    }
}