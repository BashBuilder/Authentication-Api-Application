using ApplicationApi.Application.DTO;
using ApplicationApi.Application.Interfaces;
using ApplicationApi.Infrastructure.Data;
using AuthenticationApi.Domain.Entities;
using Ecommerce.SharedLibrary.Responses;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApplicationApi.Infrastructure.Repositories
{
    public class UserRepository( AuthenticationDbContext context, IConfiguration config ) : IUser
    {
        private async Task<AppUser> GetUserByEmail(string email)
        {
            var user = await context.Users.FirstOrDefaultAsync(user => user.Email == email);
            if (user is null) return null!;
            return user;
        }

        public async Task<GetUserDTO> GetUser(int userId)
        {
            var user = await context.Users.FindAsync(userId);
            if (user is null) return null!;

            var registeredUser = new GetUserDTO(
                    user.Id,
                    user.Name,
                    user.TelephoneNumber,
                    user.Email,
                    user.Address,
                    user.Role
                );
            return registeredUser;
        }

        public async Task<Response> Login(LoginDTO payload)
        {
            var user = await GetUserByEmail(payload.Email);
            if (user is null) return new Response(false, "Invalid credentials");

            bool verifyPassword = BCrypt.Net.BCrypt.Verify(payload.password, user.Password);
            if (!verifyPassword) return new Response(false, "Invalalid Credentials");

            string token = GenerateToken(user);
            return new Response(true, token);

        }

        public async Task<Response> Register(AppUserDTO payload)
        {
            var getUser = await GetUserByEmail(payload.Email);
            if (getUser is not null) return new Response(false, "User already registered ");

            var result = context.Users.Add(new AppUser()
            {
                Name = payload.Name,
                Email = payload.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(payload.Password),
                TelephoneNumber = payload.TelephoneNumber,
                Address = payload.Address,
                Role = payload.Role
            });
            await context.SaveChangesAsync();
            return result.Entity.Id > 0 ? new Response(true, "User registered successfully") 
                : new Response(false, "Invalid data provided");

        }
        private string GenerateToken(AppUser user)
        {
            var key = Encoding.UTF8.GetBytes(config["Authentication:Key"]!);
            var securityKey = new SymmetricSecurityKey(key);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.Name),
                new(ClaimTypes.Email, user.Email),
            };

            if (!string.IsNullOrEmpty(user.Role)) claims.Add(new(ClaimTypes.Role, user.Role));

            var token = new JwtSecurityToken(
                issuer : config["Authentication:Issuer"],
                audience : config["Authentication:Audience"],
                claims : claims,
                expires : null,
                signingCredentials : credentials
             );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
