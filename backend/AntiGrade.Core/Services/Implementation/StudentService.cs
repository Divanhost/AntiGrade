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
    public class StudentService : ServiceBase, IStudentService
    {
        public StudentService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<Student> CreateStudent(StudentDto StudentDto)
        {
            var student =_mapper.Map<Student>(StudentDto);
            var result = _unitOfWork.GetRepository<Student,int>().Create(student);
            await _unitOfWork.Save();
            return result;
        }

        public async Task<bool> DeleteById(int studentId)
        {
            var student = await _unitOfWork.GetRepository<Student, int>()
                .Filter(x => x.Id == studentId)
                .FirstOrDefaultAsync();
            if (student != null)
            {
                _unitOfWork.GetRepository<Student, int>()
                    .Delete(student);
                bool result = await _unitOfWork.Save() > 0;
                return result;
            }
            else
            {
                throw new WebsiteException("Такой дисциплины не существует");
            }
        }

        public async Task<List<StudentView>> GetAllStudents()
        {
            var students = await _unitOfWork.GetRepository<Student,int>()
                                    .All()
                                    .ProjectTo<StudentView>()
                                    .ToListAsync();
            return students;
        }

        public async Task<StudentView> GetStudentById(int studentId)
        {
            var student = await _unitOfWork.GetRepository<Student,int>()
                                    .Filter(x=>x.Id == studentId)
                                    .ProjectTo<StudentView>()
                                    .FirstOrDefaultAsync();
            return student;
        }

        public async Task<Student> UpdateStudent(int StudentId, StudentDto studentDto)
        {
            if(studentDto != null)
            {
                 var student = await _unitOfWork.GetRepository<Student, int>()
                    .Filter(x => x.Id == StudentId)
                    .FirstOrDefaultAsync();
                if (student != null)
                {
                    _mapper.Map(studentDto, student);
                    _unitOfWork.GetRepository<Student, int>()
                        .Update(student);
                    await _unitOfWork.Save();
                }
                else
                {
                    throw new WebsiteException("Дисциплина не существуетs");
                }
                return student;
            } 
            else
            {
                throw new WebsiteException("Дисциплина не существует");
            }
        }
    }
}
