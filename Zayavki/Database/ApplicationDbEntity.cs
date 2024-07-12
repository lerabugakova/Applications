using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Zayavki;

public class ApplicationDbEntity
{
    [BsonId]
    public Guid Id { get; set; }

    public string ApplicationName { get; set; }
    public string ApplicationDescription { get; set; }
    public string ApplicationType { get; set; }
    public string ApplicationPriority { get; set; }
    public string ApplicationAuthor { get; set; }
    public string ApplicationExecutor { get; set; }
    public DateTime UpdatedDate { get; set; }
    public DateTime CreatedDate { get; set; }
}
