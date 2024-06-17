using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Model
{
    public class StateModel
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string BookId { get; set; }
        public bool Availability { get; set; }

        public StateModel(string id, string bookId, DateTime date, bool availability)
            
        { 
            this.Id = id;
            this.BookId = bookId;
            this.Date = date;
            this.Availability = availability;

        
        }

    }
}
