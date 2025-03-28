// AuthorService.cs
using api.Interfaces;
using api.Models;

namespace api.Services
{
    public class AuthorService
    {
        private readonly AuthorInterfaces _authorRepository;

        public AuthorService(AuthorInterfaces authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<List<Author>> GetAsync() => 
            await _authorRepository.GetAsync();
        public async Task<Author> GetAsync(string id) =>
            await _authorRepository.GetByIdAsync(id);
        public async Task CreateAsync(Author author) =>
            await _authorRepository.CreateAsync(author);
        public async Task UpdateAsync(string id, Author author) =>
            await _authorRepository.UpdateAsync(id, author);
        public async Task RemoveAsync(string id) =>
            await _authorRepository.DeleteAsync(id);        
    }
}