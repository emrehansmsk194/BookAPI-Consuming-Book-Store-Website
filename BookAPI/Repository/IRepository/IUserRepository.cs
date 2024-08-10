using BookAPI.Models.DTO;

namespace BookAPI.Repository.IRepository
{
    public interface IUserRepository
    {
        bool isUniqueUser (string username);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<UserDTO> Register(RegisterationRequestDTO registerationRequestDTO);

    }
}
