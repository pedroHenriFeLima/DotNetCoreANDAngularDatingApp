using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Entities;

namespace API.Controllers
{
    //MODEL VIEW CONTROLLER
    //Model - API
    //view - client angular
    //c - controller

     //this particular class is an API controller
    [ApiController]
    //api/users
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private DataContext _context;
        //it grants access to the database via context
        public UsersController(DataContext context){
            _context = context;
        }

        //endPoint to get all users
        [HttpGet]
        public ActionResult<IEnumerable<AppUser>> GetUsers(){

        }

        //endPoint to get a specific user
    }
}