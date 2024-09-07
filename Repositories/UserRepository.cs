using MongoDB.Driver;
using UrbanNest.Models;

namespace UrbanNest.Repositories;

public class UserRepository
{
    private readonly IMongoCollection<User> _usersCollection;

    public UserRepository(IMongoDatabase database)
    {
        _usersCollection = database.GetCollection<User>("Users");
    }

    public async Task save(User user)
    {
        await _usersCollection.InsertOneAsync(user);
    }
}