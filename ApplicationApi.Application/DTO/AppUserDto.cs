using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationApi.Application.DTO
{
    public record class AppUserDTO
        (
            int Id,
            [Required] string Name,
            [Required] string TelephoneNumber,
            [Required, EmailAddress] string Email,
            [Required] string Address,
            [Required] string Password,
            [Required] string Role
        );
}
