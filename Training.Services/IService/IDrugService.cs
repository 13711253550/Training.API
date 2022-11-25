using Training.Domain.DTO;
using Training.Domain.Entity;
using Training.Domain.Entity.Drug_Management;
using Training.Domain.Shard;

namespace Training.Services.IService
{
    public interface IDrugService
    {
        Result<object> AddDrug(Drug drug);
        Result<object> AddDrug_Order(ViewOrderdetail viewOrderdetail);
        Result<object> AddDrug_Type(Drug_Type drug_Type);
        Result<object> AddLogistics(Logistics logistics);
        Result<int> DelDrug(int Id);
        Result<int> DelDrug_Type(int Id);
        Result<int> DelLogistics(int Id);
        Result<List<Logistics>> GetLogistics();
        Result<List<Orderdetail>> GetOrderdetailList();
        Result<List<Orderdetail_DTO>> GetSmallList();
        Result<List<Drug_DTO>> GetListDrug();
        Result<List<Drug_Type_DTO>> GetListDrug_Type();
        Result<int> UpdDrug(Drug drug);
        Result<int> UpdDrug_Order(Orderdetail drug_Order);
        Result<int> UpdDrug_Type(Drug_Type drug_Type);

    }
}