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

        public async Task<List<InstituteView>> GetInstitutes()
        {
            var result = await _unitOfWork.GetRepository<Institute,int>()
                                        .All()
                                        .Include(x=>x.Departments)
                                        .ProjectTo<InstituteView>(_mapper.ConfigurationProvider)
                                        .ToListAsync();
            return result;
        }
    }
}
