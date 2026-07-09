using ApplicationApi.Application.DTO;
using Ecommerce.SharedLibrary.Responses;

namespace ApplicationApi.Application.Interfaces
{
    public interface IUser
    {
        Task<Response> Register(AppUserDTO payload);
        Task<Response> Login(LoginDTO payload);
        Task<GetUserDTO> GetUser(int userId);
    }
}
