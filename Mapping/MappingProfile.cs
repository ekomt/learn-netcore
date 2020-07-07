using AutoMapper;
using ProductApi.Resources;
using ProductApi.Models.Auth;

namespace ProductApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserSignUpResource, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(ur => ur.Email));
        }
    }
}