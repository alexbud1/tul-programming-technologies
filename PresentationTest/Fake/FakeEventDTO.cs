using Service.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationTest;

    internal class FakeEventDTO : IEventDTO
    {

    public DateTime EventDate { get; set; }
    public string StateId { get; set; }
    public string Id { get; set; }
    public string CustomerId { get; set; }
    public string Type { get; set; }

    public FakeEventDTO(string stateid, string id, string customerid, string type)
    {
        this.EventDate = DateTime.Now;
        this.StateId = stateid;
        this.Id = id;
        this.CustomerId = customerid;
        this.Type = type;
    }


}

