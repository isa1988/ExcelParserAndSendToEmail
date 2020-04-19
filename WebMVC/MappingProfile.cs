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
            InportExcelMapping();
            SendMessageMapping();
            UserInfoMapping();
        }

        private void InportExcelMapping()
        {
            CreateMap<InportExcelModel, InportExcelDto>();
        }

        private void SendMessageMapping()
        {
            CreateMap<SendMessageModel, SendMessageDto>();
            CreateMap<SendMessageDto, SendMessageModel>();
            CreateMap<InportExcelModel, SendMessageModel>()
                .ForMember(x => x.UserList, p => p.Ignore());
        }
        private void UserInfoMapping()
        {
            CreateMap<UserInfoDto, UserInfoModel>();
            CreateMap<UserInfoModel, UserInfoDto>();
        }

    }
}
