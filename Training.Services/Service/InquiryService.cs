using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TencentCloud.Ame.V20190916.Models;
using TencentCloud.Mrs.V20200910.Models;
using Training.Domain.DTO;
using Training.Domain.Entity.Drug_Management;
using Training.Domain.Entity.UserEntity;
using Training.Domain.Shard;
using Training.EFCore;
using Training.Services.IService;

namespace Training.Services.Service
{
    public class InquiryService : IInquiryService
    {
        public IRespotry<Clinical_Reception> Clinical_Reception;
        public IRespotry<Doctor> Doctor;
        public IRespotry<Label> Label;
        public IRespotry<Doctor_label> Doctor_Label;
        public IRespotry<Training.Domain.Entity.UserEntity.User.User> User;
        public IRespotry<Inquiry_Prescription> Inquiry_Prescription;
        public IRespotry<Inquiry_Result> Inquiry_Result;
        public IRespotry<Drug> Drug;
        public IRespotry<DrugOrder> DrugOrder;
        public InquiryService(IRespotry<Clinical_Reception> Clinical_Reception, IRespotry<Doctor> Doctor, IRespotry<Label> Label, IRespotry<Doctor_label> Doctor_Label, IRespotry<Training.Domain.Entity.UserEntity.User.User> User, IRespotry<Inquiry_Prescription> Inquiry_Prescription, IRespotry<Inquiry_Result> Inquiry_Result, IRespotry<Drug> Drug, IRespotry<DrugOrder> DrugOrder)
        {
            this.Clinical_Reception = Clinical_Reception;
            this.Doctor = Doctor;
            this.Label = Label;
            this.Doctor_Label = Doctor_Label;
            this.User = User;
            this.Inquiry_Prescription = Inquiry_Prescription;
            this.Inquiry_Result = Inquiry_Result;
            this.Drug = Drug;
            this.DrugOrder = DrugOrder;
        }
        /// <summary>
        /// 获取所有接诊信息
        /// </summary>
        /// <returns></returns>
        public Result<List<Clinical_Reception_DTO>> GetClinical_Reception(int Id)
        {
            var lst = Clinical_Reception.GetList().Where(x => x.Did == Id && x.state == false).Select(y => new Clinical_Reception_DTO
            {
                Cid = y.Id,
                UName = y.UName,
                Cause = y.Cause,
                Time = y.Time,
                Price = y.Price,
                phone = y.phone,
                UserSex = y.UserSex == 1 ? "男" : "女",
                UserAge = y.UserAge
            }).ToList();

            return new Result<List<Clinical_Reception_DTO>>()
            {
                code = stateEnum.Success,
                data = lst,
                message = "查询完成"
            };
        }
        /// <summary>
        /// 获取所有医生信息
        /// </summary>
        /// <returns></returns>
        public Result<List<Doctor>> GetDoctor()
        {
            return new Result<List<Doctor>>()
            {
                code = stateEnum.Success,
                data = Doctor.GetList().ToList(),
                message = "查询完成"
            };
        }

        /// <summary>
        /// 获取所有标签
        /// </summary>
        /// <returns></returns>
        public Result<List<Label>> GetLabel()
        {
            return new Result<List<Label>>()
            {
                code = stateEnum.Success,
                data = Label.GetList().ToList(),
                message = "查询完成"
            };
        }

        /// <summary>
        /// 根据参数找到所有对应的医生
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        public Doctor GetDoctorByLabel(ViewAddClinical viewAddClinical)
        {
            var result = Label.GetList().Where(x => x.introduce.Contains(viewAddClinical.Label) || x.LabelName == viewAddClinical.Label).FirstOrDefault();
            //通过医生标签关系表找到所有医生id
            var doctorId = Doctor_Label.GetList().Where(x => x.Lid == result.Id).Select(x => x.Did).ToList();
            //通过医生id找到所有医生
            var doctor = Doctor.GetList().Where(x => doctorId.Contains(x.Id)).ToList();
            //在医生集合中随机选取一个医生
            var random = new Random();
            var index = random.Next(0, doctor.Count);
          
            if (Clinical_Reception.GetList().Where(x => x.Uid == viewAddClinical.Uid && x.state == false).Count() != 0)
            {
                return null;
            }
            return doctor[index];
        }

