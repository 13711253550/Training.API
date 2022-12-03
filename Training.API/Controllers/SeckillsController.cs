using Aop.Api.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Training.Domain.DTO;
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

        /// <summary>
        /// 秒杀商品显示
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetGoods()
        {
            return Ok(seckillService.GetList());
        }

        /// <summary>
        /// 秒杀活动显示
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetList()
        {
            return Ok(seckillService.GetList());
        }

        /// <summary>
        /// 秒杀商品库存添加到Redis
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SetRedis()
        {
            seckillService.SetRedis();
            return Ok("成功");
        }

        /// <summary>
        /// 秒杀下单
        /// </summary>
        /// <param name="SeckillOrder"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Seckills(SeckillOrder SeckillOrder)
        {
            //判断活动是否过期
           string State = seckillService.Seckill(SeckillOrder);
            if (State =="活动已结束或还未开始")
            {
                return Ok("活动已结束或还未开始");
            }
            //判断是否秒杀成功
            return Ok(seckillService.opinion(SeckillOrder));
            
        }

        /// <summary>
        /// 用户的订单退款
        /// </summary>
        /// <param name="refund_DTO"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Refund(Refund_DTO refund_DTO)
        {
            seckillService.Refund(refund_DTO);
            return Ok("成功");
        }
            
        /// <summary>
        /// 用户的订单显示
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetSeckillOrder(int uid)
        {
            return Ok(seckillService.GetSeckillOrder(uid));
=======

        [HttpPost]
        public IActionResult Seckills(SeckillOrder SeckillOrder)
        {
            seckillService.Seckill(SeckillOrder);
            return Ok("成功");
            
>>>>>>> 7c15e5adcce266222ab5ca86412c2d67cb28f0d3
        }
    }
}
