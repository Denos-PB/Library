// BooksService.cs
using api.Interfaces;
using api.Models;

namespace api.Services
{
    public class BooksService
    {
        private readonly BooksInterfaces _booksRepository;

        public BooksService(BooksInterfaces booksRepository)
        {
            _booksRepository = booksRepository;
        }

        public async Task<List<Books>> GetAsync() => 
            await _booksRepository.GetAsync();

        public async Task<Books> GetAsync(string id) => 
            await _booksRepository.GetByIdAsync(id);

        public async Task<List<Books>> GetByGenreAsync(string genre) => 
            await _booksRepository.GetByGenreAsync(genre);

        public async Task<List<Books>> GetByTitleAsync(string title) => 
            await _booksRepository.GetByTitleAsync(title);

        public async Task CreateAsync(Books book) => 
            await _booksRepository.CreateAsync(book);

        public async Task UpdateAsync(string id, Books book) => 
            await _booksRepository.UpdateAsync(id, book);

        public async Task RemoveAsync(string id) => 
            await _booksRepository.DeleteAsync(id);
    }
}