        /// <summary>
        /// 根据用户Id和症状参数找到所有对应的医生添加到Clinical_Reception表中
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="label"></param>
        /// <returns></returns>
        public Result<string> AddClinical_Reception(ViewAddClinical viewAddClinical)
        {
            var doctor = GetDoctorByLabel(viewAddClinical);
            if (doctor == null)
            {
                return new Result<string>()
                {
                    code = stateEnum.Success,
                    message = "你已经有病例了请等待医生通知"
                };
            }
            var User = this.User.GetList().Where(x => x.Id == viewAddClinical.Uid).FirstOrDefault();

            Clinical_Reception clinical_Reception = new Clinical_Reception()
            {
                Uid = viewAddClinical.Uid,
                address = User.account,
                Cause = viewAddClinical.Label,
                Did = doctor.Id,
                phone = User.phone,
                UName = User.name,
                UserAge = User.UserAge,
                UserSex = User.UserSex
            };
            Clinical_Reception.Add(clinical_Reception);
            int n = Clinical_Reception.Save();
            if (n > 0)
            {
                return new Result<string>()
                {
                    code = stateEnum.Success,
                    message = "已为你找到对应医生",
                    data = clinical_Reception.Id.ToString()
                };
            }
            else
            {
                return new Result<string>()
                {
                    code = stateEnum.Error,
                    message = "请详细说明你的问题"
                };
            }
        }
        /// <summary>
        /// 医生登录
        /// </summary>
        /// <param name="viewDoctorLogin"></param>
        /// <returns></returns>
        public Result<string> DoctorLogin(ViewDoctorLogin viewDoctorLogin)
        {
            //登录 通过账号和密码
            var result = Doctor.GetList().Where(x => x.account == viewDoctorLogin.account && x.password == viewDoctorLogin.password).FirstOrDefault();
            if (result != null)
            {
                return new Result<string>()
                {
                    code = stateEnum.Success,
                    message = "登录成功",
                    data = result.Id.ToString()
                };
            }
            else
            {
                return new Result<string>()
                {
                    code = stateEnum.Error,
                    message = "账号或密码错误"
                };
            }
        }

        /// <summary>
        /// 根据用户Id删除 Clinical_Reception表中的数据
        /// </summary>
        /// <param name="Uid"></param>
        /// <returns></returns>
        public Result<string> DeleteClinical_Reception(int Uid)
        {
            var result = Clinical_Reception.GetList().Where(x => x.Uid == Uid && x.state == false).FirstOrDefault();
            if (result != null)
            {
                Clinical_Reception.Del(result);
                int n = Clinical_Reception.Save();
                if (n > 0)
                {
                    return new Result<string>()
                    {
                        code = stateEnum.Success,
                        data = "删除成功"
                    };
                }
                else
                {
                    return new Result<string>()
                    {
                        code = stateEnum.Success,
                        data = "删除失败"
                    };
                }
            }
            else
            {
                return new Result<string>()
                {
                    code = stateEnum.Error,
                    message = "没有该病例"
                };
            }
        }

