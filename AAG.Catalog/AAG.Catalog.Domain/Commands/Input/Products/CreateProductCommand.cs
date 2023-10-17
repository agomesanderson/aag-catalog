namespace AAG.Catalog.Domain.Commands.Input.Products;

public class CreateProductCommand
{
    public string CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}
