using Data.API;
using Service.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.API
{
    public interface IEventCRUD
    {
        static IEventCRUD CreateEventCRUD(IDataRepository? dataRepository = null)
        {
            return new EventCRUD(dataRepository ?? IDataRepository.CreateDatabase());
        }

        Task AddEventAsync(string id, string stateid, string customerid, string type);

        Task<IEventDTO> GetEventAsync(string id);

        Task UpdateEventAsync(string id, DateTime eventdate, string stateid, string customerid, string type);

        Task DeleteEventAsync(string id);

        Task<Dictionary<string, IEventDTO>> GetAllEventsAsync();

        Task<string> GetEventsCountAsync();


    }
}
