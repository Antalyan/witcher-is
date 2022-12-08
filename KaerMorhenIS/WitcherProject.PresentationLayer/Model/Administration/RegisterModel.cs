using System.ComponentModel.DataAnnotations;
using System.Configuration;
using Microsoft.AspNetCore.Identity;

namespace WitcherProject.PresentationLayer.Model.Administration;

public class RegisterModel
{
    [Required(ErrorMessage = "Login is required!")]
    public string UserName { get; set; }
    
    [Required]
    [MinLength(6, ErrorMessage = "Password must have at least six characters")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    [Required]
    [MinLength(6, ErrorMessage = "Password must have at least six characters")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Password does not match!")]
    public string PasswordCompare { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    public string? Surname { get; set; }
    
    public string? Cv { get; set; }
    
    public DateTime Birthdate { get; set; }

    public bool IsActive { get; set; } = true;
    
    public string? Error { get; set; }

    
}