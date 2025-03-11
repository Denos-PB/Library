using api.Interfaces;
using api.Models;
using MongoDB.Driver;

namespace api.Repository
{
    public class BooksRepository : BooksInterfaces
    {
        private readonly IMongoCollection<Books> _booksCollection;

        public BooksRepository(IMongoDatabase database)
        {
            _booksCollection = database.GetCollection<Books>("books");
        }

        public async Task<List<Books>> GetAsync()
        {
            try
            {
                return await _booksCollection.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting books: {ex.Message}");
                throw;
            }
        }

        public async Task<Books> GetByIdAsync(string id)
        {
            try
            {
                return await _booksCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting book by id: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Books>> GetByGenreAsync(string genre)
        {
            try
            {
                return await _booksCollection.Find(x => x.Genre == genre).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting books by genre: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Books>> GetByTitleAsync(string title)
        {
            try
            {
                return await _booksCollection.Find(x => x.Title == title).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting books by title: {ex.Message}");
                throw;
            }
        }

        public async Task CreateAsync(Books book)
        {
            try
            {
                await _booksCollection.InsertOneAsync(book);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating book: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateAsync(string id, Books book)
        {
            try
            {
                var result = await _booksCollection.ReplaceOneAsync(x => x.Id == id, book);
                if (result.ModifiedCount == 0)
                {
                    throw new KeyNotFoundException($"Book with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating book: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteAsync(string id)
        {
            try
            {
                var result = await _booksCollection.DeleteOneAsync(x => x.Id == id);
                if (result.DeletedCount == 0)
                {
                    throw new KeyNotFoundException($"Book with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting book: {ex.Message}");
                throw;
            }
        }

        
    }
}