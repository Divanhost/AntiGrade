using System.Collections.Generic;
using System.Threading.Tasks;
using AntiGrade.Shared.InputModels;
using AntiGrade.Shared.Models;
using AntiGrade.Shared.Models.Identity;
using AntiGrade.Shared.ViewModels;

namespace AntiGrade.Core.Services.Interfaces
{
    public interface IGeneralService
    {
        Task<int> GetCurrentMode();
        Task<bool> UpdateCurrentMode(int id);
        Task<List<Status>> GetAllStatuses();
    }
}
