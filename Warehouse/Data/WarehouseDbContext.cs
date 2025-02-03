using Microsoft.EntityFrameworkCore;
using Warehouse.Models;

namespace Warehouse.Data
{
    public class WarehouseDbContext : DbContext
    {
        public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<InventoryItem> Inventory { get; set; }
        public DbSet<Worker> Workers { get; set; }
    }
}
