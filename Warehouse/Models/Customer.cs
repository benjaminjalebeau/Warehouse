namespace Warehouse.Models;
public class Customer
{
    public int Id {get; set;}

    public required string firstName {get; set;}

    public required string lastName {get; set;}

    public required string email {get; set;}

    public required string password {get; set;}
    
}