using AutoMapper;

namespace BusinessLayer.Model.Mapping
{
    class BusinessMappingProfile : Profile
    {
        public BusinessMappingProfile()
        {
            this.CreateMap<DataAccessLayer.Model.MessageBody, Message>().ReverseMap();
            this.CreateMap<DataAccessLayer.PostService.Message, Message>().ReverseMap();
            this.CreateMap<SendMessageCommand, DataAccessLayer.Model.SendMessageCommandDb>()
                .ForMember(dest => dest.ReceiverId, opt => opt.MapFrom(src => src.Receiver.UserId));
        }
    }
}
