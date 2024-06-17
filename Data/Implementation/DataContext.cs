using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Data.API;
using Data.Database;

namespace Data.Implementation
{
    internal class DataContext : IDataContext
    {
        private readonly string _connectionString;

        public DataContext(string? connectionString = null)
        {
            if (connectionString is null)
            {
                string projectRootDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
                string dbRelativePath = @"Data\Database\Database.mdf";
                string dbPath = Path.Combine(projectRootDir, dbRelativePath);
                _connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={dbPath};Integrated Security = True; Connect Timeout = 30;";
            }
            else
            {
                _connectionString = connectionString;
            }
        }

        #region User CRUD

        public async Task AddUserAsync(IUser user)
        {
            using (var context = new DatabaseDataContext(_connectionString))
            {
                var entity = new Database.User()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Phone = user.Phone
                };

                context.Users.InsertOnSubmit(entity);
                await Task.Run(() => context.SubmitChanges());
            }
        }

        public async Task DeleteUserAsync(string id)
        {
            using (var context = new DatabaseDataContext(_connectionString))
            {
                var toDelete = (from u in context.Users where u.Id == id select u).FirstOrDefault();
                if (toDelete != null)
                {
                    context.Users.DeleteOnSubmit(toDelete);
                    await Task.Run(() => context.SubmitChanges());
                }
            }
        }

        public async Task<IUser> GetUserAsync(string id)
        {
            using (var context = new DatabaseDataContext(_connectionString))
            {
                var user = await Task.Run(() => (from u in context.Users where u.Id == id select u).FirstOrDefault());
                return user != null ? new User(user.Name, user.Email, user.Phone) { Id = user.Id } : null;
            }
        }

        public async Task<Dictionary<string, IUser>> GetAllUsersAsync()
        {
            using (var context = new DatabaseDataContext(_connectionString))
            {
                var users = await Task.Run(() => (from u in context.Users select u).ToDictionary(u => u.Id, u => new User(u.Name, u.Email, u.Phone) { Id = u.Id } as IUser));
                return users;
            }
        }

        public async Task<string> GetUsersCountAsync()
        {
            using (var context = new DatabaseDataContext(_connectionString))
            {
                return await Task.Run(() => context.Users.Count().ToString());
            }
        }

        public async Task UpdateUserAsync(IUser user)
        {
            using (var context = new DatabaseDataContext(_connectionString))
            {
                var toUpdate = (from u in context.Users where u.Id == user.Id select u).FirstOrDefault();
                if (toUpdate != null)
                {
                    toUpdate.Name = user.Name;
                    toUpdate.Email = user.Email;
                    toUpdate.Phone = user.Phone;
                    await Task.Run(() => context.SubmitChanges());
                }
            }
        }

        #endregion User CRUD

        #region Book CRUD

