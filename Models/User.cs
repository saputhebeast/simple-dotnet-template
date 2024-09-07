using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using UrbanNest.Types;

namespace UrbanNest.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string PasswordSalt { get; set; }
    [BsonRepresentation((BsonType.String))]
    public Role Role { get; set; }
}
