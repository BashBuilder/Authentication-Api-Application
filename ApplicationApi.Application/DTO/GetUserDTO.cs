
using System.ComponentModel.DataAnnotations;

namespace ApplicationApi.Application.DTO
{
    public record class GetUserDTO(
            int Id,
            [Required] string Name,
            [Required] string TelephoneNumber,
            [Required, EmailAddress] string Email,
            [Required] string Address,
            [Required] string Role
        );

}
