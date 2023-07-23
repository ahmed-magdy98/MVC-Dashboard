using Assign.DAL.Entities;
using Assign.PL.Models;
using AutoMapper;

namespace Assign.PL.Mappers
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentViewModel>().ReverseMap();
            //CreateMap<DepartmentViewModel, Department>();
        }
    }
}
