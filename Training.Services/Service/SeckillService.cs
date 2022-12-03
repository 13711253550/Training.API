
﻿using Aop.Api.Domain;
using CSRedis;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Training.Domain.DTO;
using Training.Domain.Entity.Seckill;
using Training.Domain.Entity.UserEntity.User;
using Training.Domain.Shard;
using Training.EFCore;
using Training.Services.IService;

namespace Training.Services.Service
{
    public class SeckillService : ISeckillService
    {
        #region 秒杀活动依赖注入

        public IRespotry<goods> goods;
        public IRespotry<seckill_Activity> seckill_Activity;
        public IRespotry<Seckill_Goods> Seckill_Goods;
        public IRespotry<SeckillOrder> SeckillOrder;
        public IRespotry<User> User;

        public SeckillService(IRespotry<goods> goods, IRespotry<seckill_Activity> seckill_Activity, IRespotry<Seckill_Goods> Seckill_Goods, IRespotry<SeckillOrder> SeckillOrder, IRespotry<User> User)
        {
            this.goods = goods;
            this.seckill_Activity = seckill_Activity;
            this.Seckill_Goods = Seckill_Goods;
            this.SeckillOrder = SeckillOrder;
            this.User = User;
        }
        #endregion


        /// <summary>
        /// 秒杀商品显示
        /// </summary>
        /// <returns></returns>
        public List<goods> GetGoods()
        {
            return goods.GetList().ToList();
        }

        /// <summary>
        /// 秒杀活动显示
        /// </summary>
        /// <returns></returns>
        public List<Seckill_Show_DTO> GetList()
        {
            var lst = from a in goods.GetList()
                      join b in Seckill_Goods.GetList() on a.Id equals b.GoodsId
                      join c in seckill_Activity.GetList() on b.SeckillId equals c.Id
                      select new Seckill_Show_DTO
                      {
                          ActivityName = c.ActivityName,
                          ActivityStatus = IsStart(c.StartTime, c.EndTime),
                          EndTime = c.EndTime,
                          GoodsImg = a.GoodsImg,
                          GoodsName = a.GoodsName,
                          GoodsNumber = a.GoodsNumber,
                          GoodsPrice = a.GoodsPrice,
                          SeckillPrice = c.SeckillPrice,
                          StartTime = c.StartTime,
                          GoodsId = a.Id.ToString(),
                          SeckillId = c.Id.ToString(),
                          SeckillNumber = c.SeckillNumber.ToString(),
                          ActivityStatusName = GetName(IsStart(c.StartTime, c.EndTime)),
                      };
            return lst.ToList();
        }
        
        private static string GetName(int v)
        {
            // 1未开始 2进行中 3已结束
            if (v == 1)
            {
                return "进行中";
            }
            else if (v == 2)
            {
                return "已结束";
            }
            else
            {
                return "未开始";
            }
        }

        /// <summary>
        /// 根据开始时间和结束时间判断活动是否结束（在开始时间和结束时间之内是活动开始）
        /// </summary>
        /// <param name="StartTime"></param>
        /// <param name="EndTime"></param>
        /// <returns></returns>
        public static int IsStart(DateTime StartTime, DateTime EndTime)
        {
            DateTime dt = DateTime.Now;
            int n = 0;
            if (dt >= StartTime && dt <= EndTime)
            {
                n = 1;
            }
            if (dt < StartTime)
            {
                n = 0;
            }
            if (dt > EndTime)
            {
                n = 2;
            }
            return n;
        }
        
        /// <summary>
        /// 秒杀商品库存添加到Redis
        /// </summary>
        public void SetRedis()
        {
            //连接Redis集群
            CSRedisClient redis = new CSRedisClient("127.0.0.1:6383");

            //秒杀商品库存从数据库中获取
            RedisHelper.Initialization(redis);
            var lst = GetList();

            //秒杀商品库存添加到Redis

            foreach (var item in lst)
            {
                redis.Set("GoodSNumber" + item.GoodsId, item.SeckillNumber);
            }
        }

