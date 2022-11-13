using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Domain.DTO;
using Training.Domain.Entity.Drug_Management;

namespace Training.Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //          数据模型  视图模型
            //CreateMap<Movies, MoviesViewModel>().ReverseMap(); // 例子
            CreateMap<Drug, DrugView>().ReverseMap();
        }
    }
}
