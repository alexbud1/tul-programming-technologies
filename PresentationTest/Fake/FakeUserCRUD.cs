using Service.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationTest;

internal class FakeUserCRUD : IUserCRUD
{
    private readonly FakeDataRepository _fakeRepository = new FakeDataRepository();

    public FakeUserCRUD()
    {

    }

    public async Task AddUserAsync(string id, string email, string phone, string name)
    {
        await _fakeRepository.AddUserAsync(id, email, phone, name);
    }

    public async Task<IUserDTO> GetUserAsync(string id)
    {
        return await _fakeRepository.GetUserAsync(id);
    }

    public async Task UpdateUserAsync(string id, string email, string phone, string name)
    {
        await _fakeRepository.UpdateUserAsync(id, email, phone, name);
    }

    public async Task DeleteUserAsync(string id)
    {
        await _fakeRepository.DeleteUserAsync(id);
    }

    public async Task<Dictionary<string, IUserDTO>> GetAllUsersAsync()
    {
        Dictionary<string, IUserDTO> result = new Dictionary<string, IUserDTO>();

        foreach (IUserDTO user in (await _fakeRepository.GetAllUsersAsync()).Values)
        {
            result.Add(user.Id, user);
        }

        return result;
    }

    public async Task<string> GetUsersCountAsync()
    {
        return await _fakeRepository.GetUsersCountAsync();
    }
}
