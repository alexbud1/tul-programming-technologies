using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Model
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }

        public UserModel(string id, string email, string phone, string name) 
        { 
        
            this.Id = id;
            this.Email = email;
            this.Phone = phone;
            this.Name = name;

        
        }


    }
}
