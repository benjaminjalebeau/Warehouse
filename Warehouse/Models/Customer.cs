using System.ComponentModel.DataAnnotations;
namespace Warehouse.Models;
public class Customer
{
    public int Id {get; set;}

    [Required(ErrorMessage = "Please enter your first name.")]
    public required string FirstName {get; set;}

    [Required(ErrorMessage = "Please enter your last name.")]
    public required string LastName {get; set;}

    [Required(ErrorMessage = "Please enter a valid Email.")]
    [EmailAddress(ErrorMessage ="Invalid email format.")]
    public required string Email {get; set;}

    [Required(ErrorMessage = "Please enter a password.")]
    [StringLength(20, MinimumLength = 8, ErrorMessage = "Password must be 8 characters long.")]
    public required string Password {get; set;}
    
}