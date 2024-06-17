using Data.API;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Implementation
{
    internal class DataRepository : IDataRepository
    {
        private readonly IDataContext _context;

        public DataRepository(IDataContext context)
        {
            _context = context;
        }

        // User CRUD
        public async Task AddUserAsync(string id, string email, string phone, string name)
        {
            IUser user = new User(name, email, phone);
            user.Id = id;
            await _context.AddUserAsync(user);
        }

        public async Task<IUser> GetUserAsync(string id)
        {
            IUser user = await _context.GetUserAsync(id);
            if (user == null)
                throw new Exception("This user does not exist!");
            return user;
        }

        public async Task UpdateUserAsync(string id, string email, string phone, string name)
        {
            IUser user = new User(name, email, phone) { Id = id };
            if (!await CheckIfUserExistsAsync(user.Id))
                throw new Exception("This user does not exist");
            await _context.UpdateUserAsync(user);
        }

        public async Task DeleteUserAsync(string id)
        {
            if (!await CheckIfUserExistsAsync(id))
                throw new Exception("This user does not exist");
            await _context.DeleteUserAsync(id);
        }

        public async Task<Dictionary<string, IUser>> GetAllUsersAsync()
        {
            return await _context.GetAllUsersAsync();
        }

        public async Task<string> GetUsersCountAsync()
        {
            return await _context.GetUsersCountAsync();
        }

        // Book CRUD
        public async Task AddBookAsync(string id, string title, string author, string genre)
        {
            IBook book = new Book(title, author, genre) { Id = id };
            await _context.AddBookAsync(book);
        }

        public async Task<IBook> GetBookAsync(string id)
        {
            IBook book = await _context.GetBookAsync(id);
            if (book == null)
                throw new Exception("This book does not exist!");
            return book;
        }

        public async Task UpdateBookAsync(string id, string title, string author, string genre)
        {
            IBook book = new Book(title, author, genre) { Id = id };
            if (!await CheckIfBookExistsAsync(book.Id))
                throw new Exception("This book does not exist");
            await _context.UpdateBookAsync(book);
        }

        public async Task DeleteBookAsync(string id)
        {
            if (!await CheckIfBookExistsAsync(id))
                throw new Exception("This book does not exist");
            await _context.DeleteBookAsync(id);
        }

        public async Task<Dictionary<string, IBook>> GetAllBooksAsync()
        {
            return await _context.GetAllBooksAsync();
        }

        public async Task<string> GetBooksCountAsync()
        {
            return await _context.GetBooksCountAsync();
        }

        // State CRUD
        public async Task AddStateAsync(string id, string bookid, bool availability)
        {
            if (!await CheckIfBookExistsAsync(bookid))
                throw new Exception("This book does not exist!");

            IState state = new State(bookid, availability) { Id = id };
            await _context.AddStateAsync(state);
        }

        public async Task<IState> GetStateAsync(string id)
        {
            IState state = await _context.GetStateAsync(id);
            if (state == null)
                throw new Exception("This state does not exist!");
            return state;
        }

        public async Task UpdateStateAsync(string id, string bookid, bool availability)
        {
            if (!await CheckIfBookExistsAsync(bookid))
                throw new Exception("This book does not exist!");

            IState state = new State(bookid, availability) { Id = id };
            if (!await CheckIfStateExistsAsync(state.Id))
                throw new Exception("This state does not exist");
            await _context.UpdateStateAsync(state);
        }

        public async Task DeleteStateAsync(string id)
        {
            if (!await CheckIfStateExistsAsync(id))
                throw new Exception("This state does not exist");
            await _context.DeleteStateAsync(id);
        }

        public async Task<Dictionary<string, IState>> GetAllStatesAsync()
        {
            return await _context.GetAllStatesAsync();
        }

        public async Task<string> GetStatesCountAsync()
        {
            return await _context.GetStatesCountAsync();
        }

        // Event CRUD
        public async Task AddEventAsync(string id, string stateid, string customerid, string type)
        {
            if (!await CheckIfUserExistsAsync(customerid))
                throw new Exception("This user does not exist!");

            if (!await CheckIfStateExistsAsync(stateid))
                throw new Exception("This state does not exist!");

            IEvent eventObj = new Event(stateid, customerid, type) { Id = id };

            IState state = await GetStateAsync(stateid);
            switch (type)
            {
                case "Borrow":
                    if (!state.Availability)
                        throw new Exception("This book is currently unavailable!");
                    state.Availability = false;
                    await UpdateStateAsync(state.Id, state.BookId, state.Availability);
                    break;

                case "Return":
                    if (state.Availability)
                        throw new Exception("This book is already marked as available!");
                    state.Availability = true;
                    await UpdateStateAsync(state.Id, state.BookId, state.Availability);
                    break;

                default:
                    throw new Exception("This event type does not exist!");
            }

            await _context.AddEventAsync(eventObj);
        }

        public async Task<IEvent> GetEventAsync(string id)
        {
            IEvent eventObj = await _context.GetEventAsync(id);
            if (eventObj == null)
                throw new Exception("This event does not exist!");
            return eventObj;
        }

        public async Task UpdateEventAsync(string id, DateTime eventdate, string stateid, string customerid, string type)
        {
            IEvent eventObj = new Event(stateid, customerid, type) { Id = id, EventDate = eventdate };
            if (!await CheckIfEventExistsAsync(eventObj.Id))
                throw new Exception("This event does not exist");
            await _context.UpdateEventAsync(eventObj);
        }

        public async Task DeleteEventAsync(string id)
        {
            if (!await CheckIfEventExistsAsync(id))
                throw new Exception("This event does not exist");
            await _context.DeleteEventAsync(id);
        }

        public async Task<Dictionary<string, IEvent>> GetAllEventsAsync()
        {
            return await _context.GetAllEventsAsync();
        }

        public async Task<string> GetEventsCountAsync()
        {
            return await _context.GetEventsCountAsync();
        }

        // Utils
        public async Task<bool> CheckIfUserExistsAsync(string id)
        {
            return await _context.CheckIfUserExistsAsync(id);
        }

        public async Task<bool> CheckIfBookExistsAsync(string id)
        {
            return await _context.CheckIfBookExistsAsync(id);
        }

        public async Task<bool> CheckIfStateExistsAsync(string id)
        {
            return await _context.CheckIfStateExistsAsync(id);
        }

        public async Task<bool> CheckIfEventExistsAsync(string id)
        {
            return await _context.CheckIfEventExistsAsync(id);
        }
    }
}
