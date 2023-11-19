using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ContractingAuction.Core.Enums;
using ContractingAuction.Core.Interfaces.IServices;
using ContractingAuction.Core.ViewModels;
using ContractingAuction.Infrastructure.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ContractingAuction.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly JWT _jwt;

    public AuthService(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IOptions<JWT> jwt)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _jwt = jwt.Value;
    }

    private async Task<JwtSecurityToken> CreateJwtAsync(IdentityUser user)
    {
        IList<Claim> userClaims = await _userManager.GetClaimsAsync(user);
        IList<string> roles = await _userManager.GetRolesAsync(user);
        List<Claim> roleClaims = roles.Select(role => new Claim("roles", role)).ToList();

        IEnumerable<Claim> claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("userId", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);
        SymmetricSecurityKey symmetricSecurityKey = new(Encoding.UTF8.GetBytes(_jwt.Key));

        SigningCredentials credentials = new(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
        JwtSecurityToken jwtSecurityToken = new(
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(_jwt.DurationInDays),
            signingCredentials: credentials);

        return jwtSecurityToken;
    }

    public async Task<AuthResponse> LoginAsync(string email, string password)
    {
        AuthResponse response = new();
        IdentityUser? user = await _userManager.FindByEmailAsync(email);
        if (user is null)
        {
            response.ErrorMessage = "User not Found";
            return response;
        }

        bool correctLogin = await _userManager.CheckPasswordAsync(user, password);
        if (!correctLogin)
        {
            response.ErrorMessage = "Username of Password Incorrect";
            return response;
        }

        JwtSecurityToken token = await CreateJwtAsync(user);

        response.Token = new JwtSecurityTokenHandler().WriteToken(token);
        ;
        return response;
    }

    public async Task<AuthResponse> SignupAsync(string email, string username ,string password)
    {
        IdentityUser? userEmail = await _userManager.FindByEmailAsync(email);
        if (userEmail is not null)
        {
            return new AuthResponse() { ErrorMessage = "Email in Use" };
        }

        IdentityUser user = new()
        {
            Email = email,
            UserName = username
        };

        IdentityResult result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            string errors = result.Errors.Aggregate(string.Empty, (current, error) => current + $"{error.Description}, ");
            return new AuthResponse() { ErrorMessage = errors };
        }

        await _userManager.AddToRoleAsync(user, UserRoles.User.ToString());
        JwtSecurityToken jwtToken = await CreateJwtAsync(user);
        return new AuthResponse()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(jwtToken)
        };

    }
}