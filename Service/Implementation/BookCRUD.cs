using Data.API;
using Service.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    internal class BookCRUD : IBookCRUD
    {
        private IDataRepository dataRepository;

        public BookCRUD(IDataRepository dataRepository)
        {
            this.dataRepository = dataRepository;
        }

        private IBookDTO Map(IBook book)
        {
            return new BookDTO(book.Id, book.Title, book.Author, book.Genre);
        }

        public async Task AddBookAsync(string id, string title, string author, string genre)
        {
            await this.dataRepository.AddBookAsync(id, title, author, genre);
        }

        public async Task<IBookDTO> GetBookAsync(string id)
        {
            return this.Map(await this.dataRepository.GetBookAsync(id));
        }

        public async Task UpdateBookAsync(string id, string title, string author, string genre)
        {
            await this.dataRepository.UpdateBookAsync(id, title, author, genre);
        }

        public async Task DeleteBookAsync(string id)
        {
            await this.dataRepository.DeleteBookAsync(id);
        }

        public async Task<Dictionary<string, IBookDTO>> GetAllBooksAsync()
        {
            Dictionary<string, IBookDTO> result = new Dictionary<string, IBookDTO>();

            foreach (IBook book in (await this.dataRepository.GetAllBooksAsync()).Values)
            {
                result.Add(book.Id, this.Map(book));
            }

            return result;
        }

        public async Task<string> GetBooksCountAsync()
        {
            return await this.dataRepository.GetBooksCountAsync();
        }


    }
}
