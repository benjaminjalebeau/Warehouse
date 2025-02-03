namespace Warehouse.Models;

public class Worker
{
    public int Id {get; set;}

    public required string name {get; set;}

    public required string email {get; set;}

    public required string password {get; set;}
}