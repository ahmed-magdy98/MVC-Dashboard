﻿using Assign.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assign.BLL.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        //Employee Get(int? id);
        //IEnumerable<Employee> GetAll();
        //int Add(Employee employee);
        //int Update(Employee employee);
        //int Delete(Employee employee);
        Task<IEnumerable<Employee>> GetEmployeesByDepartmentName(string DeptName);
        Task<string> GetDepartmentByEmployeeId(int? id);

        Task<IEnumerable<Employee>> Search(string name);
    }
}
