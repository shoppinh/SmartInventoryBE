using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartInventoryBE.Models;

public class Shipping : BaseEntity
{
    public string ShippingMethod { get; set; } = string.Empty;
    [Column(TypeName = "decimal(10,2)")]
    public decimal ShippingCost { get; set; }
    public DateTime ShippingDate { get; set; }
    public string ShippingStatus { get; set; } = string.Empty;
    public required Order Order { get; set; }

}
