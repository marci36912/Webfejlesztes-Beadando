namespace PrinterShop.Core.Domain.Entities;

public class Order
{
    public Guid Id { get; set; }
    
    public Guid PrinterId { get; set; }
    
    public Guid CustomerId { get; set; }
    
    public List<Guid> Components { get; set; }
    
    public virtual Printer Printer { get; set; }
    
    public virtual User Customer { get; set; }
    
    public virtual List<Component> ComponentList { get; set; }
}