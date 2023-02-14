using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Domain.DTO;
using Training.Domain.Entity.Drug_Management;
using Training.Domain.Entity.UserEntity.User;
using Training.Domain.Shard;
using Training.EFCore;
using Training.Services.IService;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Training.Services.Service
{
    public class DrugService : IDrugService
    {
        IMapper _mapper;
        public IRespotry<Drug> Drug;
        public IRespotry<User> User;
        public IRespotry<Drug_Type> Drug_Type;
        public IRespotry<Logistics> Logistics;
        public IRespotry<Orderdetail> Drug_Order;

        public DrugService(IRespotry<Drug> Drug, IRespotry<Drug_Type> Drug_Type, IRespotry<Logistics> Logistics, IRespotry<Orderdetail> Orderdetail, IMapper mapper , IRespotry<User> User)
        {
            this.Drug = Drug;
            this.Drug_Type = Drug_Type;
            this.Logistics = Logistics;
            this.Drug_Order = Orderdetail;
            _mapper = mapper;
            this.User = User;
        }
        #region 药品管理
        /// <summary>
        /// 药品添加
        /// </summary>
        /// <param name="drug"></param>
        /// <returns></returns>
        public Result<object> AddDrug(Drug drug)
        {
            Drug.Add(drug);
            int n = Drug.Save();
            if (n > 0)
            {
                return new Result<object>()
                {
                    code = stateEnum.Success,
                    message = "添加成功"
                };
            }
            else
            {
                return new Result<object>()
                {
                    code = stateEnum.Error,
                    message = "添加失败"
                };
            }
        }

        /// <summary>
        /// 药品显示
        /// </summary>
        /// <returns></returns>
        public Result<List<Drug_DTO>> GetListDrug()
        {
            var lst = from Drug in Drug.GetList()
                      join Drug_Type in Drug_Type.GetList()
                      on Drug.Drug_Type equals Drug_Type.Id
                      select new Drug_DTO()
                      {
                          Drug_Code = Drug.Drug_Code,
                          Drug_Name = Drug.Drug_Name,
                          Drug_Image = Drug.Drug_Image,
                          Drug_Price = Drug.Drug_Price,
                          Drug_IsShelves = Drug.Drug_IsShelves == true ? "是":"否",
                      };
            //Linq转SQL语句
            var lst2 = from a in Drug.GetList().Where(x=>x.Drug_Name == "") select a;
            var sql = lst2.ToString();
            Console.WriteLine(sql);
            return new Result<List<Drug_DTO>>()
            {
                code = stateEnum.Success,
                message = "查询成功",
                data = lst.ToList()
            };
        }

        /// <summary>
        /// 药品删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Result<int> DelDrug(int Id)
        {
            Drug.Del(Drug.Find(Id));
            if (Drug.Save() > 0)
            {
                return new Result<int>()
                {
                    code = stateEnum.Success,
                    message = "删除成功"
                };
            }
            else
            {
                return new Result<int>()
                {
                    code = stateEnum.Error,
                    message = "删除失败"
                };
            }
        }


        /// <summary>
        /// 药品修改
        /// </summary>
        /// <param name="drug"></param>
        /// <returns></returns>
        public Result<int> UpdDrug(Drug drug)
        {
            Drug.Upt(drug);
            if (Drug.Save() > 0)
            {
                return new Result<int>()
                {
                    code = stateEnum.Success,
                    message = "修改成功"
                };
            }
            else
            {
                return new Result<int>()
                {
                    code = stateEnum.Error,
                    message = "修改失败"
                };
            }
        }

        #endregion

        #region 药品类型管理

        /// <summary>
        /// 药品类型添加
        /// </summary>
        /// <param name="drug_Type"></param>
        /// <returns></returns>
        public Result<object> AddDrug_Type(Drug_Type drug_Type)
        {
            Drug_Type.Add(drug_Type);
            int n = Drug_Type.Save();
            if (n > 0)
            {
                return new Result<object>()
                {
                    code = stateEnum.Success,
                    message = "添加成功"
                };
            }
            else
            {
                return new Result<object>()
                {
                    code = stateEnum.Error,
                    message = "添加失败"
                };
            }
        }

        /// <summary>
        /// 药品类型显示
        /// </summary>
        /// <returns></returns>
        public Result<List<Drug_Type_DTO>> GetListDrug_Type()
        {
            var lst = Drug_Type.GetList().ToList();
            var DrugShowList = lst.Select(x => new Drug_Type_DTO
            {
                Drug_Type_Name = x.Drug_Type_Name,
                Id = x.Id,
                Drug_Type_Image = x.Drug_Type_Image,
                Drug_Type_IsShelves = x.Drug_Type_IsShelves==true?"是":"否",
                //商品类型库存 = 对应商品类型的所有库存总和
                Drug_Type_Stock = Drug.GetList().Where(y => y.Drug_Type == x.Id).Sum(y => y.Drug_Stock)
            }).ToList();
            return new Result<List<Drug_Type_DTO>>()
            {
                code = stateEnum.Success,
                message = "查询成功",
                data = DrugShowList
            };
        }

        /// <summary>
        /// 修改药品类型
        /// </summary>
        /// <param name="drug_Type"></param>
        /// <returns></returns>
        public Result<int> UpdDrug_Type(Drug_Type drug_Type)
        {
            Drug_Type.Upt(drug_Type);
            if (Drug_Type.Save() > 0)
            {
                return new Result<int>()
                {
                    code = stateEnum.Success,
                    message = "修改成功"
                };
            }
            else
            {
                return new Result<int>()
                {
                    code = stateEnum.Error,
                    message = "修改失败"
                };
            }
        }

        /// <summary>
        /// 删除药品类型
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Result<int> DelDrug_Type(int Id)
        {
            Drug_Type.Del(Drug_Type.Find(Id));
            if (Drug_Type.Save() > 0)
            {
                return new Result<int>()
                {
                    code = stateEnum.Success,
                    message = "删除成功"
                };
            }
            else
            {
                return new Result<int>()
                {
                    code = stateEnum.Error,
                    message = "删除失败"
                };
            }
        }

        #endregion

        #region 药品订单管理

        /// <summary>
        /// 药品订单添加
        /// </summary>
        /// <param name="drug_Order"></param>
        /// <returns></returns>
        public Result<object> AddDrug_Order(ViewOrderdetail viewOrderdetail)
        {
            Orderdetail drug_Order = new Orderdetail()
            {
                drug_number = viewOrderdetail.drug_number,
                delivery_fee = viewOrderdetail.delivery_fee,
                logistics_company = viewOrderdetail.logistics_company,
                order_status = viewOrderdetail.order_status,
                order_time = viewOrderdetail.order_time,
                remarks = viewOrderdetail.remarks,
                Drug = Drug.GetList().Where(x => x.Id == viewOrderdetail.DId).FirstOrDefault(),
                User = User.GetList().Where(x => x.Id == viewOrderdetail.UId).FirstOrDefault()
            };

            Drug_Order.Add(drug_Order);
            int n = Drug_Order.Save();
            if (n > 0)
            {
                return new Result<object>()
                {
                    code = stateEnum.Success,
                    message = "添加成功"
                };
            }
            else
            {
                return new Result<object>()
                {
                    code = stateEnum.Error,
                    message = "添加失败"
                };
            }
        }

        /// <summary>
        /// 订单修改
        /// </summary>
        /// <param name="drug_Order"></param>
        /// <returns></returns>
        public Result<int> UpdDrug_Order(Orderdetail drug_Order)
        {
            Drug_Order.Upt(drug_Order);
            if (Drug_Order.Save() > 0)
            {
                return new Result<int>()
                {
                    code = stateEnum.Success,
                    message = "修改成功"
                };
            }
            else
            {
                return new Result<int>()
                {
                    code = stateEnum.Error,
                    message = "修改失败"
                };
            }
        }

        /// <summary>
        /// 订单详情显示
        /// </summary>
        /// <returns></returns>
        public Result<List<Orderdetail>> GetOrderdetailList()
        {
            var lst = Drug_Order.GetList().ToList();
            return new Result<List<Orderdetail>>()
            {
                code = stateEnum.Success,
                data = lst,
                message = "查询成功"
            };
        }

        /// <summary>
        /// 订单显示
        /// </summary>
        /// <returns></returns>
        public Result<List<Orderdetail_DTO>> GetSmallList()
        {
            var lst = Drug_Order.GetList().Select(x => new Orderdetail_DTO
            {
                order_status = x.order_status,
                order_time = x.order_time,
                payable_amount = x.Drug.Drug_Price * x.drug_number,
                User_Id = x.User.Id,

            }).ToList();
            return new Result<List<Orderdetail_DTO>>()
            {
                code = stateEnum.Success,
                data = lst,
                message = "查询成功"
            };
        }
        #endregion

        #region 物流公司管理

        /// <summary>
        /// 添加公司
        /// </summary>
        /// <param name="logistics"></param>
        /// <returns></returns>
        public Result<object> AddLogistics(Logistics logistics)
        {
            Logistics.Add(logistics);
            int n = Logistics.Save();
            if (n > 0)
            {
                return new Result<object>()
                {
                    code = stateEnum.Success,
                    message = "添加成功"
                };
            }
            else
            {
                return new Result<object>()
                {
                    code = stateEnum.Error,
                    message = "添加失败"
                };
            }
        }

        /// <summary>
        /// 删除公司
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Result<int> DelLogistics(int Id)
        {
            Logistics.Del(Logistics.Find(Id));
            if (Logistics.Save() > 0)
            {
                return new Result<int>()
                {
                    code = stateEnum.Success,
                    message = "删除成功"
                };
            }
            else
            {
                return new Result<int>()
                {
                    code = stateEnum.Error,
                    message = "删除失败"
                };
            }
        }

        /// <summary>
        /// 显示公司
        /// </summary>
        /// <returns></returns>
        public Result<List<Logistics>> GetLogistics()
        {
            var lst = Logistics.GetList().ToList();
            return new Result<List<Logistics>>()
            {
                code = stateEnum.Success,
                data = lst,
                message = "查询成功"
            };
        }

        #endregion


        //批量删除
        public Result<int> DelDrug_Order(string Ids)
        {
            string[] arr = Ids.Split(','); 
            foreach (var item in Ids)
            {
                Drug_Order.Del(Drug_Order.Find(item));
            }
            if (Drug_Order.Save() > 0)
            {
                return new Result<int>()
                {
                    code = stateEnum.Success,
                    message = "删除成功"
                };
            }
            else
            {
                return new Result<int>()
                {
                    code = stateEnum.Error,
                    message = "删除失败"
                };
            }
        }
    }
}

