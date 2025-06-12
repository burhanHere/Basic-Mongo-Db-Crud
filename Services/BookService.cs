using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongooCrud.Dtos;
using MongooCrud.Models;

namespace MongooCrud.Services;

public class BookService : DbConnectionService
{
    private readonly IMongoCollection<Book> _booksCollection;

    public BookService(IOptions<MongoDbSettings> mongoDbSettings) : base(mongoDbSettings)
    {
        _booksCollection = _connection.GetCollection<Book>(mongoDbSettings.Value.CollectionName);
    }

    public async Task<List<Book>> GetAsync() =>
        await _booksCollection.Find(_ => true).ToListAsync();

    public async Task<Book?> GetAsync(string id) =>
        await _booksCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Book newBook) =>
        await _booksCollection.InsertOneAsync(newBook);

    public async Task UpdateAsync(string id, Book updatedBook) =>
        await _booksCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

    public async Task RemoveAsync(string id) =>
        await _booksCollection.DeleteOneAsync(x => x.Id == id);

}