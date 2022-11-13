using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Domain.DTO;
using Training.Domain.Entity.Drug_Management;
using Training.Domain.Shard;
using Training.EFCore;

namespace Training.Services.Service
{
    public class DrugService
    {
        public IRespotry<Drug> Drug;
        public IRespotry<Drug_Type> Drug_Type;
        public IRespotry<Logistics> Logistics;
        public IRespotry<Orderdetail> Orderdetail;

        public DrugService(IRespotry<Drug> Drug, IRespotry<Drug_Type> Drug_Type, IRespotry<Logistics> Logistics, IRespotry<Orderdetail> Orderdetail)
        {
            this.Drug = Drug;
            this.Drug_Type = Drug_Type;
            this.Logistics = Logistics;
            this.Orderdetail = Orderdetail;
        }

        public Result<object> AddDrug(Drug drug)
        {
            Drug.Add(drug);
            int n = Drug.Save();
            if (n > 0)
            {
                return new Result<object>()
                {
                    Code = stateEnum.Success,
                    Message = "添加成功"
                };
            }
            else
            {
                return new Result<object>()
                {
                    Code = stateEnum.Error,
                    Message = "添加失败"
                };
            }
        }

        public Result<List<DrugView>> ShowDrug()
        {
            var lst = from Drug in Drug.GetList()
                      join Drug_Type in Drug_Type.GetList()
                      on Drug.Drug_Type equals Drug_Type.Id
                      select new DrugView();

            return new Result<DrugView>()
            {
                Code = stateEnum.Success,
                Message = "查询成功",
                Data = lst.ToList()
            };
        }
    }
}
