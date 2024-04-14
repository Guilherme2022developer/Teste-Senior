using AutoMapper;
using Sênior.Business.Models;
using SêniorTeste.API.ViewModels;

namespace SêniorTeste.API.Config
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Person, PersonViewModels>().ReverseMap();
            CreateMap<PersonCreate, PersonCreateViewModel>().ReverseMap();
            CreateMap<PersonCreate, PersonResponseViewModel>();
            CreateMap<LoginUser, LoginViewModel>().ReverseMap();
        }
    }
}
