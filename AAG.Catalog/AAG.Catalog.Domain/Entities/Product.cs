using AAG.Catalog.Domain.Commands.Input.Products;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace AAG.Catalog.Domain.Entities;

public class Product
{
    #region Props

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string? CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    #endregion

    #region Constructors

    public Product()
    {

    }

    #endregion

    #region Factories

    public static Product Create(CreateProductCommand command)
    {
        return new Product
        {
            CategoryId = command.CategoryId,
            Name = command.Name,
            Description = command.Description,
            Price = command.Price,
            CreatedAt = DateTime.Now,
        };
    }

    public static Product Update(UpdateProductCommand command, string id)
    {
        return new Product
        {
            Id = id,
            CategoryId = command.CategoryId,
            Name = command.Name,
            Description = command.Description,
            Price = command.Price,
            UpdatedAt = DateTime.Now,
        };
    }

    #endregion
}
