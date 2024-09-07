using Microsoft.Extensions.Options;
using MongoDB.Driver;
using UrbanNest.Configurations;

namespace UrbanNest.Middlewares;

public class MongoDbMiddleware
{
    private readonly IServiceCollection _serviceCollection;
    
    private readonly IConfiguration _configuration;

    public MongoDbMiddleware(IServiceCollection serviceCollection, IConfiguration configuration)
    {
        _serviceCollection = serviceCollection;
        _configuration = configuration;
    }

    public void ConfigureMongoDb()
    {
        _serviceCollection.Configure<MongoDbSettings>(_configuration.GetSection("MongoDbSettings"));

        _serviceCollection.AddSingleton<IMongoClient, MongoClient>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
            return new MongoClient(settings.ConnectionString);
        });

        _serviceCollection.AddScoped(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(settings.DatabaseName);
        });
    }
}
