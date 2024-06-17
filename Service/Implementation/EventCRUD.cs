using Data.API;
using Service.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    internal class EventCRUD : IEventCRUD
    {
        private IDataRepository dataRepository;

        public EventCRUD(IDataRepository dataRepository)
        {
            this.dataRepository = dataRepository;
        }

         private IEventDTO Map(IEvent even)
        {
            return new EventDTO(even.Id, even.StateId, even.CustomerId, even.Type, even.EventDate);
        }

        public async Task AddEventAsync(string id, string stateid, string customerid, string type = "")
        {
            await this.dataRepository.AddEventAsync(id, stateid, customerid, type);
        }

        public async Task<IEventDTO> GetEventAsync(string id)
        {
            return this.Map(await this.dataRepository.GetEventAsync(id));
        }

        public async Task UpdateEventAsync(string id, DateTime eventdate, string stateid, string customerid, string type = "")
        {
            await this.dataRepository.UpdateEventAsync(id, eventdate, stateid, customerid, type);
        }

        public async Task DeleteEventAsync(string id)
        {
            await this.dataRepository.DeleteEventAsync(id);
        }

        public async Task<Dictionary<string, IEventDTO>> GetAllEventsAsync()
        {
            Dictionary<string, IEventDTO> result = new Dictionary<string, IEventDTO>();

            foreach (IEvent even in (await this.dataRepository.GetAllEventsAsync()).Values)
            {
                result.Add(even.Id, this.Map(even));
            }

            return result;
        }

        public async Task<string> GetEventsCountAsync()
        {
            return await this.dataRepository.GetEventsCountAsync();
        }


    }
}
