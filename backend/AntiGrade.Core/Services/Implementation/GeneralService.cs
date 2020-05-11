using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AntiGrade.Core.Services.Interfaces;
using AntiGrade.Data.Repositories.Interfaces;
using AntiGrade.Shared.Exceptions;
using AntiGrade.Shared.InputModels;
using AntiGrade.Shared.Models;
using AntiGrade.Shared.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AntiGrade.Core.Services.Implementation
{
    public class GeneralService : ServiceBase, IGeneralService
    {
        public GeneralService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<int> GetCurrentMode()
        {
            var result = await _unitOfWork.GetRepository<Mode, int>().All().LastOrDefaultAsync();
            return result.WorkMode;
        }

        public async Task<bool> UpdateCurrentMode(int mode)
        {
           var utcNow = DateTime.UtcNow;
           var dbMode = new Mode(){
               WorkMode = mode,
               UpdatedAt = utcNow
           };
            _unitOfWork.ModeRepository.Create(dbMode);
            return await _unitOfWork.Save() > 0;
        }
         public async Task<List<Status>> GetAllStatuses()
        {
            var result = await _unitOfWork.GetRepository<Status, int>().All().ToListAsync();
            return result;
        }
        // Institute CRUD
        public async Task<List<InstituteView>> GetInstitutes()
        {
            var result = await _unitOfWork.GetRepository<Institute,int>()
                                        .All()
                                        .Include(x=>x.Departments)
                                        .ProjectTo<InstituteView>(_mapper.ConfigurationProvider)
                                        .ToListAsync();
            return result;
        }
        public async Task<bool> CreateInstitute(InstituteView institute)
        {
            var dbInstitute = _mapper.Map<Institute>(institute);
            var result = _unitOfWork.GetRepository<Institute,int>().Create(dbInstitute);
            return await _unitOfWork.Save() >0;
        }
        public async Task<bool> UpdateInstitute(InstituteView institute)
        {
            var dbInstitute = _mapper.Map<Institute>(institute);
            _unitOfWork.GetRepository<Institute,int>().Update(dbInstitute);
            return await _unitOfWork.Save() >0;
        }
        public async Task<bool> DeleteInstitute(int id)
        {
             var institute = await _unitOfWork.GetRepository<Institute,int>().Filter(x=>x.Id == id).FirstOrDefaultAsync();
            _unitOfWork.GetRepository<Institute,int>().Delete(institute);
            return await _unitOfWork.Save() >0;
        }
        // end Institute CRUD

        // Department CRUD
        public async Task<List<DepartmentView>> GetDepartments(int instituteId)
        {
            var result = await _unitOfWork.GetRepository<Institute,int>()
                                        .Filter(x=>x.Id == instituteId)
                                        .SelectMany(x=>x.Departments)
                                        .ProjectTo<DepartmentView>(_mapper.ConfigurationProvider)
                                        .ToListAsync();
            return result;
        }
        public async Task<bool> CreateDepartments(List<DepartmentView> departments)
        {
            var dbDepartments = _mapper.Map<List<Department>>(departments);
            _unitOfWork.GetRepository<Department,int>().Create(dbDepartments);
            return await _unitOfWork.Save() >0;
        }
        public async Task<bool> UpdateDepartments(List<DepartmentView> departments)
        {
            var dbDepartments = _mapper.Map<List<Department>>(departments);
            _unitOfWork.GetRepository<Department,int>().Update(dbDepartments);
            return await _unitOfWork.Save() >0;
        }
        public async Task<bool> DeleteDepartment(int id)
        {
             var institute = await _unitOfWork.GetRepository<Department,int>().Filter(x=>x.Id == id).FirstOrDefaultAsync();
            _unitOfWork.GetRepository<Department,int>().Delete(institute);
            return await _unitOfWork.Save() >0;
        }
        // end Department CRUD

         // Course CRUD
        public async Task<List<CourseView>> GetCourses()
        {
            var result = await _unitOfWork.GetRepository<Course,int>()
                                        .All()
                                        .ProjectTo<CourseView>(_mapper.ConfigurationProvider)
                                        .ToListAsync();
            return result;
        }
        public async Task<bool> CreateCourse(CourseView course)
        {
            var dbCourse = _mapper.Map<Course>(course);
            _unitOfWork.GetRepository<Course,int>().Create(dbCourse);
            return await _unitOfWork.Save() >0;
        }
        public async Task<bool> UpdateCourse(CourseView course)
        {
            var dbCourse = _mapper.Map<List<Course>>(course);
            _unitOfWork.GetRepository<Course,int>().Update(dbCourse);
            return await _unitOfWork.Save() >0;
        }
        public async Task<bool> DeleteCourse(int id)
        {
             var dbCourse = await _unitOfWork.GetRepository<Course,int>().Filter(x=>x.Id == id).FirstOrDefaultAsync();
            _unitOfWork.GetRepository<Course,int>().Delete(dbCourse);
            return await _unitOfWork.Save() >0;
        }
        // end Course CRUD
    }
}
