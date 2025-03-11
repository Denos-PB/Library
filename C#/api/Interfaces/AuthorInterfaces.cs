using api.Models;

namespace api.Interfaces
{
    public interface AuthorInterfaces
    {
        Task<List<Author>> GetAsync();
        Task<Author> GetByIdAsync(string id);
        Task CreateAsync(Author author);
        Task UpdateAsync(string id, Author author);
        Task DeleteAsync(string id);
    }
}