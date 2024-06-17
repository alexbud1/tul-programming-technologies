using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Model
{
    public class EventModel
    {
        public DateTime EventDate { get; set; }
        public string StateId { get; set; }
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string Type { get; set; }


        public EventModel(string id, string stateId, string customerId, string type, DateTime eventDate)
        {
            this.Id = id;
            this.StateId = stateId;
            this.CustomerId = customerId;
            this.Type = type;
            this.EventDate = eventDate;

        }


    }
}