        /// <summary>
        /// 药方添加
        /// </summary>
        /// <param name="viewAddPrescription"></param>
        /// <returns></returns>
        public Result<string> AddPrescription(ViewAddPrescription viewAddPrescription)
        {
            Inquiry_Prescription prescription = new Inquiry_Prescription()
            {
                Drug_Id = viewAddPrescription.Drug_Code,
                drug_img = viewAddPrescription.Drug_Image.Substring(21),
                Drug_Number = viewAddPrescription.Drug_Number,
                drug_name = viewAddPrescription.Drug_Name,
                drug_price = viewAddPrescription.Drug_Price * viewAddPrescription.Drug_Number,
                inquiry_result_Id = viewAddPrescription.inquiry_result_Id
            };
            Inquiry_Prescription.Add(prescription);
            int n = Inquiry_Prescription.Save();
            if (n > 0)
            {
                return new Result<string>()
                {
                    code = stateEnum.Success,
                    message = "添加成功",
                    data = prescription.Id.ToString()
                };
            }
            else
            {
                return new Result<string>()
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
        public Result<List<DrugShow_DTO>> DrugShow()
        {
            var lst = from a in Drug.GetList()
                      select new DrugShow_DTO()
                      {
                          Drug_Code = a.Drug_Code,
                          //截取a.Drug_Image的http://localhost:5079
                          Drug_Image = a.Drug_Image,
                          Drug_Name = a.Drug_Name,
                          Drug_Number = 0,
                          Drug_Price = a.Drug_Price,
                          inquiry_result_Id = null
                      };
            return new Result<List<DrugShow_DTO>>()
            {
                code = stateEnum.Success,
                data = lst.ToList(),
                message = "查询成功"
            };
        }

        /// <summary>
        /// 药方药品展示
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public Result<List<Inquiry_Prescription>> GetInquiry_PrescriptionList(string uid)
        {
            var lst = from a in Inquiry_Prescription.GetList()
                      where a.inquiry_result_Id == uid
                      select new Inquiry_Prescription()
                      {
                          Id = a.Id,
                          Drug_Id = a.Drug_Id,
                          drug_img = a.drug_img,
                          Drug_Number = a.Drug_Number,
                          drug_name = a.drug_name,
                          drug_price = a.drug_price
                      };
            return new Result<List<Inquiry_Prescription>>()
            {
                code = stateEnum.Success,
                data = lst.ToList(),
                message = "查询成功"
            };
        }

        /// <summary>
        /// 添加问诊结果
        /// </summary>
        /// <param name="viewAddInquiry_Result"></param>
        /// <returns></returns>
        public Result<string> AddInquiry_Result(ViewAddInquiry_Result viewAddInquiry_Result)
        {

            var result = Clinical_Reception.GetList().Where(x => x.Id == viewAddInquiry_Result.Cid).FirstOrDefault();
            if (result != null)
            {
                Inquiry_Result inquiry_Result = new Inquiry_Result()
                {
                    Cid = viewAddInquiry_Result.Cid,
                    action_in_chief = viewAddInquiry_Result.action_in_chief,
                    Drug_method = viewAddInquiry_Result.Drug_method,
                    inquiry_result_content = viewAddInquiry_Result.inquiry_result_content,
                    prescription_Id = viewAddInquiry_Result.prescription_Id
                };
                Inquiry_Result.Add(inquiry_Result);
                int n = Inquiry_Result.Save();
                if (n > 0)
                {
                    result.state = true;
                    return new Result<string>()
                    {
                        code = stateEnum.Success,
                        data = "添加成功"
                    };
                }
                else
                {
                    return new Result<string>()
                    {
                        code = stateEnum.Error,
                        data = "添加失败"
                    };
                }
            }
            else
            {
                return new Result<string>()
                {
                    code = stateEnum.Error,
                    data = "没有该病例"
                };
            }
        }

        /// <summary>
        /// 问诊结果药方药品联查
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public Result<object> GetresultList(int uid)
        {

            var lst = from a in Inquiry_Result.GetList()
                      join b in Clinical_Reception.GetList()
                      on a.Cid equals b.Id
                      where b.Uid == uid && b.state == false
                      select new
                      {
                          a.Cid,
                          a.action_in_chief,
                          a.Drug_method,
                          a.inquiry_result_content,
                          a.prescription_Id,
                      };

            var list = Inquiry_Prescription.GetList().Where(x => x.inquiry_result_Id == lst.FirstOrDefault().prescription_Id);


            return new Result<object>()
            {
                code = stateEnum.Success,
                data = new
                {
                    Diagnose = lst.FirstOrDefault(),
                    Drug = list
                },
                message = "查询成功"
            };
        }

        /// <summary>
        /// 根据开药获取用户Id
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        private int GetUid(string guid)
        {
            var lst = from a in Inquiry_Result.GetList()
                      join b in Clinical_Reception.GetList()
                      on a.Cid equals b.Id
                      where a.prescription_Id == guid
                      select new
                      {
                          b.Uid
                      };
            return lst.ToList().FirstOrDefault().Uid;
        }

        private int GetCid(string guid)
        {
            var lst = from a in Inquiry_Result.GetList()
                      join b in Clinical_Reception.GetList()
                      on a.Cid equals b.Id
                      where a.prescription_Id == guid
                      select new
                      {
                          b.Id
                      };
            return lst.ToList().FirstOrDefault().Id;
        }

        /// <summary>
        /// 添加药品顶单
        /// </summary>
        /// <param name="viewDrug"></param>
        /// <returns></returns>
        public Result<string> AddDrgOrder(List<ViewDrug> viewDrug)
        {
            var drugOrder_DTO = from a in viewDrug
                                select new DrugOrder_DTO
                                {
                                    Drug_Id = a.Drug_Id,
                                    Drug_Number = a.Drug_Number,
                                    drug_img = a.drug_img,
                                    drug_name = a.drug_name,
                                    drug_price = a.drug_price,
                                    inquiry_result_Id = a.inquiry_result_Id,
                                    User_Id = GetUid(a.inquiry_result_Id),
                                    Cid = GetCid(a.inquiry_result_Id)
                                };

            var DrugOrders = from a in drugOrder_DTO.ToList()
                             select new DrugOrder
                             {
                                 inquiry_result_Id = a.inquiry_result_Id,
                                 Drug_Id = a.Drug_Id,
                                 drug_img = a.drug_img,
                                 Drug_Number = a.Drug_Number,
                                 drug_name = a.drug_name,
                                 drug_price = a.drug_price,
                                 User = User.GetList().Where(x => x.Id == a.User_Id).FirstOrDefault()
                             };
            foreach (var item in DrugOrders.ToList())
            {
                DrugOrder.Add(item);
            }


            if (Drug.Save() > 0)
            {
                var State = Clinical_Reception.Find(drugOrder_DTO.FirstOrDefault().Cid);
                State.state = true;
                Clinical_Reception.Upt(State);
                int a = Clinical_Reception.Save();
                return new Result<string>()
                {
                    code = stateEnum.Success,
                    data = "添加成功"
                };
            }
            else
            {
                return new Result<string>()
                {
                    code = stateEnum.Error,
                    data = "添加失败"
                };

            }

        }
    }
}
