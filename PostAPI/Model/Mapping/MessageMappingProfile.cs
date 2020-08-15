using AutoMapper;
using BusinessLayer.Model;

namespace PostAPI.Model.Mapping
{
    public class MessageMappingProfile : Profile
    {
        public MessageMappingProfile()
        {
            this.CreateMap<Message, MessageDto>()
                .ReverseMap();
            this.CreateMap<SendMessageCommandDto, SendMessageCommand>()
                .ForMember(dest => dest.Receiver, opt => opt.MapFrom(src => src.Receiver));

            this.CreateMap<string, User>()
                .ForMember(dest => dest.Login, opt => opt.MapFrom(src => src));
        }
    }
}
