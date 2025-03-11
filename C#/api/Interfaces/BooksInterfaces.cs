using api.Models;

namespace api.Interfaces
{
    public interface BooksInterfaces
    {
        Task<List<Books>> GetAsync();
        Task<Books> GetByIdAsync(string id);
        Task<List<Books>> GetByGenreAsync(string genre);
        Task<List<Books>> GetByTitleAsync(string title);
        Task CreateAsync(Books book);
        Task UpdateAsync(string id, Books book);
        Task DeleteAsync(string id);
    }
}