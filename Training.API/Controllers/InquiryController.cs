using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Training.Domain.DTO;
using Training.Services.IService;

namespace Training.API.Controllers
{
    /// <summary>
    /// 问诊接口
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InquiryController : ControllerBase
    {
        public IInquiryService InquiryService;
        public InquiryController(IInquiryService InquiryService)
        {
            this.InquiryService = InquiryService;
        }

        /// <summary>
        /// 获取问诊列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetInquiryList(int Id)
        {
            return Ok(InquiryService.GetClinical_Reception(Id));
        }

        /// <summary>
        /// 获取医生列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetDoctorList()
        {
            return Ok(InquiryService.GetDoctor());
        }

        /// <summary>
        /// 获取所有标签
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetLabelList()
        {
            return Ok(InquiryService.GetLabel());
        }

        /// <summary>
        /// 根据参数输入的标签获取医生
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetDoctorByLabel(ViewAddClinical viewAddClinical)
        {
            return Ok(InquiryService.AddClinical_Reception(viewAddClinical));
        }

        /// <summary>
        /// 医生登录
        /// </summary>
        /// <param name="viewDoctorLogin"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DoctorLogin(ViewDoctorLogin viewDoctorLogin)
        {
            return Ok(InquiryService.DoctorLogin(viewDoctorLogin));
        }


        /// <summary>
        /// 删除问诊
        /// </summary>
        /// <param name="Uid"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult DeleteClinical_Reception(int Uid)
        {
            return Ok(InquiryService.DeleteClinical_Reception(Uid));
        }


        /// <summary>
        /// 添加就诊药品
        /// </summary>
        /// <param name="viewAddPrescription"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddPrescription(ViewAddPrescription viewAddPrescription)
        {
            return Ok(InquiryService.AddPrescription(viewAddPrescription));
        }

        /// <summary>
        /// 生成一个Guid
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetGuid()
        {
            return Guid.NewGuid().ToString();
        }

        /// <summary>
        /// 显示添加药品
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ShowDrug()
        {
            return Ok(InquiryService.DrugShow());
        }

        /// <summary>
        /// 显示药方
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ShowPrescription(string uid)
        {
            return Ok(InquiryService.GetInquiry_PrescriptionList(uid));
        }

        /// <summary>
        /// 添加问诊结果
        /// </summary>
        /// <param name="viewAddInquiry_Result"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddInquiry_Result(ViewAddInquiry_Result viewAddInquiry_Result)
        {
            return Ok(InquiryService.AddInquiry_Result(viewAddInquiry_Result));
        }

        /// <summary>
        /// 显示用户确诊单
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ShowInquiry_Result(int uid)
        {
            return Ok(InquiryService.GetresultList(uid));
        }

        /// <summary>
        /// 用户下单
        /// </summary>
        /// <param name="ViewDrug"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddOrder(List<ViewDrug> ViewDrug)
        {
            return Ok(InquiryService.AddDrgOrder(ViewDrug));
        }
    }
}
