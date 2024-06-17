using Service.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Model
{
    public class EventModelOperation
    {
        public static EventModelOperation CreateModelOperation(IEventCRUD? eventCrud = null)
        {
            return new EventModelOperation(eventCrud ?? IEventCRUD.CreateEventCRUD());
        }



        private IEventCRUD eventCRUD;

        public EventModelOperation(IEventCRUD? eventCrud = null)
        {
            this.eventCRUD = eventCrud ?? IEventCRUD.CreateEventCRUD();
        }

        private EventModel Map(IEventDTO even)
        {
            return new EventModel(even.Id, even.StateId, even.CustomerId, even.Type, even.EventDate);
        }

        public async Task AddAsync(string id, string stateid, string customerid, string type = "")
        {
            await this.eventCRUD.AddEventAsync(id, stateid, customerid, type);
        }

        public async Task<EventModel> GetAsync(string id)
        {
            return this.Map(await this.eventCRUD.GetEventAsync(id));
        }

        public async Task UpdateAsync(string id, DateTime eventdate, string stateid, string customerid, string type = "")
        {
            await this.eventCRUD.UpdateEventAsync(id, eventdate, stateid, customerid, type);
        }

        public async Task DeleteAsync(string id)
        {
            await this.eventCRUD.DeleteEventAsync(id);
        }

        public async Task<Dictionary<string, EventModel>> GetAllAsync()
        {
            Dictionary<string, EventModel> result = new Dictionary<string, EventModel>();

            foreach (IEventDTO even in (await this.eventCRUD.GetAllEventsAsync()).Values)
            {
                result.Add(even.Id, this.Map(even));
            }

            return result;
        }

        public async Task<string> GetCountAsync()
        {
            return await this.eventCRUD.GetEventsCountAsync();
        }
    }
}
