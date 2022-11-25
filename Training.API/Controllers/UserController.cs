using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        

    }
}
