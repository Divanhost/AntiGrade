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
    public class WorkService : ServiceBase, IWorkService
    {
        public WorkService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<List<StudentCriteria>> GetStudentCriteria(List<int> studentIds)
        {
            var result = await _unitOfWork.GetRepository<StudentCriteria, int>()
                                            .Filter(x => studentIds.Contains(x.StudentId))
                                            .ToListAsync();
            return result;
        }

        public async Task<bool> UpdateStudentCriteria(List<StudentCriteria> studentCriteria)
        {

            var criteriaForCreate = studentCriteria.Where(x => x.Id == 0).ToList();
            _unitOfWork.GetRepository<StudentCriteria, int>().Create(criteriaForCreate);

            var criteriasForUpdate = studentCriteria.Where(x => x.Id != 0).ToList();
            _unitOfWork.GetRepository<StudentCriteria, int>().Update(criteriasForUpdate);


            return await _unitOfWork.Save() > 0;

        }


        public async Task<List<StudentWork>> GetStudentWorks(int subjectId, List<int> workIds)
        {
            var result = await _unitOfWork.GetRepository<Subject, int>()
                                            .Filter(x => x.Id == subjectId)
                                            .SelectMany(x=>x.Works)
                                            .Where(y=> workIds.Contains(y.Id))
                                            .SelectMany(y => y.StudentWorks)
                                            .ToListAsync();
            return result;
        }

        public async Task<bool> UpdateStudentWorks(List<StudentWork> studentWorks)
        {
              var worksForDelete = studentWorks.Where(x => x.Touched && x.SumOfPoints == 0).ToList();
            _unitOfWork.GetRepository<StudentWork, int>().Delete(worksForDelete);

            var worksForUpdate = studentWorks.Where(x => x.Touched && x.SumOfPoints != 0).ToList();
            _unitOfWork.GetRepository<StudentWork, int>().Update(worksForUpdate);

            var worksForCreate = studentWorks.Where(x => !x.Touched).ToList();
            worksForCreate.ForEach(x=>x.Touched = true);
            _unitOfWork.GetRepository<StudentWork, int>().Create(worksForCreate);

            return await _unitOfWork.Save() > 0;
        }
    }
}
