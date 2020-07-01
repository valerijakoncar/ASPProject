﻿using ASPProjekat.EFDataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekat.ApiApp.Core
{
    public class JwtManager
    {
        private readonly ASPProjekatContext _context;
        //private readonly string _issuer;
        //private readonly string _secretKey;


        public JwtManager(ASPProjekatContext context)//, string issuer, string secretKey
        {
            _context = context;
            //_issuer = issuer;
            //_secretKey = secretKey;
        }

        public string MakeToken(string username, string password)
        {
            var user = _context.Users.Include(u => u.UserUseCases)//join sa tabelom userusecase
                .FirstOrDefault(x => x.Username == username && x.Password == password);

            if (user == null)
            {
                return null;
            }

            var actor = new JwtActor
            {
                Id = user.Id,
                AllowedUseCases = user.UserUseCases.Select(x => x.UseCaseId),
                Identity = user.Username
            };
            var issuer = "asp_api";
            var secretKey = "ThisIsMyVerySecretKey";
            //var actor = new FakeApiActor();

            var claims = new List<Claim> // Jti : "", -> skup podataka koje definisemo u Jwt tokenu
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString(), ClaimValueTypes.String, issuer),
                new Claim(JwtRegisteredClaimNames.Iss, "asp_api", ClaimValueTypes.String, issuer),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64, issuer),
                new Claim("UserId", actor.Id.ToString(), ClaimValueTypes.String, issuer),
                new Claim("ActorData", JsonConvert.SerializeObject(actor), ClaimValueTypes.String, issuer)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var now = DateTime.UtcNow;
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: "Any",
                claims: claims,
                notBefore: now,
                expires: now.AddDays(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

