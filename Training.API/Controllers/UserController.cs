using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Training.Domain.Entity.UserEntity;
using Training.Services.IService;

namespace Training.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserService UserService;
        public UserController(IUserService UserService)
        {
            this.UserService = UserService;
        }

        [HttpPost]
        public IActionResult AddDoctor(Doctor doctor)
        {
            return Ok(UserService.AddDoctor(doctor));
        }

    }
}
