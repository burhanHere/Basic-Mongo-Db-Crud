using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongooCrud.Dtos;
using MongooCrud.Models;

namespace MongooCrud.Services;

public class DbConnectionService(IOptions<MongoDbSettings> mongoDbSettings)
{
    protected readonly IMongoDatabase _connection = new MongoClient(mongoDbSettings.Value.ConnectionString)
                    .GetDatabase(mongoDbSettings.Value.DatabaseName);
}