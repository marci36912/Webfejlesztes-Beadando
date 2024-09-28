namespace PrinterShop.Shared.Dtos;

public class PrinterDto
{
    public Guid Id { get; set; }
    
    public string Brand { get; set; }
    
    public string Model { get; set; }
    
    public Guid MainBoard { get; set; }
    
    public Guid StepperMotor { get; set; }
    
    public Guid HeatingBed { get; set; }
    
    public Guid BedLevelingSensor { get; set; }
    
    public Guid Extruder { get; set; }
}