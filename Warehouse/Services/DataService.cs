using Microsoft.EntityFrameworkCore;
using Warehouse.Data;
using Warehouse.Models;

public class DataService{
    private readonly WarehouseDbContext _context;

    public DataService(WarehouseDbContext context)
    {
        _context = context;
    }

    // Enter any CRUD operations below, 
    // _context is the db you will be interacting with. Make sure you finish each function with "_context.SaveChangesAsync();"

    /*********Worker CRUD**********/

    /*********Inventory CRUD**********/

    /*********Customer CRUD**********/
}