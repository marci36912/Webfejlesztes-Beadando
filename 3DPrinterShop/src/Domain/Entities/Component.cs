using PrinterShop.Shared.Enums;

namespace PrinterShop.Core.Domain.Entities;

public class Component
{
    public Guid ModelNumber { get; set; }
    
    public ComponentType ComponentType { get; set; }
    
    public string Brand { get; set; }
    
    public string Model { get; set; }
    
    public float Price { get; set; }

    public string Description { get; set; }
    
    public virtual List<Printer> Printers { get; set; }
    
    public virtual List<Order> Orders { get; set; }
}