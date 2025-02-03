
namespace Warehouse.Models;

public class InventoryItem
{
    public int Id {get; set;} // will be auto-incremented. 

    public required string Name {get; set;} // Name of the item

    public string? Description {get; set;} // basic description of the item

    public required float Weight {get; set;} // This weight is in kg.

    public required int CustomerId {get; set;} // id of the customer's account the item belongs to.

    public string? LocationId {get; set;} // Two character Id of the location {A-Z} + {0-9}

    public required DateTime ReceivedDate {get; set;} // Day in which inventory item was received by warehouse.

    public DateTime? ShippedDate {get; set;} // Day in which inventory item was received by warehouse.
}