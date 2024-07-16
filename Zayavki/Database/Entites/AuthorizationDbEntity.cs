using MongoDB.Bson.Serialization.Attributes;

namespace Zayavki;

public class AuthorizationDbEntity
{
    [BsonId]
    public Guid Id { get; set; }

    public string UserName { get; set; }

    public string EncryptedPassword { get; set; }

    public string Role { get; set; }
}
