using System.Collections.Generic;
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
    public class SubjectService : ServiceBase, ISubjectService
    {
        public SubjectService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<Subject> CreateSubject(SubjectDto subjectDto)
        {
            var subject =_mapper.Map<Subject>(subjectDto);
            var result = _unitOfWork.GetRepository<Subject,int>().Create(subject);
            await _unitOfWork.Save();
            return result;
        }

        public async Task<bool> DeleteById(int subjectId)
        {
            var subject = await _unitOfWork.GetRepository<Subject, int>()
                .Filter(x => x.Id == subjectId)
                .FirstOrDefaultAsync();
            if (subject != null)
            {
                _unitOfWork.GetRepository<Subject, int>()
                    .Delete(subject);
                bool result = await _unitOfWork.Save() > 0;
                return result;
            }
            else
            {
                throw new WebsiteException("Такой дисциплины не существует");
            }
        }

        public async Task<List<SubjectView>> GetAllSubjects()
        {
            var subjects = await _unitOfWork.GetRepository<Subject,int>()
                                    .Filter(x=>!x.IsDeleted)
                                    .ProjectTo<SubjectView>()
                                    .ToListAsync();
            return subjects;
        }

        public async Task<SubjectView> GetSubjectById(int subjectId)
        {
            var subject = await _unitOfWork.GetRepository<Subject,int>()
                                    .Filter(x=>x.Id == subjectId)
                                    .ProjectTo<SubjectView>()
                                    .FirstOrDefaultAsync();
            return subject;
        }

        public async Task<Subject> UpdateSubject(int subjectId, SubjectDto subjectDto)
        {
            if(subjectDto != null)
            {
                 var subject = await _unitOfWork.GetRepository<Subject, int>()
                    .Filter(x => x.Id == subjectId)
                    .FirstOrDefaultAsync();
                if (subject != null)
                {
                    _mapper.Map(subjectDto, subject);
                    _unitOfWork.GetRepository<Subject, int>()
                        .Update(subject);
                    await _unitOfWork.Save();
                }
                else
                {
                    throw new WebsiteException("Дисциплина не существуетs");
                }
                return subject;
            } 
            else
            {
                throw new WebsiteException("Дисциплина не существует");
            }
        }
    }
}
