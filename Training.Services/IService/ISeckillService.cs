using Training.Domain.DTO;
using Training.Domain.Entity.Seckill;
using Training.Domain.Shard;

namespace Training.Services.IService
{
    public interface ISeckillService
    {
        List<goods> GetGoods();
        List<Seckill_Show_DTO> GetList();
        void SetRedis();
        string Seckill(SeckillOrder SeckillOrder);

        ViewAddAlipayTrade Getinput(int sAId, int uid);
        void UptState(int sAId, int uid);
        void Refund(Refund_DTO refund_DTO);
        Result<string> opinion(SeckillOrder seckillOrder);

        Result<List<SeckillOrder_DTO>> GetSeckillOrder(int uid);
        bool OpinionSaidState(int sAId);
    }
}