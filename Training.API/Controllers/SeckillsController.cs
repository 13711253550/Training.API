using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Training.Domain.Entity.Seckill;
using Training.Services.IService;

namespace Training.API.Controllers
{
    /// <summary>
    /// 秒杀活动
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SeckillsController : ControllerBase
    {
        public ISeckillService seckillService;
        public SeckillsController(ISeckillService seckillService)
        {
            this.seckillService = seckillService;
        }

        //秒杀商品显示
        [HttpGet]
        public IActionResult GetGoods()
        {
            return Ok(seckillService.GetGoods());
        }

        //秒杀活动显示
        [HttpGet]
        public IActionResult GetList()
        {
            return Ok(seckillService.GetList());
        }

        //秒杀商品库存添加到Redis
        [HttpGet]
        public IActionResult SetRedis()
        {
            seckillService.SetRedis();
            return Ok("成功");
        }

        [HttpPost]
        public IActionResult Seckills(SeckillOrder SeckillOrder)
        {
            seckillService.Seckill(SeckillOrder);
            return Ok("成功");
            
        }
    }
}
