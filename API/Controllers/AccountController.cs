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
using API.Interfaces;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenservice;
        public AccountController(DataContext context, ITokenService tokenservice)
        {
            _tokenservice = tokenservice;
            _context = context;
        }

        [HttpPost("register")]
        //data could be in the body of the request
        //url
        //because of the APIController attribute inside of the BaseApiController, it can automatically identify the parameters
        public async Task<ActionResult<UserDTO>> Register(RegisterDto registerDto)
        {
            //check if user already exist
            //because 'ActionResult' is being used as a return type - http reponses can be sent back
            if (await UserExists(registerDto.Username)) return BadRequest("User already exist!!");

            //whenever using keyword is used it will dispose the class after a while
            //HMACSHA512 class somehow it is linked to a disposible method
            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                UserName = registerDto.Username.ToLower(),
                //ComputeHash receives a bytery datatype, therefore password (string) needs to be converted
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };
            _context.Users.Add(user);
            //actually sabe data into the databse
            await _context.SaveChangesAsync();
            return new UserDTO
            {
                Username = user.UserName,
                Token = _tokenservice.CreateToken(user)
            };
        }

        //used to send a request with a payload
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> login(LoginDto loginDetails)
        {
            //FindAsync is uselful when trying to find an element through a primary key
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == loginDetails.Username.ToLower());

            if (user == null)
            {
                return Unauthorized("Invalid Username");
            }
            //calculate computed hash of their password using the password password salt
            using var hmac = new HMACSHA512(user.PasswordSalt);
            //this line will generate the same hash as when the user was created
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDetails.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.passwordHash[i])
                {
                    return Unauthorized("invalid password");
                }
            }

            return new UserDTO
            {
                Username = user.UserName,
                Token = _tokenservice.CreateToken(user)
            };
        }
        //the method below will be called prior to creating a user on the database
        private async Task<bool> UserExists(string username)
        {
            //check if there is already an entry in the DB with an username with this name already
            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}