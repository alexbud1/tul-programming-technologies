using Data.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceTests
{
    internal class TestUser : IUser
    {

        public TestUser(string id, string email, string phone, string name) {
            
            this.Id = id;
            this.Email = email;
            this.Phone = phone;
            this.Name = name;
        
        }

        public string Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }

    }
}
