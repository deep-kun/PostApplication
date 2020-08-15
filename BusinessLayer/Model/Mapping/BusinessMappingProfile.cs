using AutoMapper;

namespace BusinessLayer.Model.Mapping
{
    class BusinessMappingProfile : Profile
    {
        public BusinessMappingProfile()
        {
            // message
            this.CreateMap<DataAccessLayer.Model.MessageBody, Message>().ReverseMap();
            this.CreateMap<DataAccessLayer.PostService.Message, Message>().ReverseMap();
            this.CreateMap<SendMessageCommand, DataAccessLayer.Model.SendMessageCommandDb>()
                .ForMember(dest => dest.ReceiverId, opt => opt.MapFrom(src => src.Receiver.UserId));

            // users
            this.CreateMap<DataAccessLayer.PostService.User, User>()
                .ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.UserLogin))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PasswordHash))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.RoleId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));
        }
    }
}
