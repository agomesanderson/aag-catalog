using AAG.Catalog.Domain.Commands.Input.Products.Validation;
using AAG.Catalog.Domain.Repositories;
using AAG.Catalog.Infra.Common;

namespace AAG.Catalog.Domain.Commands.Input.Products;

public class CreateProductCommand
{
    public string CategoryId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
