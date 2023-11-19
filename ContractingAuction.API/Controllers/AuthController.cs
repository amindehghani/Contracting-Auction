using ContractingAuction.Core.Dtos;
using ContractingAuction.Core.Interfaces.IServices;
using ContractingAuction.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ContractingAuction.API.Controllers;

[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(
        IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [Route("api/[controller]/login")]
    public async Task<AuthResponse> Login(LoginDto login)
    {
        return await _authService.LoginAsync(login.Email!, login.Password!);
    }

    [HttpPost]
    [Route("api/[controller]/signup")]
    public async Task<AuthResponse> Signup(SignupDto signup)
    {
        return await _authService.SignupAsync(signup.Email, signup.Username, signup.Password);
    }
}