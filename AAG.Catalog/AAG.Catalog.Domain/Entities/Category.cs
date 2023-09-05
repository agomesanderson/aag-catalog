using AAG.Catalog.Domain.Commands.Input.Categories;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AAG.Catalog.Domain.Entities;

public class Category
{
    #region Props

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    #endregion

    #region Constructors

    public Category()
    {

    }

    #endregion

    #region Factories

    public static Category Create(CreateCategoryCommand command)
    {
        return new Category
        {
            Name = command.Name,
            CreatedAt = DateTime.Now,
        };
    }

    public static Category Update(UpdateCategoryCommand command, string id)
    {
        return new Category
        {
            Id = id,
            Name = command.Name,
            UpdatedAt = DateTime.Now,
        };
    }

    #endregion
}
