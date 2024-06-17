using Data.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceTests
{
    internal class FakeDataRepository : IDataRepository
    {
        public Dictionary<string, IUser> Users = new Dictionary<string, IUser>();
        public Dictionary<string, IBook> Books = new Dictionary<string, IBook>();
        public Dictionary<string, IState> States = new Dictionary<string, IState>();
        public Dictionary<string, IEvent> Events = new Dictionary<string, IEvent>();

        // User CRUD
        public async Task AddUserAsync(string id, string email, string phone, string name)
        {
            Users.Add(id, new FakeUser { Id = id, Email = email, Phone = phone, Name = name });
            await Task.CompletedTask;
        }

        public async Task<IUser> GetUserAsync(string id)
        {
            return await Task.FromResult(Users.ContainsKey(id) ? Users[id] : null);
        }

        public async Task UpdateUserAsync(string id, string email, string phone, string name)
        {
            if (Users.ContainsKey(id))
            {
                Users[id].Email = email;
                Users[id].Phone = phone;
                Users[id].Name = name;
            }
            await Task.CompletedTask;
        }

        public async Task DeleteUserAsync(string id)
        {
            Users.Remove(id);
            await Task.CompletedTask;
        }

        public async Task<Dictionary<string, IUser>> GetAllUsersAsync()
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
            Books.Add(id, new FakeBook { Id = id, Title = title, Author = author, Genre = genre });
            await Task.CompletedTask;
        }

        public async Task<IBook> GetBookAsync(string id)
        {
            return await Task.FromResult(Books.ContainsKey(id) ? Books[id] : null);
        }

        public async Task UpdateBookAsync(string id, string title, string author, string genre)
        {
            if (Books.ContainsKey(id))
            {
                Books[id].Title = title;
                Books[id].Author = author;
                Books[id].Genre = genre;
            }
            await Task.CompletedTask;
        }

        public async Task DeleteBookAsync(string id)
        {
            Books.Remove(id);
            await Task.CompletedTask;
        }

        public async Task<Dictionary<string, IBook>> GetAllBooksAsync()
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
            if (!Books.ContainsKey(bookid))
                throw new Exception("This book does not exist!");

            States.Add(id, new FakeState { Id = id, BookId = bookid, Availability = availability });
            await Task.CompletedTask;
        }

        public async Task<IState> GetStateAsync(string id)
        {
            return await Task.FromResult(States.ContainsKey(id) ? States[id] : null);
        }

        public async Task UpdateStateAsync(string id, string bookid, bool availability)
        {
            if (States.ContainsKey(id))
            {
                States[id].BookId = bookid;
                States[id].Availability = availability;
            }
            await Task.CompletedTask;
        }

        public async Task DeleteStateAsync(string id)
        {
            States.Remove(id);
            await Task.CompletedTask;
        }

        public async Task<Dictionary<string, IState>> GetAllStatesAsync()
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
            if (!Users.ContainsKey(customerid))
                throw new Exception("This user does not exist!");

            if (!States.ContainsKey(stateid))
                throw new Exception("This state does not exist!");

            Events.Add(id, new FakeEvent { Id = id, StateId = stateid, CustomerId = customerid, Type = type });
            await Task.CompletedTask;
        }

        public async Task<IEvent> GetEventAsync(string id)
        {
            return await Task.FromResult(Events.ContainsKey(id) ? Events[id] : null);
        }

        public async Task UpdateEventAsync(string id, DateTime eventdate, string stateid, string customerid, string type)
        {
            if (Events.ContainsKey(id))
            {
                Events[id].EventDate = eventdate;
                Events[id].StateId = stateid;
                Events[id].CustomerId = customerid;
                Events[id].Type = type;
            }
            await Task.CompletedTask;
        }

        public async Task DeleteEventAsync(string id)
        {
            Events.Remove(id);
            await Task.CompletedTask;
        }

        public async Task<Dictionary<string, IEvent>> GetAllEventsAsync()
        {
            return await Task.FromResult(Events);
        }

        public async Task<string> GetEventsCountAsync()
        {
            return await Task.FromResult(Events.Count.ToString());
        }

        // Utils
        public async Task<bool> CheckIfUserExistsAsync(string id)
        {
            return await Task.FromResult(Users.ContainsKey(id));
        }

        public async Task<bool> CheckIfBookExistsAsync(string id)
        {
            return await Task.FromResult(Books.ContainsKey(id));
        }

        public async Task<bool> CheckIfStateExistsAsync(string id)
        {
            return await Task.FromResult(States.ContainsKey(id));
        }

        public async Task<bool> CheckIfEventExistsAsync(string id)
        {
            return await Task.FromResult(Events.ContainsKey(id));
        }
    }

    // Fake User, Book, State, and Event classes for the FakeDataRepository
    internal class FakeUser : IUser
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
    }

    internal class FakeBook : IBook
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
    }

    internal class FakeState : IState
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string BookId { get; set; }
        public bool Availability { get; set; }
    }

    internal class FakeEvent : IEvent
    {
        public string Id { get; set; }
        public DateTime EventDate { get; set; }
        public string StateId { get; set; }
        public string CustomerId { get; set; }
        public string Type { get; set; }
    }
}
