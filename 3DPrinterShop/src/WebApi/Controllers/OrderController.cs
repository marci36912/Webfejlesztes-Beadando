using Microsoft.AspNetCore.Mvc;
using PrinterShop.Core.Application.Interfaces.Services;
using PrinterShop.Core.Domain.Entities;

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
    public async Task<IActionResult> AddAsync([FromBody] Order order)
    {
        await _orderService.AddAsync(order);
        
        return Ok();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Order>> Get(Guid id)
    {
        var result = await _orderService.GetAsync(id);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<List<Order>>> GetAllAsync()
    {
        var result = await _orderService.GetAllAsync();
        
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] Order order)
    {
        await _orderService.UpdateAsync(order);

        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _orderService.DeleteAsync(id);
        
        return Ok();
    }
}