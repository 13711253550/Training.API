using Training.Domain.DTO;
using Training.Domain.Entity.Seckill;

namespace Training.Services.IService
{
    public interface ISeckillService
    {
        List<goods> GetGoods();
        List<Seckill_Show_DTO> GetList();
        void SetRedis();
        void Seckill(SeckillOrder SeckillOrder);

        ViewAddAlipayTrade Getinput(int id);
    }
}