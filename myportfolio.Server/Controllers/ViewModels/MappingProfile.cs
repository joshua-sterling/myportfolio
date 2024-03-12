using AutoMapper;
using myportfolio.Server.Models;

namespace myportfolio.Server.Controllers.ViewModels
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RowingEventViewModel, RowingEvent>();
        }
    }
}
