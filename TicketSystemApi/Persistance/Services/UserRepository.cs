using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TicketSystemApi.Models;
using TicketSystemApi.Persistance.Data;
using TicketSystemApi.Persistance.Interfaces;

namespace TicketSystemApi.Persistance.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly IJWTAuthServices _JwtAuthService;
        private readonly UserManager<User> _usermanager;
        private readonly IMapper _mapper;
        private readonly IBaseRepositry<User> _baseRepositry;

        public UserRepository(IJWTAuthServices JWTAuthService,
                             UserManager<User> userManager,
                             IBaseRepositry<User> baseRepositry,
                             IMapper mapper)
        {
            _usermanager = userManager;
            _mapper = mapper;
            _baseRepositry = baseRepositry;
            _JwtAuthService = JWTAuthService;
        }

        public async Task<SignIn_Result> GetTokenAsync(TokenRequestModel model)
        {
            SignIn_Result result = new SignIn_Result();

            if (model.Username == null || model.Password == null)
            {
                result.Message = "Invaild Email or Password.";
                return result;
            }
            var user = await _usermanager.FindByNameAsync(model.Username);

            if (user == null || !(await _usermanager.CheckPasswordAsync(user, model.Password)))
            {
                result.Message = "Email or Password Isn't correct";
                return result;
            }
            
            var jwtsecuirtytoken = await CreateJwtToken(user);
            result.AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtsecuirtytoken);
            result.Username = user.UserName;
            result.Success = true;
            result.ExprirationDate = jwtsecuirtytoken.ValidTo;
            result.Message = (jwtsecuirtytoken.Audiences.FirstOrDefault().Contains("Invaild"))?jwtsecuirtytoken.Audiences.FirstOrDefault(): "Authenticated successfully";
            return result;
        }

        public async Task<SignIn_Result> RegisterModel(RegisterModel model)
        {
            if (await _usermanager.FindByEmailAsync(model.Email) != null)
                return new SignIn_Result { Message = "Email Already registered" };
            if (await _usermanager.FindByNameAsync(model.Username) != null)
                return new SignIn_Result { Message = "Username Already registered" };
            bool IsPhoneAlreadyRegistered = _usermanager.Users.Any(item => item.PhoneNumber == model.PhoneNumber);
            if (IsPhoneAlreadyRegistered)
            {
                return new SignIn_Result { Message = "Phone Number already exist." };
            }

            //_mapper.Map<User>(model);
            User user = new User();
            user = _mapper.Map<User>(model);

            try
            {
                var result = await _usermanager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    var errors = string.Empty;
                    foreach (var error in result.Errors)
                    {
                        errors = $"{error.Description}";
                    }
                    return new SignIn_Result { Message = errors };
                }
            }
            catch
            {
                return new SignIn_Result { Message = "Invalid Data" };
            }

            //await _usermanager.AddToRoleAsync(user, "User");
            var JwtCredinaltoken = await CreateJwtToken(user);

            return new SignIn_Result { Message = "User Registered Successfully", AccessToken = new JwtSecurityTokenHandler().WriteToken(JwtCredinaltoken), Username = user.UserName, ID = user.Id, ExprirationDate = JwtCredinaltoken.ValidTo };
        }

        private async Task<JwtSecurityToken> CreateJwtToken(User user)
        {
            var userClaims = await _usermanager.GetClaimsAsync(user);
            var roles = await _usermanager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            if(!roles.Contains("admin"))
            {
                var jwtSecurityToken = new JwtSecurityToken(
                    audience: "Invaild Login Credientails " + ",User isn't authorized for login."//will use as Error message;
                   );
                return jwtSecurityToken;
            }
            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            
            var claims = BuildClaims(user)
            .Union(userClaims)
            .Union(roleClaims);

            try
            {
                return _JwtAuthService.BuildToken(claims);
            }
            catch (Exception ex)
            {

                var jwtSecurityToken = new JwtSecurityToken(
                     issuer: null,
                     audience: "Invaild Login Credientails " + ex.Message,//will use as Error message;
                     claims: null,
                     expires: null);
                return jwtSecurityToken;
            }
        }

        private Claim[] BuildClaims(User user)
        {
            //User is Valid
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            };
            return claims;
        }

        public User GetUser(string mobilenum)
        {
            return _baseRepositry.GetTByMobileNumber(mobilenum);
        }

        public bool isexistTicket(string userid)
        {
            var result = _baseRepositry.getTicket(userid);
            return (result.ticket == null) ? true : false;
        }
    }


}