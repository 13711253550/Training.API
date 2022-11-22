using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Training.Domain.DTO;
using Training.Domain.Entity.Drug_Management;
using Training.Domain.Shard;
using Training.Services.IService;

namespace Training.API.Controllers
{
    /// <summary>
    /// 药品管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DrugController : ControllerBase
    {
        public IDrugService drugService;
        public DrugController(IDrugService drugService)
        {
            this.drugService = drugService;
        }
        #region 显示数据

        /// <summary>
        /// 显示所有药品
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetDrug()
        {
            var drug = drugService.GetListDrug();
            return Ok(drug);
        }

        /// <summary>
        /// 显示所有药品分类
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetDrug_Type()
        {
            var drug = drugService.GetListDrug_Type();
            return Ok(drug);
        }

        /// <summary>
        /// 显示所有公司
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetLogistics()
        {
            return Ok(drugService.GetLogistics());
        }

        /// <summary>
        /// 显示商品详情表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetOrderdetailList()
        {
            return Ok(drugService.GetOrderdetailList());
        }

        /// <summary>
        /// 显示订单表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetSmallList()
        {
            return Ok(drugService.GetSmallList());
        }
        #endregion

        #region 添加数据

        /// <summary>
        /// 添加药品数据
        /// </summary>
        /// <param name="drug"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddDrug(Drug drug)
        {
            return Ok(drugService.AddDrug(drug));
        }

        /// <summary>
        /// 添加订单详情表数据
        /// </summary>
        /// <param name="drug_Order"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddDrug_Order(ViewOrderdetail viewOrderdetail)
        {
            return Ok(drugService.AddDrug_Order(viewOrderdetail));
        }
        [HttpPost]
        /// <summary>
        /// 添加药品分类数据
        /// </summary>
        /// <param name="drug_Type"></param>
        /// <returns></returns>
        public IActionResult AddDrug_Type(Drug_Type drug_Type)
        {
            return Ok(drugService.AddDrug_Type(drug_Type));
        }
        [HttpPost]
        /// <summary>
        /// 添加物流公司数据
        /// </summary>
        /// <param name="logistics"></param>
        /// <returns></returns>
        public IActionResult AddLogistics(Logistics logistics)
        {
            return Ok(drugService.AddDrug);
        }


        #endregion

        #region  删除数据
        /// <summary>
        /// 删除药品列表数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult DelDrug(int id)
        {
            return Ok(drugService.DelDrug(id));
        }
        /// <summary>
        /// 删除药品分类表数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult DelDrug_Type(int id)
        {
            return Ok(drugService.DelDrug_Type(id));
        }
        /// <summary>
        /// 删除药品公司数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult DelLogistics(int id)
        {
            return Ok(drugService.DelLogistics(id));
        }
        #endregion

        #region 修改数据
        /// <summary>
        /// 修改药品数据
        /// </summary>
        /// <param name="drug"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UpdDrug(Drug drug)
        {
            return Ok(drugService.UpdDrug(drug));
        }
        /// <summary>
        /// 修改商品详情表
        /// </summary>
        /// <param name="drug_Order"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UpdDrug_Order(Orderdetail drug_Order)
        {
            return Ok(drugService.UpdDrug_Order(drug_Order));
        }
        /// <summary>
        /// 修改商品类型表
        /// </summary>
        /// <param name="drug_Type"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UpdDrug_Type(Drug_Type drug_Type)
        {
            return Ok(drugService.UpdDrug_Type(drug_Type));
        }

        #endregion

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UploadFile(IFormFile file)
        {
            if (file == null)
            {
                return Ok(new Result<string>() { code = stateEnum.Error, message = "上传失败" });
            }
            //获取文件名
            string fileName = file.FileName;
            //获取文件后缀
            string fileExt = Path.GetExtension(fileName);
            //判断文件后缀
            if (fileExt != ".jpg" && fileExt != ".png" && fileExt != ".gif")
            {
                return Ok(new Result<string>() { code = stateEnum.Error, message = "文件格式不正确" });
            }
            //判断文件大小
            if (file.Length > 1024 * 1024 * 5)
            {
                return Ok(new Result<string>() { code = stateEnum.Error, message = "文件大小不能超过5M" });
            }
            //获取文件路径
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", fileName);
            ////判断文件是否存在
            //if (System.IO.File.Exists(filePath))
            //{
            //    return Ok(new Result<string>() { code = stateEnum.Error, message = "文件已经存在" });
            //}
            //创建文件
            using (var stream = System.IO.File.Create(filePath))
            {
                file.CopyTo(stream);
            }
            return Ok(new
            {
                ImgName = "/img/" + fileName,
                ImgBase64 = Convert.ToBase64String(System.IO.File.ReadAllBytes(filePath))
            });
        }

        //测试
        //[HttpGet]
        //public IActionResult Test()
        //{
        //    return Ok(drugService.GetListSql());
        //}

    }
}
