using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Warehouse.Data;
using Warehouse.Models;

public class DataService
{
    private readonly WarehouseDbContext _context;

    public DataService(WarehouseDbContext context)
    {
        _context = context;
    }

    // Enter any CRUD operations below, 
    // _context is the db you will be interacting with."

    //Checks if an email is already being used by any worker or customer.
    public async Task<bool> IsEmailTakenAsync(string email)
    {
        var checkWorkers = await _context.Workers.AnyAsync(w => w.Email == email);
        var checkCustomers = await _context.Customers.AnyAsync(c => c.Email == email);

        return checkWorkers || checkCustomers;
    }

    /*********Worker CRUD**********/
    // Returns all workers

    // original function
    // I created another one because this one requests an id that is not used in the function

    // public async Task<List<Worker>> GetWorkersAsync(int id)
    // {
    //     return await _context.Workers.ToListAsync();
    // }

    public async Task<List<Worker>> GetWorkersAsync()
    {
        return await _context.Workers.ToListAsync();
    }
    
    // get a worker inf by his/her Id
    public async Task<Worker?> GetWorkerByIdAsync(int id)
    {
        return await _context.Workers.FindAsync(id);
    }

    // Adds a new worker
    public async Task AddWorkerAsync(Worker worker)
    {
        _context.Workers.Add(worker);
        await _context.SaveChangesAsync();
    }

    // Updates a worker's info
    public async Task UpdateWorkerAsync(Worker worker)
    {
        _context.Workers.Update(worker);
        await _context.SaveChangesAsync();
    }

    //Removes a worker
    public async Task DeleteWorkerAsync(int id)
    {
        var worker = await _context.Workers.FindAsync(id);
        if (worker != null)
        {
            _context.Workers.Remove(worker);
            await _context.SaveChangesAsync();
        }
    }

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
    //Gets customer by customer id
    public async Task<Customer?> GetCustomerByIdAsync(int id)
    {
        return await _context.Customers.FindAsync(id);
    }

    //Gets all customers
    public async Task<List<Customer>> GetCustomersAsync() => await _context.Customers.ToListAsync();

    //Adds a new customer
    public async Task AddCustomerAsync(Customer customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

    }

    //Updates a customer's info
    public async Task UpdateCustomerAsync(Customer customer)
    {
        _context.Customers.Update(customer);
        await _context.SaveChangesAsync();
    }

    //Removes a customer from the db
    public async Task RemoveCustomerAsync(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer != null)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }
    }
}