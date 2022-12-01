using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeOut
{
    internal class Timing
    {
        public void OrderTimeOut()
        {
            //获取redis中的订单信息
            var order = RedisHelper.LRange("Order", 0, -1);
            //遍历订单信息
            foreach (var item in order)
            {
                //将订单信息转换为对象
                var orderInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<Training.Domain.Entity.UserEntity.DrugOrder>(item);
                //判断订单是否超时
                if (orderInfo.CreateTime.AddMinutes(30) < DateTime.Now)
                {
                    //修改订单状态
                    orderInfo.OrderState = 2;
                    //将订单信息转换为json字符串
                    var orderJson = Newtonsoft.Json.JsonConvert.SerializeObject(orderInfo);
                    //将订单信息存入redis
                    RedisHelper.LSet("Order", 0, orderJson);
                    //将库存数量返回
                    RedisHelper.IncrBy("DrugNumber", orderInfo.Drug_Number);
                }
            }
        }

        //商品库存进行预热
        public void SetRedis()
        {
            //从数据库中获取商品信息

        }
    }
}
