using Service.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationTest;

internal class FakeBookCRUD : IBookCRUD
{
    private readonly FakeDataRepository _fakeRepository = new FakeDataRepository();

    public FakeBookCRUD()
    {

    }

    public async Task AddBookAsync(string id, string title, string author, string genre)
    {
        await _fakeRepository.AddBookAsync(id, title, author, genre);
    }

    public async Task<IBookDTO> GetBookAsync(string id)
    {
        return await _fakeRepository.GetBookAsync(id);
    }

    public async Task UpdateBookAsync(string id, string title, string author, string genre)
    {
        await _fakeRepository.UpdateBookAsync(id, title, author, genre);
    }

    public async Task DeleteBookAsync(string id)
    {
        await _fakeRepository.DeleteBookAsync(id);
    }

    public async Task<Dictionary<string, IBookDTO>> GetAllBooksAsync()
    {
        Dictionary<string, IBookDTO> result = new Dictionary<string, IBookDTO>();

        foreach (IBookDTO book in (await _fakeRepository.GetAllBooksAsync()).Values)
        {
            result.Add(book.Id, book);
        }

        return result;
    }

    public async Task<string> GetBooksCountAsync()
    {
        return await _fakeRepository.GetBooksCountAsync();
    }
}
