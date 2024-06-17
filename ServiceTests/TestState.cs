using Data.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceTests
{
    internal class TestState : IState
    {
        public TestState(string id, string bookId, bool availability)
        
        {
            this.Id = id;
            this.BookId = bookId;
            this.Availability = availability;
            this.Date = DateTime.Now;


        }


        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string BookId { get; set; }
        public bool Availability { get; set; }

    }
}
