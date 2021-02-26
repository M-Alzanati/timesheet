using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using TimeSheetAPI.Models;

namespace TimeSheetAPI.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);

        IEnumerable<MyIdentityUser> GetAll();

        MyIdentityUser GetById(string id);
    }

    public class UserService : IUserService
    {
        public IImmutableDictionary<MyIdentityUser, string> UsersRefreshTokensReadOnlyDictionary => _usersRefreshTokens.ToImmutableDictionary();

        private readonly ConcurrentDictionary<MyIdentityUser, string> _usersRefreshTokens;

        private IdentityDataContext _dataContext;

        private IConfiguration _configuration;

        public UserService(IdentityDataContext dataContext, IConfiguration config)
        {
            _dataContext = dataContext;
            _configuration = config;
            _usersRefreshTokens = new ConcurrentDictionary<MyIdentityUser, string>();
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _dataContext.Users.SingleOrDefault(x => x.Email == model.Username);
            if (user == null) return null;

            var token = generateJwtToken(user);

            _usersRefreshTokens.AddOrUpdate(user, token, (s, t) => token);
            return new AuthenticateResponse(user, token);
        }

        public IEnumerable<MyIdentityUser> GetAll()
        {
            return _dataContext.Users.ToList();
        }

        public MyIdentityUser GetById(string id)
        {
            return _dataContext.Users.SingleOrDefault(r => r.Id == id);
        }

        private string generateJwtToken(MyIdentityUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var permClaims = new List<Claim>();
            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permClaims.Add(new Claim("uuid", user.Id));

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                permClaims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
