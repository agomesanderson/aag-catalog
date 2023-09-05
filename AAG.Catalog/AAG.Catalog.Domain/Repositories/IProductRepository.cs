using AAG.Catalog.Domain.Entities;

namespace AAG.Catalog.Domain.Repositories;

public interface IProductRepository
{
    Task<Product?> Get(string id);
    Task<List<Product>> GetByCatetoryId(string categoryId);
    Task<List<Product>> GetAll();
    Task Insert(Product product);
    Task Update(Product product);
    Task Delete(string id);
}
