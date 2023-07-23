using Assign.DAL.Entities;
using Assign.PL.Models;
using AutoMapper;

namespace Assign.PL.Mappers
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();
        }
    }
}
