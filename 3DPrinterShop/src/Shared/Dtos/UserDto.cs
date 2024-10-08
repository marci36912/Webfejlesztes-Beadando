﻿using PrinterShop.Shared.Enums;

namespace PrinterShop.Shared.Dtos;

public class UserDto
{
    public Guid Id { get; set; }
    
    public UserType Type { get; set; }
    
    public string UserName { get; set; }
    
    public string Password { get; set; }
    
    public string Email { get; set; }
}