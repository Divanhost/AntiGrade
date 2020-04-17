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

        public async Task<List<StudentCriteriaDto>> GetStudentCriteria(int workId)
        {
            var result = await _unitOfWork.GetRepository<Work, int>()
                                            .Filter(x => x.Id == workId)
                                            .SelectMany(x => x.Criterias)
                                            .SelectMany(y=>y.StudentCriterias)
                                            .ProjectTo<StudentCriteriaDto>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return result;
        }

        public async Task<bool> UpdateStudentCriteria(List<StudentCriteriaDto> studentCriteria)
        {
            var criteriasForUpdate = studentCriteria.Where(x => x.Touched).ToList();
            var criteriaForCreate = studentCriteria.Where(x => !x.Touched).ToList();
            criteriaForCreate.ForEach(x=>x.Touched = true);


            var dbCriteriaCreate = _mapper.Map<List<StudentCriteria>>(criteriaForCreate);
            var dbCriteriaUpdate = _mapper.Map<List<StudentCriteria>>(criteriasForUpdate);

            _unitOfWork.GetRepository<StudentCriteria, int>().Create(dbCriteriaCreate);

            _unitOfWork.GetRepository<StudentCriteria, int>().Update(dbCriteriaUpdate);


            return await _unitOfWork.Save() > 0;

        }


        public async Task<List<StudentWork>> GetStudentWorks(int subjectId)
        {
            var result = await _unitOfWork.GetRepository<Subject, int>()
                                            .Filter(x => x.Id == subjectId)
                                            .SelectMany(x=>x.Works)
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
