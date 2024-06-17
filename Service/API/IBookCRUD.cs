using Data.API;
using Service.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.API
{
    public interface IBookCRUD
    {
        static IBookCRUD CreateBookCRUD(IDataRepository? dataRepository = null)
        {
            return new BookCRUD(dataRepository ?? IDataRepository.CreateDatabase());
        }

        Task AddBookAsync(string id, string title, string author, string genre);

        Task<IBookDTO> GetBookAsync(string id);

        Task UpdateBookAsync(string id, string title, string author, string genre);

        Task DeleteBookAsync(string id);

        Task<Dictionary<string, IBookDTO>> GetAllBooksAsync();

        Task<string> GetBooksCountAsync();


    }
}