        /// <summary>
        /// 对商品进行秒杀
        /// </summary>
        /// <param name="SeckillOrder"></param>
        public string Seckill(SeckillOrder SeckillOrder)
        {
            //判断活动是否过期
            var Seckill_Activity = seckill_Activity.GetList().Where(x => x.Id == SeckillOrder.SAId).FirstOrDefault();
            if (Seckill_Activity.EndTime < DateTime.Now || Seckill_Activity.StartTime > DateTime.Now)
            {
                return "活动已结束或还未开始";
            }
            else
            {
                var lst = GetList();
                //启用线程
                Task.Run(() =>
                {
                    CSRedisClient redis = new CSRedisClient("127.0.0.1:6380");
                    RedisHelper.Initialization(redis);
                    //获取秒杀商品库存
                    var SeckillNumber = redis.Get("GoodSNumber" + SeckillOrder.Gid);
                    //判断商品库存是否大于0
                    if (Convert.ToInt32(SeckillNumber) > 0)
                    {
                        redis.Set("SeckillOrder" + SeckillOrder.SAId + SeckillOrder.Uid, SeckillOrder);
                        redis.IncrBy("GoodSNumber" + SeckillOrder.Gid, -1);
                    }
                });
                return "已参与秒杀";
            }
        }

        /// <summary>
        /// 判断是否秒杀成功 
        /// </summary>
        /// <param name="SeckillOrder"></param>
        /// <returns></returns>
        public Result<string> opinion(SeckillOrder SeckillOrder)
        {
            CSRedisClient redis = new CSRedisClient("127.0.0.1:6383");
            RedisHelper.Initialization(redis);
            string RedisData = redis.Get("SeckillOrder" + SeckillOrder.SAId + SeckillOrder.Uid);
            if (RedisData != null)
            {
                return new Result<string>()
                {
                    code = stateEnum.Success,
                    data = "秒杀成功",
                    message = "秒杀成功"
                };
            }
            else
            {
                return new Result<string>()
                {
                    code = stateEnum.Error,
                    data = "秒杀失败",
                    message = "秒杀失败"
                };
            }

        }

