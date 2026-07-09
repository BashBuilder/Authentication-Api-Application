using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationApi.Application.DTO
{
    public record class LoginDTO(
            [Required, EmailAddress]
            string Email,
            [Required]
            string password
        );

}
