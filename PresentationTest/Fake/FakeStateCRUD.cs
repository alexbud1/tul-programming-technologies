using Service.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationTest;

internal class FakeStateCRUD : IStateCRUD
{
    private readonly FakeDataRepository _fakeRepository = new FakeDataRepository();

    public FakeStateCRUD()
    {

    }

    public async Task AddStateAsync(string id, string bookid, bool availability)
    {
        await _fakeRepository.AddStateAsync(id, bookid, availability);
    }

    public async Task<IStateDTO> GetStateAsync(string id)
    {
        return await _fakeRepository.GetStateAsync(id);
    }

    public async Task UpdateStateAsync(string id, string bookid, bool availability)
    {
        await _fakeRepository.UpdateStateAsync(id, bookid, availability);
    }

    public async Task DeleteStateAsync(string id)
    {
        await _fakeRepository.DeleteStateAsync(id);
    }

    public async Task<Dictionary<string, IStateDTO>> GetAllStatesAsync()
    {
        Dictionary<string, IStateDTO> result = new Dictionary<string, IStateDTO>();

        foreach (IStateDTO state in (await _fakeRepository.GetAllStatesAsync()).Values)
        {
            result.Add(state.Id, state);
        }

        return result;
    }

    public async Task<string> GetStatesCountAsync()
    {
        return await _fakeRepository.GetStatesCountAsync();
    }
}

