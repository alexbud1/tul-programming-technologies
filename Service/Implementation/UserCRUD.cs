using Data.API;
using Service.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    internal class UserCRUD : IUserCRUD
    {
        private IDataRepository dataRepository;

        public UserCRUD(IDataRepository dataRepository)
        {
            this.dataRepository = dataRepository;
        }

        private IUserDTO Map(IUser user)
        {
            return new UserDTO(user.Id, user.Email, user.Phone, user.Name);
        }

        public async Task AddUserAsync(string id, string email, string phone, string name)
        {
            await this.dataRepository.AddUserAsync(id, email, phone, name);
        }

        public async Task<IUserDTO> GetUserAsync(string id)
        {
            return this.Map(await this.dataRepository.GetUserAsync(id));
        }

        public async Task UpdateUserAsync(string id, string email, string phone, string name)
        {
            await this.dataRepository.UpdateUserAsync(id, email, phone, name);
        }

        public async Task DeleteUserAsync(string id)
        {
            await this.dataRepository.DeleteUserAsync(id);
        }

        public async Task<Dictionary<string, IUserDTO>> GetAllUsersAsync()
        {
            Dictionary<string, IUserDTO> result = new Dictionary<string, IUserDTO>();

            foreach (IUser user in (await this.dataRepository.GetAllUsersAsync()).Values)
            {
                result.Add(user.Id, this.Map(user));
            }

            return result;
        }

        public async Task<string> GetUsersCountAsync()
        {
            return await this.dataRepository.GetUsersCountAsync();
        }


    }
}
