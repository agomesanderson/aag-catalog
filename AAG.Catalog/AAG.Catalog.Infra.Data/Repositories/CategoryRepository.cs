using AAG.Catalog.Domain.Entities;
using AAG.Catalog.Domain.Repositories;
using AAG.Catalog.Infra.CrossCuttings.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AAG.Catalog.Infra.Data.Repositories;

public class CategoryRepository : ICategoryRepository
{
    public readonly AppConfigurations _settings;
    private readonly IMongoCollection<Category> _categoryCollection;

    public CategoryRepository(IOptions<AppConfigurations> _options)
    {
        _settings = _options.Value;

        var mongoClient = new MongoClient(
            _settings.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            _settings.DatabaseName);

        _categoryCollection = mongoDatabase.GetCollection<Category>(
            "Category");
    }

    public async Task<Category?> Get(string id)
    {
        try
        {
            return await _categoryCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<List<Category>> GetAll()
    {
        try
        {
            return await _categoryCollection.Find(_ => true).ToListAsync();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task Insert(Category category)
    {
        await _categoryCollection.InsertOneAsync(category);
    }

    public async Task Update(Category category)
    {
        await _categoryCollection.ReplaceOneAsync(x => x.Id == category.Id, category);
    }

    public async Task Delete(string id)
    {
        await _categoryCollection.DeleteOneAsync(x => x.Id == id);
    }
}
