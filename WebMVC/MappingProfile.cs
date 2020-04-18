using AutoMapper;
using Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Models;

namespace WebMVC
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            SendMessageMapping();
        }

        private void SendMessageMapping()
        {
            CreateMap<InportExcelModel, InportExcelDto>();
        }
    }
}