        public async Task AddBookAsync(IBook book)
        {
            using (var context = new DatabaseDataContext(_connectionString))
            {
                var entity = new Database.Book()
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    Genre = book.Genre
                };

                context.Books.InsertOnSubmit(entity);
                await Task.Run(() => context.SubmitChanges());
            }
        }

        public async Task DeleteBookAsync(string id)
        {
            using (var context = new DatabaseDataContext(_connectionString))
            {
                var toDelete = (from b in context.Books where b.Id == id select b).FirstOrDefault();
                if (toDelete != null)
                {
                    context.Books.DeleteOnSubmit(toDelete);
                    await Task.Run(() => context.SubmitChanges());
                }
            }
        }

        public async Task<IBook> GetBookAsync(string id)
        {
            using (var context = new DatabaseDataContext(_connectionString))
            {
                var book = await Task.Run(() => (from b in context.Books where b.Id == id select b).FirstOrDefault());
                return book != null ? new Book(book.Title, book.Author, book.Genre) { Id = book.Id } : null;
            }
        }

        public async Task<Dictionary<string, IBook>> GetAllBooksAsync()
        {
            using (var context = new DatabaseDataContext(_connectionString))
            {
                var books = await Task.Run(() => (from b in context.Books select b).ToDictionary(b => b.Id, b => new Book(b.Title, b.Author, b.Genre) { Id = b.Id } as IBook));
                return books;
            }
        }

        public async Task<string> GetBooksCountAsync()
        {
            using (var context = new DatabaseDataContext(_connectionString))
            {
                return await Task.Run(() => context.Books.Count().ToString());
            }
        }

        public async Task UpdateBookAsync(IBook book)
        {
            using (var context = new DatabaseDataContext(_connectionString))
            {
                var toUpdate = (from b in context.Books where b.Id == book.Id select b).FirstOrDefault();
                if (toUpdate != null)
                {
                    toUpdate.Title = book.Title;
                    toUpdate.Author = book.Author;
                    toUpdate.Genre = book.Genre;
                    await Task.Run(() => context.SubmitChanges());
                }
            }
        }

        #endregion Book CRUD

        #region State CRUD

        public async Task AddStateAsync(IState state)
        {
            using (var context = new DatabaseDataContext(_connectionString))
            {
                var entity = new Database.State()
                {
                    Id = state.Id,
                    Date = state.Date,
                    BookId = state.BookId,
                    Availability = state.Availability
                };

                context.States.InsertOnSubmit(entity);
                await Task.Run(() => context.SubmitChanges());
            }
        }

        public async Task UpdateStateAsync(IState state)
        {
            using (var context = new DatabaseDataContext(_connectionString))
            {
                var toUpdate = (from s in context.States where s.Id == state.Id select s).FirstOrDefault();
                if (toUpdate != null)
                {
                    toUpdate.Date = state.Date;
                    toUpdate.BookId = state.BookId;
                    toUpdate.Availability = state.Availability;
                    await Task.Run(() => context.SubmitChanges());
                }
            }
        }

        public async Task DeleteStateAsync(string id)
        {
            using (var context = new DatabaseDataContext(_connectionString))
            {
                var toDelete = (from s in context.States where s.Id == id select s).FirstOrDefault();
                if (toDelete != null)
                {
                    context.States.DeleteOnSubmit(toDelete);
                    await Task.Run(() => context.SubmitChanges());
                }
            }
        }

        public async Task<IState> GetStateAsync(string id)
        {
            using (var context = new DatabaseDataContext(_connectionString))
            {
                IQueryable<Database.State> query = context.States.Where(s => s.Id == id);
                var state = await Task.Run(() => query.FirstOrDefault());
                return state != null ? new State(state.BookId, state.Availability) { Id = state.Id, Date = state.Date } : null;
            }
        }


        public async Task<Dictionary<string, IState>> GetAllStatesAsync()
        {
            using (var context = new DatabaseDataContext(_connectionString))
            {
                var states = await Task.Run(() => (from s in context.States select s).ToDictionary(s => s.Id, s => new State(s.BookId, s.Availability) { Id = s.Id, Date = s.Date } as IState));
                return states;
            }
        }

        public async Task<string> GetStatesCountAsync()
        {
            using (var context = new DatabaseDataContext(_connectionString))
            {
                return await Task.Run(() => context.States.Count().ToString());
            }
        }

        #endregion State CRUD

        #region Event CRUD

        public async Task AddEventAsync(IEvent even)
        {
            using (var context = new DatabaseDataContext(_connectionString))
            {
                var entity = new Database.Event()
                {
                    Id = even.Id,
                    EventDate = even.EventDate,
                    StateId = even.StateId,
                    CustomerId = even.CustomerId,
                    Type = even.Type
                };

                context.Events.InsertOnSubmit(entity);
                await Task.Run(() => context.SubmitChanges());
            }
        }

        public async Task UpdateEventAsync(IEvent even)
        {
            using (var context = new DatabaseDataContext(_connectionString))
            {
                var toUpdate = (from e in context.Events where e.Id == even.Id select e).FirstOrDefault();
                if (toUpdate != null)
                {
                    toUpdate.EventDate = even.EventDate;
                    toUpdate.StateId = even.StateId;
                    toUpdate.CustomerId = even.CustomerId;
                    toUpdate.Type = even.Type;
                    await Task.Run(() => context.SubmitChanges());
                }
            }
        }

        public async Task DeleteEventAsync(string id)
        {
            using (var context = new DatabaseDataContext(_connectionString))
            {
                var toDelete = (from e in context.Events where e.Id == id select e).FirstOrDefault();
                if (toDelete != null)
                {
                    context.Events.DeleteOnSubmit(toDelete);
                    await Task.Run(() => context.SubmitChanges());
                }
            }
        }

        public async Task<IEvent> GetEventAsync(string id)
        {
            using (var context = new DatabaseDataContext(_connectionString))
            {
                var even = await Task.Run(() => (from e in context.Events where e.Id == id select e).FirstOrDefault());
                return even != null ? new Event(even.StateId, even.CustomerId, even.Type) { Id = even.Id, EventDate = even.EventDate } : null;
            }
        }

        public async Task<Dictionary<string, IEvent>> GetAllEventsAsync()
        {
            using (var context = new DatabaseDataContext(_connectionString))
            {
                var events = await Task.Run(() => (from e in context.Events select e).ToDictionary(e => e.Id, e => new Event(e.StateId, e.CustomerId, e.Type) { Id = e.Id, EventDate = e.EventDate } as IEvent));
                return events;
            }
        }

        public async Task<string> GetEventsCountAsync()
        {
            using (var context = new DatabaseDataContext(_connectionString))
            {
                return await Task.Run(() => context.Events.Count().ToString());
            }
        }

        #endregion Event CRUD

        #region Utils

        public async Task<bool> CheckIfUserExistsAsync(string id)
        {
            return (await GetUserAsync(id)) != null;
        }

        public async Task<bool> CheckIfBookExistsAsync(string id)
        {
            return (await GetBookAsync(id)) != null;
        }

        public async Task<bool> CheckIfStateExistsAsync(string id)
        {
            return (await GetStateAsync(id)) != null;
        }

        public async Task<bool> CheckIfEventExistsAsync(string id)
        {
            return (await GetEventAsync(id)) != null;
        }

        #endregion Utils
    }
}
