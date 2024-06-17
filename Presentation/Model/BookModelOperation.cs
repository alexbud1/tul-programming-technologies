using Service.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Presentation.Model
{


    public class BookModelOperation
    {
        public static BookModelOperation CreateModelOperation(IBookCRUD? bookCrud = null)
        {
            return new BookModelOperation(bookCrud ?? IBookCRUD.CreateBookCRUD());
        }


        private IBookCRUD bookCRUD;

        public BookModelOperation(IBookCRUD? bookCrud = null)
        {
            this.bookCRUD = bookCrud ?? IBookCRUD.CreateBookCRUD();
        }

        private BookModel Map(IBookDTO book)
        {
            return new BookModel(book.Id, book.Title, book.Author, book.Genre);
        }

        public async Task AddAsync(string id, string title, string author, string genre)
        {
            await this.bookCRUD.AddBookAsync(id, title, author, genre);
        }

        public async Task<BookModel> GetAsync(string id)
        {
            return this.Map(await this.bookCRUD.GetBookAsync(id));
        }

        public async Task UpdateAsync(string id, string title, string author, string genre)
        {
            await this.bookCRUD.UpdateBookAsync(id, title, author, genre);
        }

        public async Task DeleteAsync(string id)
        {
            await this.bookCRUD.DeleteBookAsync(id);
        }

        public async Task<Dictionary<string, BookModel>> GetAllAsync()
        {
            Dictionary<string, BookModel> result = new Dictionary<string, BookModel>();

            foreach (IBookDTO book in (await this.bookCRUD.GetAllBooksAsync()).Values)
            {
                result.Add(book.Id, this.Map(book));
            }

            return result;
        }

        public async Task<string> GetCountAsync()
        {
            return await this.bookCRUD.GetBooksCountAsync();
        }
    }
}
