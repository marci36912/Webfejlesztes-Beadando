using Shared.Enums;

namespace PrinterShop.Core.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    
    public UserType Type { get; set; }
    
    public string UserName { get; set; }
    
    public string Email { get; set; }
    
    public string Password { get; set; }
    
    public virtual List<Order> Orders { get; set; }
}