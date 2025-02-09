using System.ComponentModel.DataAnnotations;
namespace Warehouse.Models;

public class Worker
{
    public int Id {get; set;}

    [Required(ErrorMessage = "Please enter your full name.")]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "Your full name must have at least 3 characters.")]
    public required string Name {get; set;}

    [Required(ErrorMessage = "Please enter a valid Email.")]
    [EmailAddress(ErrorMessage ="Invalid email format.")]
    public required string Email {get; set;}

    [Required(ErrorMessage = "Please enter a password.")]
    [StringLength(20, MinimumLength = 8, ErrorMessage = "Password must be 8 characters long.")]
    public required string Password {get; set;}
}