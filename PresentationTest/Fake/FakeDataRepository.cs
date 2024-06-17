using Service.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationTest;

internal class FakeDataRepository
{
    public Dictionary<string, IUserDTO> Users = new Dictionary<string, IUserDTO>();
    public Dictionary<string, IBookDTO> Books = new Dictionary<string, IBookDTO>();
    public Dictionary<string, IEventDTO> Events = new Dictionary<string, IEventDTO>();
    public Dictionary<string, IStateDTO> States = new Dictionary<string, IStateDTO>();

    // User CRUD
    public async Task AddUserAsync(string id, string email, string phone, string name)
    {
        Users.Add(id, new FakeUserDTO(id, email, phone, name));
    }

    public async Task<IUserDTO> GetUserAsync(string id)
    {
        return await Task.FromResult(Users[id]);
    }

    public async Task UpdateUserAsync(string id, string email, string phone, string name)
    {
        Users[id].Email = email;
        Users[id].Phone = phone;
        Users[id].Name = name;
    }

    public async Task DeleteUserAsync(string id)
    {
        Users.Remove(id);
    }

    public async Task<Dictionary<string, IUserDTO>> GetAllUsersAsync()
    {
        return await Task.FromResult(Users);
    }

    public async Task<string> GetUsersCountAsync()
    {
        return await Task.FromResult(Users.Count.ToString());
    }

    // Book CRUD
    public async Task AddBookAsync(string id, string title, string author, string genre)
    {
        Books.Add(id, new FakeBookDTO(id, title, author, genre));
    }

    public async Task<IBookDTO> GetBookAsync(string id)
    {
        return await Task.FromResult(Books[id]);
    }

    public async Task UpdateBookAsync(string id, string title, string author, string genre)
    {
        Books[id].Title = title;
        Books[id].Author = author;
        Books[id].Genre = genre;
    }

    public async Task DeleteBookAsync(string id)
    {
        Books.Remove(id);
    }

    public async Task<Dictionary<string, IBookDTO>> GetAllBooksAsync()
    {
        return await Task.FromResult(Books);
    }

    public async Task<string> GetBooksCountAsync()
    {
        return await Task.FromResult(Books.Count.ToString());
    }

    // State CRUD
    public async Task AddStateAsync(string id, string bookid, bool availability)
    {
        States.Add(id, new FakeStateDTO(id, bookid, availability));
    }

    public async Task<IStateDTO> GetStateAsync(string id)
    {
        return await Task.FromResult(States[id]);
    }

    public async Task UpdateStateAsync(string id, string bookid, bool availability)
    {
        States[id].BookId = bookid;
        States[id].Availability = availability;
    }

    public async Task DeleteStateAsync(string id)
    {
        States.Remove(id);
    }

    public async Task<Dictionary<string, IStateDTO>> GetAllStatesAsync()
    {
        return await Task.FromResult(States);
    }

    public async Task<string> GetStatesCountAsync()
    {
        return await Task.FromResult(States.Count.ToString());
    }

    // Event CRUD
    public async Task AddEventAsync(string id, string stateid, string customerid, string type)
    {
        Events.Add(id, new FakeEventDTO(id, stateid, customerid, type));
    }

    public async Task<IEventDTO> GetEventAsync(string id)
    {
        return await Task.FromResult(Events[id]);
    }

    public async Task UpdateEventAsync(string id, DateTime eventdate, string stateid, string customerid, string type)
    {
        var eventDto = (FakeEventDTO)Events[id];
        eventDto.EventDate = eventdate;
        eventDto.StateId = stateid;
        eventDto.CustomerId = customerid;
        eventDto.Type = type;
    }

    public async Task DeleteEventAsync(string id)
    {
        Events.Remove(id);
    }

    public async Task<Dictionary<string, IEventDTO>> GetAllEventsAsync()
    {
        return await Task.FromResult(Events);
    }

    public async Task<string> GetEventsCountAsync()
    {
        return await Task.FromResult(Events.Count.ToString());
    }
}
