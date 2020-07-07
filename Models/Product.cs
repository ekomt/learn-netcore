using System.ComponentModel.DataAnnotations.Schema;

public class ProductDTO
{
    public long Id { get; set; }
    public string Name { get; set; }
    public int Qty { get; set; }
}

public class Product : ProductDTO
{
    [Column(TypeName = "decimal(18,4)")]
    public decimal Price { get; set; }
}