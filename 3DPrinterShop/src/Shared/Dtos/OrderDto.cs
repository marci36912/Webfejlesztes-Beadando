namespace PrinterShop.Shared.Dtos;

public class OrderDto
{
    public Guid Id { get; set; }
    
    public Guid PrinterId { get; set; }
    
    public Guid CustomerId { get; set; }
    
    public List<Guid> Components { get; set; }
}