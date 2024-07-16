using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Zayavki;

public class AuthorizationRepository : IAuthorizationRepository
{
    private readonly IMongoCollection<AuthorizationDbEntity> _collection;

    public AuthorizationRepository(IOptions<AuthorizationDatabaseSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        var database = client.GetDatabase(settings.Value.DatabaseName);
        _collection = database.GetCollection<AuthorizationDbEntity>(settings.Value.CollectionName);
    }

    public async Task<AuthorizationDbEntity> GetUser(string userName, string encryptedPassword, CancellationToken cancellationToken = default)
    {
        var filter = Builders<AuthorizationDbEntity>.Filter.Eq(x => x.UserName, userName) & Builders<AuthorizationDbEntity>.Filter.Eq(x => x.EncryptedPassword, encryptedPassword);
        return await _collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }
}
