using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Entities;
using System.Linq;

namespace API.Controllers
{
    //MODEL VIEW CONTROLLER
    //Model - API
    //view - client angular
    //c - controller
    public class UsersController : BaseApiController
    {
        private DataContext _context;
        //it grants access to the database via context
        public UsersController(DataContext context){
            _context = context;
        }

        //endPoint to get all users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers(){
            //returning a list of users asyncronously
            return await _context.Users.ToListAsync();
        }

        //endPoint to get a specific user
        //api/users/3
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id){
            return await _context.Users.FindAsync(id);
        }
    }
}