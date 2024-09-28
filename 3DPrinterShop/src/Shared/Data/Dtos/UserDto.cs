using Shared.Enums;

namespace PrinterShop.Shared.Data.Dtos;

public class UserDto
{
    public Guid Id { get; set; }
    
    public UserType Type { get; set; }
    
    public string UserName { get; set; }
}