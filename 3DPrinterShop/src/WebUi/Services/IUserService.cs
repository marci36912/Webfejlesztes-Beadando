using Refit;

namespace WebUi.Services;

[Headers("Content-Type: application/json")]
public interface IUserService
{
    [Get("/latest")]
    Task<UserDto> GetLatest();
}