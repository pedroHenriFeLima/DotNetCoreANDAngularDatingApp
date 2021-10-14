using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using API.DTOs;
using Microsoft.EntityFrameworkCore;


namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        public AccountController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        //data could be in the body of the request
        //url
        //because of the APIController attribute inside of the BaseApiController, it can automatically identify the parameters
        public async Task<ActionResult<AppUser>> Register (RegisterDto registerDto) {
            //check if user already exist
            if(await UserExist(registerDto.Username)) return BadRequest("User already exist!!");

            //whenever using keyword is used it will dispose the class after a while
            //HMACSHA512 class somehow it is linked to a disposible method
            using var hmac = new HMACSHA512();

            var user = new AppUser{
                UserName = registerDto.Username.ToLower(),
                //ComputeHash receives a bytery datatype, therefore password (string) needs to be converted
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };
            _context.Users.Add(user);
            //actually sabe data into the databse
            await _context.SaveChangesAsync();
            return user;
        }
        //the method below will be called prior to creating a user on the database
        private async Task<bool> UserExist(string username) {
            //check if there is already an entry in the DB with an username with this name already
            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}