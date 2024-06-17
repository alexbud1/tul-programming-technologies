using Service.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationTest;

internal class FakeEventCRUD : IEventCRUD
{
    private readonly FakeDataRepository _fakeRepository = new FakeDataRepository();

    public FakeEventCRUD()
    {

    }

    public async Task AddEventAsync(string id, string stateid, string customerid, string type)
    {
        await _fakeRepository.AddEventAsync(id, stateid, customerid, type);
    }

    public async Task<IEventDTO> GetEventAsync(string id)
    {
        return await _fakeRepository.GetEventAsync(id);
    }

    public async Task UpdateEventAsync(string id, DateTime eventdate, string stateid, string customerid, string type)
    {
        await _fakeRepository.UpdateEventAsync(id, eventdate, stateid, customerid, type);
    }

    public async Task DeleteEventAsync(string id)
    {
        await _fakeRepository.DeleteEventAsync(id);
    }

    public async Task<Dictionary<string, IEventDTO>> GetAllEventsAsync()
    {
        Dictionary<string, IEventDTO> result = new Dictionary<string, IEventDTO>();

        foreach (IEventDTO evt in (await _fakeRepository.GetAllEventsAsync()).Values)
        {
            result.Add(evt.Id, evt);
        }

        return result;
    }

    public async Task<string> GetEventsCountAsync()
    {
        return await _fakeRepository.GetEventsCountAsync();
    }
}

