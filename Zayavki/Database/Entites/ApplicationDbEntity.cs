using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Zayavki;

public class ApplicationDbEntity
{
    [BsonId]
    public Guid Id { get; set; }

    public string Abonent { get; set; }
    public string Address { get; set; }
    public string Incident { get; set; }
    public string Duty { get; set; }
    public string Type { get; set; }
    public DateTime UpdatedDate { get; set; }
    public DateTime CreatedDate { get; set; }
}
