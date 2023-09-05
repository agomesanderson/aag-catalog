using AAG.Catalog.Domain.Entities;

namespace AAG.Catalog.Domain.Repositories;

public interface ICategoryRepository
{
    Task<Category?> Get(string id);
    Task<List<Category>> GetAll();
    Task Insert(Category category);
    Task Update(Category category);
    Task Delete(string id);
}
