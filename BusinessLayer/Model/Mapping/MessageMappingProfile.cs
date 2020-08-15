using AutoMapper;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Model.Mapping
{
    class MessageMappingProfile : Profile
    {
        public MessageMappingProfile()
        {
            this.CreateMap<MessageBody, Message>().ReverseMap();
        }
    }
}
