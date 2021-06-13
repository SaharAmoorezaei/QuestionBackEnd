using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Alamut.Abstractions.Structure;
using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebApi.Application.Dto;
using WebApi.Helpers;
using WebApi.Infra;
using System.Linq;
using WebApi.Domain.Models;

namespace WebApi.Services
{
    public interface IUserService
    {
        Result<LoginResponse> Authenticate(string username, string password);
        Result<List<Application.Dto.User>> GetUsers();
        Result Register(RegisterModel model);
        Result ChangeRole(ChangeRoleModel model);
    }

    public class UserService : IUserService
    {
        IUnitOfWork _repo;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings, IUnitOfWork repo, IMapper mapper)
        {
            _appSettings = appSettings.Value;
            _repo = repo;
            _mapper = mapper;
        }

        public Result<LoginResponse> Authenticate(string username, string password)
        {
            var user = _repo.UserRepository.GetAll(u => u.UserName == username && u.Password == password, includeProperties: "UserRoles.Role").FirstOrDefault();

            if (user == null)
                return Result<LoginResponse>.Error("username or password was incorrect!");

            var response = new LoginResponse() { User = _mapper.Map<Application.Dto.User>(user) };

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, response.User.Id.ToString()),
                    new Claim(ClaimTypes.Role, response.User.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            response.Token = tokenHandler.WriteToken(token);
            return Result<LoginResponse>.Okay(response);
        }

        public Result<List<Application.Dto.User>> GetUsers()
        {
            var user = _repo.UserRepository.GetAll(u => true, includeProperties: "UserRoles.Role");
            var result = _mapper.Map<List<Application.Dto.User>>(user);
            return Result<List<Application.Dto.User>>.Okay(result);
        }

        public Result Register(RegisterModel model)
        {
            try
            {
                var role = _repo.RoleRepository.Find(p => p.Id == (int)Application.Enum.Role.Client).FirstOrDefault();
                var user = _mapper.Map<Domain.Models.User>(model);
                var userRole = new UserRole() { Role = role, User = user };
                _repo.UserRoleRepository.Add(userRole);
                _repo.Save();
                return Result.Okay("Register was successful.");
            }
            catch (Exception exception)
            {
                Console.Write(exception);
                return Result.Error("Register was not successful.");
            }
        }

        public Result ChangeRole(ChangeRoleModel model)
        {
            try
            {
                var userRole = _repo.UserRoleRepository.Find(p => p.UserId == model.Id).FirstOrDefault();
                var role = _repo.RoleRepository.Find(p =>p.Id == (int)model.Role).FirstOrDefault();
                userRole.Role = role;
                _repo.UserRoleRepository.Update(userRole);
                _repo.Save();
                return Result.Okay("Role was changed successfully.");
            }
            catch (Exception exception)
            {
                Console.Write(exception);
                return Result.Error("Role was not changed.");
            }
        }
    }
}