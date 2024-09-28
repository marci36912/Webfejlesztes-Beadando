using Shared.Enums;

namespace PrinterShop.Shared.Data.Dtos;

public class ComponentDto
{
    public Guid ModelNumber { get; set; }
    
    public ComponentType ComponentType { get; set; }
    
    public string Brand { get; set; }
    
    public string Model { get; set; }
    
    public float Price { get; set; }

    public string Description { get; set; }
}