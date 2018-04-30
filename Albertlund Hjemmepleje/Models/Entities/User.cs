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
            private int phone;
            private string occupation;

            public User(string email, string password, string role, string name, int phone, string occupation)
            {
                this.email = email;
                this.password = password;
                this.role = role;
                this.name = name;
                this.phone = phone;
                this.occupation = occupation;
            }
        }
    }
