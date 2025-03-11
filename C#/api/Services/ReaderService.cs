using api.Models;
using MongoDB.Driver;

namespace api.Services
{
    public class ReaderService
    {
        private readonly IMongoCollection<Reader> _readerCollection;

        public ReaderService(IMongoDatabase mongodb)
        {
            _readerCollection = mongodb.GetCollection<Reader>("readers");
        }


        public async Task<List<Reader>> GetAsync()
        {
            try
            {
                return await _readerCollection.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting readers: {ex.Message}");
                throw;
            }
        }

        public async Task<Reader?> GetAsync(string id)
        {
            try
            {
                return await _readerCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting reader: {ex.Message}");
                throw;
            }
        }

        public async Task CreateReaderAsync(Reader reader)
        {
            try
            {
                await _readerCollection.InsertOneAsync(reader);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating reader: {ex.Message}");
                throw;
            }
        }


        public async Task UpdateReaderAsync(string id, Reader reader)
        {
            try
            {
                await _readerCollection.ReplaceOneAsync(x => x.Id == id, reader);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating reader: {ex.Message}");
                throw;
            }
        }

        public async Task RemoveReaderAsync(string id)
        {
            try
            {
                await _readerCollection.DeleteOneAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting reader: {ex.Message}");
                throw;
            }
        }

        public async Task CreateComentAsync(string readerId, string bookId, Coment comment)
        {
            try
            {
                var reader = await GetAsync(readerId);
                if (reader == null)
                {
                    throw new Exception($"Reader with ID '{readerId}' not found");
                }

                if (reader.Coments == null)
                {
                    reader.Coments = new List<Coment>();
                }

                comment.BookId = bookId;
                reader.Coments.Add(comment);
                await UpdateReaderAsync(readerId, reader);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating comment: {ex.Message}");
                throw;
            }
        }

    }
}