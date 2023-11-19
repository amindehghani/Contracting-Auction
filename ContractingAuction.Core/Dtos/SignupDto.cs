using System.ComponentModel.DataAnnotations;

namespace ContractingAuction.Core.Dtos;

public class SignupDto
{
    [Required] public string Email { get; set; } = "";
    [Required] public string Password { get; set; } = "";
    [Required] public string Username { get; set; } = "";
}