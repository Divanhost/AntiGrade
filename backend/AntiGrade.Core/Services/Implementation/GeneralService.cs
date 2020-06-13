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
        public async Task<InstituteView> GetInstitute(int id)
        {
            var result = await _unitOfWork.GetRepository<Institute,int>()
                                        .Filter(x=>x.Id == id)
                                        .Include(x=>x.Departments)
                                        .ProjectTo<InstituteView>(_mapper.ConfigurationProvider)
                                        .FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> CreateInstitute(InstituteView institute)
        {
            var dbInstitute = _mapper.Map<Institute>(institute);
            var result = _unitOfWork.GetRepository<Institute,int>().Create(dbInstitute);
            return await _unitOfWork.Save() >0;
        }
        public async Task<bool> UpdateInstitute(InstituteView instituteDto)
        {
            var institute = _mapper.Map<Institute>(instituteDto);
            var dbInstitute = await _unitOfWork.GetRepository<Institute, int>()
                   .Filter(x => x.Id == institute.Id)
                   .FirstOrDefaultAsync();
            dbInstitute.Name = institute.Name;
            dbInstitute.Departments = null;
            _unitOfWork.GetRepository<Institute,int>().Update(dbInstitute);
            foreach (var item in institute.Departments)
            {
                item.InstituteId = dbInstitute.Id;
            }
            await UpdateDepartments(institute.Departments, dbInstitute.Id);
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
        // public async Task<bool> UpdateDepartments(List<DepartmentView> departments)
        // {
        //     var dbDepartments = _mapper.Map<List<Department>>(departments);
        //     _unitOfWork.GetRepository<Department,int>().Update(dbDepartments);
        //     return await _unitOfWork.Save() >0;
        // }
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
          public async Task<CourseView> GetCourse(int id)
        {
            var result = await _unitOfWork.GetRepository<Course,int>()
                                        .Filter(x=>x.Id == id)
                                        .ProjectTo<CourseView>(_mapper.ConfigurationProvider)
                                        .FirstOrDefaultAsync();
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
        public async Task<bool> CreateNewSemester(SemesterView semester)
        {
            var dbSemester = _mapper.Map<Semester>(semester);
            _unitOfWork.GetRepository<Semester,int>().Create(dbSemester);
            return await _unitOfWork.Save() >0;
        }
        public async Task<List<SemesterView>> GetSemesters()
        {
            var result = await _unitOfWork.GetRepository<Semester,int>()
                                        .All()
                                        .OrderByDescending(x=>x.Subjects.Any())
                                        .ThenByDescending(x=>x.Year)
                                        .ThenBy(x=>x.Id)
                                        .ProjectTo<SemesterView>(_mapper.ConfigurationProvider)
                                        .ToListAsync();
            return result;
        }
        public async Task<SemesterView> GetLastSemester()
        {
            var result = await _unitOfWork.GetRepository<Semester,int>()
                                        .All()
                                        .OrderByDescending(x=>x.Id)
                                        .Take(1)
                                        .ProjectTo<SemesterView>(_mapper.ConfigurationProvider)
                                        .FirstOrDefaultAsync();
            return result;
        }
        private async Task UpdateDepartments(List<Department> newdepartments, int instituteId)
        {
            var existingIds = await GetDepartmentIdsForInstitute(instituteId);
            var departmentsForDelete = existingIds
                .Where(sl => !newdepartments.Any(c => c.Id == sl)).ToList();
            _unitOfWork.GetRepository<Department, int>().Delete(x => departmentsForDelete.Contains(x.Id));
            
            var departmentsForUpdate = newdepartments.Where(x => x.Id != 0).ToList();
            _unitOfWork.GetRepository<Department, int>().Update(departmentsForUpdate);

            var departmentsForCreate = newdepartments.Where(x => x.Id == 0).ToList();
           
            _unitOfWork.GetRepository<Department, int>().Create(departmentsForCreate);
        }
        private async Task<List<int>> GetDepartmentIdsForInstitute(int id)
        {
            var result = await _unitOfWork.GetRepository<Institute, int>()
                .Filter(x => x.Id == id)
                .SelectMany(x => x.Departments)
                .Select(x=>x.Id)
                .ToListAsync();
            return result;
        }
    }
}
