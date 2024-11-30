using MoviesApi.Data.Models;

namespace MoviesApi.Services
{
    public interface IAuthServices
    {
        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task<AuthModel> SignInAsync(SignInModel model);

        Task<string> AddRoleAsync(AssignRoleModel model);
        Task<string> CreateRoleAsync(AddRoleModel model);

        Task<AuthModel> RefreshTokenAsync(string token);

        Task<bool> RevokeTokenAsync(string token);
    }
}
