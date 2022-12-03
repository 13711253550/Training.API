using Training.Domain.DTO;
using Training.Domain.Entity.Seckill;
<<<<<<< HEAD
using Training.Domain.Shard;
=======
>>>>>>> 7c15e5adcce266222ab5ca86412c2d67cb28f0d3

namespace Training.Services.IService
{
    public interface ISeckillService
    {
        List<goods> GetGoods();
        List<Seckill_Show_DTO> GetList();
        void SetRedis();
<<<<<<< HEAD
        string Seckill(SeckillOrder SeckillOrder);

        ViewAddAlipayTrade Getinput(int sAId, int uid);
        void UptState(int sAId, int uid);
        void Refund(Refund_DTO refund_DTO);
        Result<string> opinion(SeckillOrder seckillOrder);

        Result<List<SeckillOrder_DTO>> GetSeckillOrder(int uid);
        bool OpinionSaidState(int sAId);
=======
        void Seckill(SeckillOrder SeckillOrder);

        ViewAddAlipayTrade Getinput(int id);
>>>>>>> 7c15e5adcce266222ab5ca86412c2d67cb28f0d3
    }
}