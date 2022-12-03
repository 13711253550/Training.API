using Aop.Api;
using Aop.Api.Domain;
using Aop.Api.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text;
using Training.Domain.DTO;
using Training.Services.IService;

namespace Training.API.Controllers
{
    /// <summary>
    /// 支付宝支付接口
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PayController : ControllerBase
    {
        public IOptions<AlipayConfig> alipayConfig;
        public IInquiryService inquiryService;
        public ISeckillService seckillService;

        public PayController(IOptions<AlipayConfig> alipayConfig, IInquiryService inquiryService, ISeckillService seckillService)
        {
            this.alipayConfig = alipayConfig;
            this.inquiryService = inquiryService;
            this.seckillService = seckillService;
        }
        /// <summary>
        /// 获取到所有的订单
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetDrugOrder(int Id)
        {
            return Ok(inquiryService.GetGoodsOrder(Id));
        }

        /// <summary>
        /// 支付宝支付
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Pay(int Id)
        {
            ViewAddAlipayTrade input = inquiryService.GetInquiry(Id);
            IAopClient aopClient = new DefaultAopClient(alipayConfig.Value);

            AlipayTradePagePayRequest request = new AlipayTradePagePayRequest();

            //同步通知地址

            request.SetReturnUrl($"http://localhost:5079/api/Pay/UptState?id={Id}");

            //异步通知地址

            request.SetNotifyUrl("");

            //业务参数

            request.BizContent = "{" +
                "    \"out_trade_no\":\"" + input.OutTradeNo + "\"," +
                "    \"product_code\":\"FAST_INSTANT_TRADE_PAY\"," +
                "    \"total_amount\":" + input.TotalAmount + "," +
                "    \"subject\":\"" + input.Subject + "\"," +
                "    \"body\":\"" + input.Subject + "\"," +
                "    \"extend_params\":{" +
                "    }" +
                "  }";

            #region 另一种写法
            //支付宝支付完成后 会跳转到同步通知地址


            //AlipayTradePagePayModel model = new AlipayTradePagePayModel();
            //model.OutTradeNo = input.OutTradeNo;
            //model.ProductCode = "FAST_INSTANT_TRADE_PAY";
            //model.TotalAmount = input.TotalAmount.ToString();
            //model.Subject = input.Subject;
            //model.Body = input.Subject;
            //model.ExtendParams = new ExtendParams();
            //request.SetBizModel(model);

            //AlipayTradePagePayResponse response = aopClient.pageExecute(request);

            //return Content(response.Body, "text/html", Encoding.UTF8);
            #endregion

            //调用支付宝接口
            var response = aopClient.pageExecute(request);
            
            return Content(response.Body, "text/html", Encoding.UTF8);
        }

        /// <summary>
        /// 支付宝支付完成后 会跳转到同步通知地址
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult UptState(int id)
        {
            inquiryService.UptState(id);
            //跳转http://localhost:8080/#/payment页面
            return Redirect("http://localhost:8080/#/payment");
        }


        
        /// <summary>
        /// 秒杀订单支付
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SeckillPay(int sAId, int uid)
        {
            bool opinion = seckillService.OpinionSaidState(sAId);
            if (opinion==true)
            {
                ViewAddAlipayTrade input = seckillService.Getinput(sAId, uid);
            IAopClient aopClient = new DefaultAopClient(alipayConfig.Value);

            AlipayTradePagePayRequest request = new AlipayTradePagePayRequest();

            //同步通知地址

            request.SetReturnUrl($"http://localhost:5079/api/Pay/UptState?id={Id}");

            //异步通知地址

            request.SetNotifyUrl("");

            //业务参数

            request.BizContent = "{" +
                "    \"out_trade_no\":\"" + input.OutTradeNo + "\"," +
                "    \"product_code\":\"FAST_INSTANT_TRADE_PAY\"," +
                "    \"total_amount\":" + input.TotalAmount + "," +
                "    \"subject\":\"" + input.Subject + "\"," +
                "    \"body\":\"" + input.Subject + "\"," +
                "    \"extend_params\":{" +
                "    }" +
                "  }";

            #region 另一种写法
            //支付宝支付完成后 会跳转到同步通知地址


            //AlipayTradePagePayModel model = new AlipayTradePagePayModel();
            //model.OutTradeNo = input.OutTradeNo;
            //model.ProductCode = "FAST_INSTANT_TRADE_PAY";
            //model.TotalAmount = input.TotalAmount.ToString();
            //model.Subject = input.Subject;
            //model.Body = input.Subject;
            //model.ExtendParams = new ExtendParams();
            //request.SetBizModel(model);

            //AlipayTradePagePayResponse response = aopClient.pageExecute(request);

            //return Content(response.Body, "text/html", Encoding.UTF8);
            #endregion

            //调用支付宝接口
            var response = aopClient.pageExecute(request);

            return Content(response.Body, "text/html", Encoding.UTF8);
        }
            else
            {
                return Ok("活动已过期");
            }

        }

        /// <summary>
        /// 秒杀订单支付完成后 会跳转到同步通知地址
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult UptSeckillState(int sAId, int uid)
        {
            seckillService.UptState(sAId, uid);
            //跳转http://localhost:8080/#/payment页面
            return Redirect("http://localhost:8080/#/SeckillOrder");
        }
    }
}
