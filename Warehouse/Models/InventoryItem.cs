using System.ComponentModel.DataAnnotations;
namespace Warehouse.Models;

public class InventoryItem
{
    public int Id {get; set;} // will be auto-incremented. 

    [Required]
    public required string Name {get; set;} // Name of the item

    public string? Description {get; set;} // basic description of the item

    [Required(ErrorMessage = "Please enter a weight in Kg.")]
    [Range(.1, 1000, ErrorMessage = "Please enter a weight between .1 and 1000 kg")]
    public required double Weight {get; set;} // This weight is in kg.

    [Required(ErrorMessage = "Please Enter a Customer Id")]
    public required int CustomerId {get; set;} // id of the customer's account the item belongs to.

    public string? LocationId {get; set;} // Two character Id of the location {A-Z} + {0-9}
    
    [Required(ErrorMessage = "Please enter the date the item was recieved.")]
    [DataType(DataType.Date)]
    public required DateTime ReceivedDate {get; set;} // Day in which inventory item was received by warehouse.

    [DataType(DataType.Date)]
    public DateTime? ShippedDate {get; set;} // Day in which inventory item was received by warehouse.
}