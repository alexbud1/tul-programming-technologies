using Service.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Model
{
    public class UserModelOperation
    {
        public static UserModelOperation CreateModelOperation(IUserCRUD? userCrud = null)
        {
            return new UserModelOperation(userCrud);
        }


        private IUserCRUD userCRUD;

        public UserModelOperation(IUserCRUD? userCrud = null)
        {
            this.userCRUD = userCrud ?? IUserCRUD.CreateUserCRUD();
        }

        private UserModel Map(IUserDTO user)
        {
            return new UserModel(user.Id, user.Email, user.Phone, user.Name);
        }

        public async Task AddAsync(string id, string email, string phone, string name)
        {
            await this.userCRUD.AddUserAsync(id, email, phone, name);
        }

        public async Task<UserModel> GetAsync(string id)
        {
            return this.Map(await this.userCRUD.GetUserAsync(id));
        }

        public async Task UpdateAsync(string id, string email, string phone, string name)
        {
            await this.userCRUD.UpdateUserAsync(id, email, phone, name);
        }

        public async Task DeleteAsync(string id)
        {
            await this.userCRUD.DeleteUserAsync(id);
        }

        public async Task<Dictionary<string, UserModel>> GetAllAsync()
        {
            Dictionary<string, UserModel> result = new Dictionary<string, UserModel>();

            foreach (IUserDTO user in (await this.userCRUD.GetAllUsersAsync()).Values)
            {
                result.Add(user.Id, this.Map(user));
            }

            return result;
        }

        public async Task<string> GetCountAsync()
        {
            return await this.userCRUD.GetUsersCountAsync();
        }
    }
}
