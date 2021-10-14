using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class AppUser
    {
        //the entity framework will identify ID as an incremental field
        //auto implemented property
        //public means that this property can be get and set from any other class in our app
        //protected either this class or inheritance
        //private class itself

        //because entity framework needs to get and set this property they need to be public
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] passwordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}