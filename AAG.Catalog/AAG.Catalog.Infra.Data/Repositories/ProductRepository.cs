using AAG.Catalog.Domain.Entities;
using AAG.Catalog.Domain.Repositories;
using AAG.Catalog.Infra.CrossCuttings.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AAG.Catalog.Infra.Data.Repositories;

public class ProductRepository : IProductRepository
{
    public readonly AppConfigurations _settings;
    private readonly IMongoCollection<Product> _productCollection;

    public ProductRepository(IOptions<AppConfigurations> _options)
    {
        _settings = _options.Value;

        var mongoClient = new MongoClient(
            _settings.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            _settings.DatabaseName);

        _productCollection = mongoDatabase.GetCollection<Product>(
            "Product");
    }

    public async Task<Product?> Get(string id)
    {
        try
        {
            return await _productCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<List<Product>> GetByCatetoryId(string categoryId)
    {
        try
        {
            return await _productCollection.Find(x => x.CategoryId == categoryId).ToListAsync();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<List<Product>> GetAll()
    {
        try
        {
            return await _productCollection.Find(_ => true).ToListAsync();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task Insert(Product product)
    {
        await _productCollection.InsertOneAsync(product);
    }

    public async Task Update(Product product)
    {
        await _productCollection.ReplaceOneAsync(x => x.Id == product.Id, product);
    }

    public async Task Delete(string id)
    {
        await _productCollection.DeleteOneAsync(x => x.Id == id);
    }
}
