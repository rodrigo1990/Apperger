using System;
using System.Collections.Generic;
using System.Text;

namespace Apperger.Data
{
    public class Usuario
    {
        public string email { get; set; }
        public string password { get; set; }

        public Usuario(string email, string password)
        {
            this.email = email;
            this.password = password;
        }
    }
}
