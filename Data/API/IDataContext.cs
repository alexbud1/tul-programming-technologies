using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Data.API
{
    public interface IDataContext
    {
        static IDataContext CreateContext(string? connectionString = null)
        {
            return new Data.Implementation.DataContext(connectionString);
        }


        // Customer CRUD
        Task AddUserAsync(IUser user);
        Task DeleteUserAsync(string id);
        Task<IUser> GetUserAsync(string id);
        Task<Dictionary<string, IUser>> GetAllUsersAsync();
        Task<string> GetUsersCountAsync();
        Task UpdateUserAsync(IUser user);

        // Book CRUD
        Task AddBookAsync(IBook book);
        Task DeleteBookAsync(string id);
        Task<IBook> GetBookAsync(string id);
        Task<Dictionary<string, IBook>> GetAllBooksAsync();
        Task<string> GetBooksCountAsync();
        Task UpdateBookAsync(IBook book);

        // State CRUD
        Task AddStateAsync(IState state);
        Task UpdateStateAsync(IState state);
        Task DeleteStateAsync(string id);
        Task<IState> GetStateAsync(string id);
        Task<Dictionary<string, IState>> GetAllStatesAsync();
        Task<string> GetStatesCountAsync();
        

        // Event CRUD
        Task AddEventAsync(IEvent even);
        Task UpdateEventAsync(IEvent even);
        Task DeleteEventAsync(string id);
        Task<IEvent> GetEventAsync(string id);
        Task<Dictionary<string, IEvent>> GetAllEventsAsync();
        Task<string> GetEventsCountAsync();


        // Utils
        Task<bool> CheckIfUserExistsAsync(string id);
        Task<bool> CheckIfBookExistsAsync(string id);
        Task<bool> CheckIfStateExistsAsync(string id);
        Task<bool> CheckIfEventExistsAsync(string id);


    }
}
