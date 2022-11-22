using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Training.Domain.DTO;
using Training.Domain.Entity;
using Training.Domain.Shard;
using Training.Services.IService;
using Training.Services.Service;

namespace Training.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public IJWTService m;
        public IUserService n;
        public ValuesController(IJWTService m, IUserService n)
        {
            this.m = m;
            this.n = n;
        }


        [HttpGet]
        public IActionResult GetUser()
        {
            return Ok(n.GetUser());
        }

        [HttpPost]
        public IActionResult Login(LoginDTO login)
        {
            return Ok(m.Login(login));
        }

        [HttpGet]
        public IActionResult GetInfo()
        {
            return Ok(new Result<dynamic>()
            {
                code = stateEnum.Success,
                data = new { name = "管理员", roles = new string[] { "admin" } },
                message = "成功"
            });
        }

        [HttpGet]
        public IActionResult test()
        {
            return Ok(new Result<string>()
            {
                code = stateEnum.Success,
                data = "测试成功",
                message = "成功"
            });
        }

        [Authorize]
        [HttpGet]
        public IActionResult Show()
        {
            return Ok(new Result<string>()
            {
                code = stateEnum.Success,
                message = "成功",
                data = "成功"
            });
        }

        [HttpGet]
        public IActionResult GetNewToken()
        {
            //获取当前请求的Token
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            return Ok(new Result<string>()
            {
                code = stateEnum.Success,
                message = "成功",
                data = m.GetToken(token)
            });
        }
    }
}
