namespace ProductStockManager.Core;
using System.ComponentModel.DataAnnotations;

public class Product
{
    [Key]
    public string ProductId { get; set; }

    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    [Range(0, int.MaxValue)]
    public int StockAvailable { get; set; }

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}
