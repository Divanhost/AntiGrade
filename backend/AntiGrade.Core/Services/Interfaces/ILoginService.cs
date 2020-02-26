using System.Threading.Tasks;
using AntiGrade.Shared.InputModels;
using AntiGrade.Shared.Models;

namespace AntiGrade.Core.Services.Interfaces
{
    public interface ILoginService
    {
        Task<string> LoginAsync(LoginDto user);
        Task<TokenCouple> RenewAsync(TokenCouple tokenCouple);
    }
}