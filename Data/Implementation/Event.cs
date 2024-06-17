using Data.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implementation
{
    internal class Event : API.IEvent
    {
        public DateTime EventDate { get; set; }
        public string StateId { get; set; }
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string Type { get; set; }

        public Event(string stateId, string customerId, string type)
        {
            EventDate = DateTime.Now;
            StateId = stateId;
            CustomerId = customerId;
            Id = Guid.NewGuid().ToString();
            Type = type;
        }
    }
}
