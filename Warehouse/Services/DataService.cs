using System.Runtime.CompilerServices;
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
    // _context is the db you will be interacting with."

    /*********Worker CRUD**********/

    /*********Inventory CRUD**********/
    //Returns all inventory items stored in DB
    public async Task<List<InventoryItem>> GetInventoryAsync() => await _context.Inventory.ToListAsync();


    //Returns Inventory item by customer
    public async Task<InventoryItem?> GetInventoryItemByIdAsync(int id)
    {
        return await _context.Inventory.FindAsync(id);
    }


    // Returns a list of items that belong to a single customer.
    public async Task<List<InventoryItem>> GetInventoryByCustomerIdAsync(int customerId)
    {
        return await _context.Inventory
            .Where(item => item.CustomerId == customerId)
            .ToListAsync();
    }


    // Returns a list of items that are stored at a certain location
    public async Task<List<InventoryItem>> GetInventoryByLocationIdAsync(string locationId)
    {
        return await _context.Inventory
            .Where(item => item.LocationId == locationId)
            .ToListAsync();
    }

    
    // Adds a new Item to the DB
    public async Task AddInventoryItemAsync(InventoryItem item)
    {
        _context.Inventory.Add(item);
        await _context.SaveChangesAsync();
    }

    // Updates an inventory items info in the db
    public async Task UpdateInventoryItemAsync(InventoryItem item)
    {
        _context.Inventory.Update(item);
        await _context.SaveChangesAsync();
    }


    //Removes an inventory item from the db.
    public async Task DeleteInventoryItemAsync(int id)
    {   
        var item = await _context.Inventory.FindAsync(id);
        if (item != null)
        {
            _context.Inventory.Remove(item);
            await _context.SaveChangesAsync();
        }
        
    }

    /*********Customer CRUD**********/
}