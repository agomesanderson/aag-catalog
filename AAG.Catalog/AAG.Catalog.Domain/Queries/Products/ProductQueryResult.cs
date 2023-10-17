using AAG.Catalog.Domain.Entities;

namespace AAG.Catalog.Domain.Queries.Products;

public class ProductQueryResult
{
    public string? Id { get; set; }
    public string? CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public static ProductQueryResult? CreateProductQueryResult(Product? product)
    {
        if (product is null)
            return null;

        return new ProductQueryResult
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt,
        };
    }

    public static List<ProductQueryResult>? CreateProductQueryResult(List<Product>? products)
    {
        if (products is null || products.Count is 0)
            return null;

        return products.Select(p =>
            new ProductQueryResult
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt,
            }
        ).ToList();
    }
}
