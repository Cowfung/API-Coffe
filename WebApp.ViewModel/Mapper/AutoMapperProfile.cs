using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WebApp.Model;
using WebApp.ViewModel.ViewModel;

namespace WebApp.ViewModel.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainToViewModelMappingProfile()); // Entity -> ViewModel
                cfg.AddProfile(new ViewModelToDomainMappingProfile()); // ViewModel -> Entity
            });
        }
    }
}
