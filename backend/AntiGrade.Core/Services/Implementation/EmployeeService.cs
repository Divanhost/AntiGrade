using System.Collections.Generic;
using System.Threading.Tasks;
using AntiGrade.Core.Services.Interfaces;
using AntiGrade.Data.Repositories.Interfaces;
using AntiGrade.Shared.Enums;
using AntiGrade.Shared.Exceptions;
using AntiGrade.Shared.InputModels;
using AntiGrade.Shared.Models;
using AntiGrade.Shared.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AntiGrade.Core.Services.Implementation
{
    public class EmployeeService : ServiceBase, IEmployeeService
    {
        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<Employee> CreateEmployee(EmployeeDto EmployeeDto)
        {
            var employee =_mapper.Map<Employee>(EmployeeDto);
            var result = _unitOfWork.GetRepository<Employee,int>().Create(employee);
            await _unitOfWork.Save();
            return result;
        }

        public async Task<bool> DeleteById(int employeeId)
        {
            var employee = await _unitOfWork.GetRepository<Employee, int>()
                .Filter(x => x.Id == employeeId)
                .FirstOrDefaultAsync();
            if (employee != null)
            {
                _unitOfWork.GetRepository<Employee, int>()
                    .Delete(employee);
                bool result = await _unitOfWork.Save() > 0;
                return result;
            }
            else
            {
                throw new WebsiteException("Такой дисциплины не существует");
            }
        }

        public async Task<List<EmployeeView>> GetAllEmployees()
        {
            var employees = await _unitOfWork.GetRepository<Employee,int>()
                                    .All()
                                    .ProjectTo<EmployeeView>()
                                    .ToListAsync();
            return employees;
        }

        public async Task<List<EmployeeView>> GetAllTeachers()
        {
            var employees = await _unitOfWork.GetRepository<Employee,int>()
                                    .Filter(x=> x.EmployeePositionId == (int)Positions.Teacher)
                                    .ProjectTo<EmployeeView>(_mapper.ConfigurationProvider)
                                    .ToListAsync();
            return employees;
        }


        public async Task<EmployeeView> GetEmployeeById(int employeeId)
        {
            var employee = await _unitOfWork.GetRepository<Employee,int>()
                                    .Filter(x=>x.Id == employeeId)
                                    .ProjectTo<EmployeeView>()
                                    .FirstOrDefaultAsync();
            return employee;
        }

        public async Task<Employee> UpdateEmployee(int EmployeeId, EmployeeDto employeeDto)
        {
            if(employeeDto != null)
            {
                 var employee = await _unitOfWork.GetRepository<Employee, int>()
                    .Filter(x => x.Id == EmployeeId)
                    .FirstOrDefaultAsync();
                if (employee != null)
                {
                    _mapper.Map(employeeDto, employee);
                    _unitOfWork.GetRepository<Employee, int>()
                        .Update(employee);
                    await _unitOfWork.Save();
                }
                else
                {
                    throw new WebsiteException("Дисциплина не существуетs");
                }
                return employee;
            } 
            else
            {
                throw new WebsiteException("Дисциплина не существует");
            }
        }
    }
}