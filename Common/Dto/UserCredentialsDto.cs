using System.ComponentModel.DataAnnotations;

namespace Common.Dto
{
    public record UserCredentialsDto([EmailAddress]string Email, string Password);
}