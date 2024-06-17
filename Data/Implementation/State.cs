using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implementation
{
    internal class State : API.IState
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string BookId { get; set; }
        public bool Availability { get; set; }


        public State() {
            Id = string.Empty;
            Date = DateTime.Now;
            BookId = string.Empty;
            Availability = false;
        }
        public State(string bookId, bool availability)
        {
            Id = Guid.NewGuid().ToString();
            Date = DateTime.Now;
            BookId = bookId;
            Availability = availability;
        }
    }


    
}