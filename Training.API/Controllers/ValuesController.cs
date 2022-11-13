using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Training.Domain.Entity;
using Training.Services.IService;
using Training.Services.Service;

namespace Training.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public IJWTService m;
        public ValuesController(IJWTService m)
        {
            this.m = m;
        }
        
        [HttpGet]
        public object Login(string account, string password)
        {
            return m.Login(account, password);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Show()
        {
            return Ok("成功");
        }
    }
}
