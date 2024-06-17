using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implementation
{
    internal class User : API.IUser
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }

        public User(string name, string email, string phone)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Email = email;
            Phone = phone;

        }

        public User()
        {
            Id = string.Empty;
            Email = string.Empty;
            Phone = string.Empty;
            Name = string.Empty;
            Email = string.Empty;
            Phone = string.Empty;
        }
    }
}
