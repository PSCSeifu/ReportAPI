using System;
using System.Collections.Generic;
using System.Text;

namespace Report.Data.Entites
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