        /// <summary>
        /// 秒杀商品支付
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ViewAddAlipayTrade Getinput(int sAId, int uid)
        {
            CSRedisClient redis = new CSRedisClient("127.0.0.1:6383");
            RedisHelper.Initialization(redis);
            //根据订单号在Redis里查询键值
            var SeckillOrder = redis.Get("SeckillOrder" + sAId + uid);
            SeckillOrder seckillOrder = JsonConvert.DeserializeObject<SeckillOrder>(SeckillOrder);
            if (seckillOrder != null)
            {
                ViewAddAlipayTrade res = new ViewAddAlipayTrade()
                {
                    OutTradeNo = Guid.NewGuid().ToString(),
                    Subject = goods.GetList().Where(x => x.Id == seckillOrder.Gid).FirstOrDefault().GoodsName,
                    TotalAmount = Math.Round(seckill_Activity.GetList().Where(x => x.Id == seckillOrder.SAId).FirstOrDefault().SeckillPrice, 2)
                };
                return res;
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// 秒杀商品支付成功后修改订单状态
        /// </summary>
        /// <param name="id"></param>
        public void UptState(int sAId, int uid)
        {
            CSRedisClient redis = new CSRedisClient("127.0.0.1:6383");
            RedisHelper.Initialization(redis);
            //根据订单号在Redis里查询键值
            var SeckillOrder = redis.Get("SeckillOrder" + sAId + uid);
            SeckillOrder seckillOrder = JsonConvert.DeserializeObject<SeckillOrder>(SeckillOrder);
            if (seckillOrder != null)
            {
                //修改订单状态
                seckillOrder.OrderState = 1;
                seckillOrder.PayTime = DateTime.Now;
                //修改Redis里的键值
                RedisHelper.Set("SeckillOrder" + sAId + uid, seckillOrder);
            }
        }

        /// <summary>
        /// 在活动过期后的35分钟后进行与数据库的同步
        /// </summary>
        public void RedisToDB()
        {
            //连接Redis集群
            CSRedisClient redis = new CSRedisClient("127.0.0.1:6383");
            RedisHelper.Initialization(redis);
            //获取Redis里的所有键值
            var lst = redis.Keys("SeckillOrder" + "*");
            //遍历键值
            foreach (var item in lst)
            {
                //获取键值
                var redisSeckillOrder = redis.Get(item);
                SeckillOrder seckillOrder = JsonConvert.DeserializeObject<SeckillOrder>(redisSeckillOrder);
                //判断订单状态是否为1 
                //判断时间是否在活动结束时间后的35分钟之后 
                if (seckillOrder.OrderState == 1 && DateTime.Now > seckill_Activity.GetList().Where(x => x.Id == seckillOrder.SAId).FirstOrDefault().EndTime.AddMinutes(35))
                {
                    //修改订单状态
                    seckillOrder.OrderState = 2;
                    //修改Redis里的键值
                    RedisHelper.Set(item, seckillOrder);
                    //将订单同步到数据库
                    SeckillOrder.Add(seckillOrder);
                }
            }
            SeckillOrder.Save();
        }

        /// <summary>
        /// 用户退款修改订单状态将用户库存进行退回
        /// </summary>
        /// <param name="refund_DTO"></param>
        public void Refund(Refund_DTO refund_DTO)
        {
            //连接Redis集群
            CSRedisClient redis = new CSRedisClient("127.0.0.1:6380");
            RedisHelper.Initialization(redis);
            //通过订单号获取Redis里的键值
            var SeckillOrder = redis.Get("SeckillOrder" + refund_DTO.SAId + refund_DTO.Uid);
            SeckillOrder seckillOrder = JsonConvert.DeserializeObject<SeckillOrder>(SeckillOrder);
            if (seckillOrder != null)
            {
                //修改订单状态
                seckillOrder.OrderState = 2;
                //修改Redis里的键值
                RedisHelper.Set("SeckillOrder" + refund_DTO.SAId + refund_DTO.Uid, seckillOrder);
                //将用户库存进行退回
                redis.IncrBy("GoodSNumber" + seckillOrder.Gid, 1);
            }
        }

        /// <summary>
        /// 获取到订单信息
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public Result<List<SeckillOrder_DTO>> GetSeckillOrder(int uid)
        {
            //连接Redis集群
            CSRedisClient redis = new CSRedisClient("127.0.0.1:6380");
            RedisHelper.Initialization(redis);
            //获取所有结尾时用户ID的键
            var RedisData = redis.Keys("*" + uid);
            List<SeckillOrder_DTO> lst = new List<SeckillOrder_DTO>();
            //遍历键值
            foreach (var item in RedisData)
            {
                //判断键值是否包含SeckillOrder
                if (item.Contains("SeckillOrder"))
                {
                    //获取键值
                    var redisSeckillOrder = redis.Get(item);
                    SeckillOrder seckillOrder = JsonConvert.DeserializeObject<SeckillOrder>(redisSeckillOrder);
                    SeckillOrder_DTO seckillOrder_DTO = new SeckillOrder_DTO()
                    {
                        CreateTime = seckillOrder.CreateTime,
                        GName = goods.GetList().Where(x => x.Id == seckillOrder.Gid).FirstOrDefault().GoodsName,
                        OName = GetOrderStateName(seckillOrder.OrderState),
                        UName = User.GetList().Where(x => x.Id == seckillOrder.Gid).FirstOrDefault().name,
                        SName = seckill_Activity.GetList().Where(x => x.Id == seckillOrder.SAId).FirstOrDefault().ActivityName,
                        OrderId = seckillOrder.OrderId,
                        PayMoney = seckillOrder.PayMoney,
                        PayTime = seckillOrder.PayTime == null ? "未支付" : "已支付",
                        Gid = seckillOrder.Gid,
                        SAId = seckillOrder.SAId,
                        Uid = seckillOrder.Uid
                    };
                    lst.Add(seckillOrder_DTO);
                }
            }
            return new Result<List<SeckillOrder_DTO>>()
            {
                code = stateEnum.Success,
                data = lst,
                message = "成功"
            };
        }

        private string GetOrderStateName(int state)
        {
            switch (state)
            {
                case 0:
                    return "未支付";
                case 1:
                    return "已支付";
                case 2:
                    return "已退款";
                default:
                    return "未支付";
            }
        }

        public bool OpinionSaidState(int sAId)
        {
            var lst = seckill_Activity.GetList().Where(x => x.Id == sAId).FirstOrDefault().EndTime;
            if (DateTime.Now > lst)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
