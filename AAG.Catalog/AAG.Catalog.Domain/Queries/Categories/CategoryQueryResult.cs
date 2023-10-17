using AAG.Catalog.Domain.Entities;

namespace AAG.Catalog.Domain.Queries.Categories;

public class CategoryQueryResult
{
    public string? Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public static CategoryQueryResult? CreateCategoryQueryResult(Category? category)
    {
        if (category is null)
            return null;

        return new CategoryQueryResult
        {
            Id = category.Id,
            Name = category.Name,
            CreatedAt = category.CreatedAt,
            UpdatedAt = category.UpdatedAt,
        };
    }

    public static List<CategoryQueryResult>? CreateCategoryQueryResult(List<Category>? categories)
    {
        if (categories is null || categories.Count is 0)
            return null;

        return categories.Select(c => 
            new CategoryQueryResult
            {
                Id = c.Id,
                Name = c.Name,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
            }
        ).ToList();
    }
}
