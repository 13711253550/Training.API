using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Training.Domain.DTO;
using Training.Domain.Entity;
using Training.Domain.Shard;
using Training.Services.IService;

namespace Training.API.Controllers
{
    /// <summary>
    /// JWT管理
    /// </summary>
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

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetUser()
        {
            return Ok(n.GetUser());
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login(Login_DTO login)
        {
            return Ok(m.Login(login));
        }
        /// <summary>
        /// 用户角色返回
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 获取新Token
        /// </summary>
        /// <returns></returns>
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
