using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Zayavki;

public class ApplicationRepository : IApplicationRepository
{
    private readonly IMongoCollection<ApplicationDbEntity> _applications;

    public ApplicationRepository(IOptions<ApplicationDatabaseSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        var database = client.GetDatabase(settings.Value.DatabaseName);

        _applications = database.GetCollection<ApplicationDbEntity>(settings.Value.ApplicationCollectionName);
    }

    public List<ApplicationDbEntity> Get() =>
        _applications.Find(application => true).ToList();

    public async Task<ApplicationDbEntity> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _applications
            .Find(Builders<ApplicationDbEntity>.Filter.Eq("_id", id ))
            .FirstOrDefaultAsync();
        return result;
    }

    public async Task<ApplicationDbEntity> CreateAsync(ApplicationDbEntity application, CancellationToken cancellationToken = default)
    {
        await _applications.InsertOneAsync(application, cancellationToken: cancellationToken);
        return application;
    }

    public void Update(Guid id, ApplicationDbEntity applicationIn) =>
        _applications.ReplaceOne(application => application.Id == id, applicationIn);

    public void Remove(ApplicationDbEntity applicationIn) =>
        _applications.DeleteOne(application => application.Id == applicationIn.Id);

    public void Remove(Guid id) => 
        _applications.DeleteOne(application => application.Id == id);
}
