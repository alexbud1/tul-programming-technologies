using Data.API;
using Service.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.API
{
    public interface IUserCRUD
    {
        static IUserCRUD CreateUserCRUD(IDataRepository? dataRepository = null)
        {
            return new UserCRUD(dataRepository ?? IDataRepository.CreateDatabase());
        }
        Task AddUserAsync(string id, string email, string phone, string name);

        Task<IUserDTO> GetUserAsync(string id);

        Task UpdateUserAsync(string id, string email, string phone, string name);

        Task DeleteUserAsync(string id);

        Task<Dictionary<string, IUserDTO>> GetAllUsersAsync();

        Task<string> GetUsersCountAsync();

    }
}
