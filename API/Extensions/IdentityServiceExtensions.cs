using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config){
            //using Microsoft.AspNetCore.Authentication.JwtBearer;
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer
            (options => {
                //using Microsoft.IdentityModel.Tokens;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    //ensure users are validated with a good token
                    ValidateIssuerSigningKey = true,
                    //using System.Text;
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            return services;
        }
    }
}