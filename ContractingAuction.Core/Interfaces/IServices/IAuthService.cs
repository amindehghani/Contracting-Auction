using ContractingAuction.Core.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace ContractingAuction.Core.Interfaces.IServices;

public interface IAuthService
{
    Task<AuthResponse> LoginAsync(string email, string password);
    Task<AuthResponse> SignupAsync(string email, string username, string password);
}