namespace PrinterShop.Core.Domain.Entities;

public class Printer
{
    public Guid Id { get; set; }
    
    public string Brand { get; set; }
    
    public string Model { get; set; }
    
    public Guid MainBoard { get; set; }
    
    public Guid StepperMotor { get; set; }
    
    public Guid HeatingBed { get; set; }
    
    public Guid BedLevelingSensor { get; set; }
    
    public Guid Extruder { get; set; }
    
    public virtual List<Order> Orders { get; set; }
}