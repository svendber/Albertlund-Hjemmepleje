using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Albertlund_Hjemmepleje.Models.Entities
{
    public class User
    {
        private string email;
        private string password;
        private string role;
        private string name;
        private uint phone;
        private string occupation;

        public User(string name, string email, uint phone, string occupation, string role)
        {
            this.Email = email;
            this.Role = role;
            this.Name = name;
            this.Phone = phone;
            this.Occupation = occupation;
        }

        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public string Role { get => role; set => role = value; }
        public string Name { get => name; set => name = value; }
        public uint Phone { get => phone; set => phone = value; }
        public string Occupation { get => occupation; set => occupation = value; }
    }
}

