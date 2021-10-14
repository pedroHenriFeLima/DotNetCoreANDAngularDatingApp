using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    //the below will be returned once user logs in
    public class UserDTO
    {
        public string Username { get; set; }
        public string Token { get; set; }
    }
}