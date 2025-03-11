using api.Interfaces;
using api.Models;
using MongoDB.Driver;

namespace api.Repository
{
    public class AuthorRepository : AuthorInterfaces
    {
        private readonly IMongoCollection<Author> _authorsCollection;

        public AuthorRepository(IMongoDatabase database)
        {
            _authorsCollection = database.GetCollection<Author>("authors");
        }

        public async Task<List<Author>> GetAsync()
        {
            try
            {
                return await _authorsCollection.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting authors: {ex.Message}");
                throw;
            }
        }

        public async Task<Author> GetByIdAsync(string id)
        {
            try
            {
                return await _authorsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting author: {ex.Message}");
                throw;
            }
        }

        public async Task CreateAsync(Author author)
        {
            try
            {
                await _authorsCollection.InsertOneAsync(author);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating author: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateAsync(string id, Author author)
        {
            try
            {
                var result = await _authorsCollection.ReplaceOneAsync(x => x.Id == id, author);
                if (result.ModifiedCount == 0)
                {
                    throw new KeyNotFoundException($"Author with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating author: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteAsync(string id)
        {
            try
            {
                var result = await _authorsCollection.DeleteOneAsync(x => x.Id == id);
                if (result.DeletedCount == 0)
                {
                    throw new KeyNotFoundException($"Author with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting author: {ex.Message}");
                throw;
            }
        }
    }
}