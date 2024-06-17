using Data.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceTests
{
    internal class TestEvent : IEvent
    {
        public TestEvent(string id, string stateId, string customerId, string type) 
        {
            this.Id = id;
            this.StateId = stateId;
            this.CustomerId = customerId;
            this.Type = type;
            this.EventDate = DateTime.Now;

        }

        public DateTime EventDate { get; set; }
        public string StateId { get; set; }
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string Type { get; set; }


    }
}
