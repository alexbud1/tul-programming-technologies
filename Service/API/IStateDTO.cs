using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.API
{
    public interface IStateDTO
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string BookId { get; set; }
        public bool Availability { get; set; }


    }
}
