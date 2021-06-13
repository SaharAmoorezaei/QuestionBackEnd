using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApi.Application.Dto;
using Alamut.Abstractions.Structure;
using WebApi.Services;
using WebApi.Application.Enum;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public ActionResult<Result<LoginResponse>> Login([FromBody]LoginModel model)
        {
            return _userService.Authenticate(model.Username, model.Password);
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public ActionResult<Result> Register([FromBody]RegisterModel model)
        {
            return _userService.Register(model);
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]  
        public ActionResult<Result> GetAll()
        {
            return _userService.GetUsers();
        }

        [HttpPut("ChangeRole")]
        [Authorize(Roles = "Admin")]
        public ActionResult<Result> ChangeRole([FromBody]ChangeRoleModel model)
        {
            return _userService.ChangeRole(model);
        }


    }

}
