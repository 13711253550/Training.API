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

        public SeckillService(IRespotry<goods> goods, IRespotry<seckill_Activity> seckill_Activity, IRespotry<Seckill_Goods> Seckill_Goods)
        {
            this.goods = goods;
            this.seckill_Activity = seckill_Activity;
            this.Seckill_Goods = Seckill_Goods;
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
                          ActivityStatus = c.ActivityStatus,
                          EndTime = c.EndTime,
                          GoodsImg = a.GoodsImg,
                          GoodsName = a.GoodsName,
                          GoodsNumber = a.GoodsNumber,
                          GoodsPrice = a.GoodsPrice,
                          SeckillPrice = c.SeckillPrice,
                          StartTime = c.StartTime,
                          GoodsId = a.Id.ToString(),
                          SeckillId = c.Id.ToString(),
                          SeckillNumber = c.SeckillNumber.ToString()
                      };
            return lst.ToList();
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
        public void Seckill(SeckillOrder SeckillOrder)
        {
            var lst = GetList();
            //启用多线程
            Task.Run(() =>
            {
                CSRedisClient redis = new CSRedisClient("127.0.0.1:6380");
                RedisHelper.Initialization(redis);
                //获取秒杀商品库存
                var SeckillNumber = redis.Get("GoodSNumber" + SeckillOrder.Gid);
                //判断商品库存是否大于0
                if (Convert.ToInt32(SeckillNumber) > 0)
                {
                    redis.Set("SeckillOrder" + SeckillOrder.Uid, SeckillOrder);
                    redis.IncrBy("GoodSNumber" + SeckillOrder.Gid, -1);
                }
                else
                {
                    Console.WriteLine("商品已售完");
                }
            });
        }


        /// <summary>
        /// 秒杀商品支付
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ViewAddAlipayTrade Getinput(int id)
        {
            CSRedisClient redis = new CSRedisClient("127.0.0.1:6383");
            RedisHelper.Initialization(redis);
            //根据订单号在Redis里查询键值
            var SeckillOrder = redis.Get("SeckillOrder" + id);
            SeckillOrder seckillOrder = JsonConvert.DeserializeObject<SeckillOrder>(SeckillOrder);
            if (seckillOrder != null)
            {
                ViewAddAlipayTrade res = new ViewAddAlipayTrade()
                {
                    OutTradeNo = Guid.NewGuid().ToString(),
                    Subject = goods.GetList().Where(x => x.Id == seckillOrder.Gid).FirstOrDefault().GoodsName,
                    TotalAmount = Math.Round(seckill_Activity.GetList().Where(x=>x.Id == seckillOrder.SAId).FirstOrDefault().SeckillPrice,2)
                };
                return res;
            }
            else
            {
                return null;
            }

        }
    }
}
