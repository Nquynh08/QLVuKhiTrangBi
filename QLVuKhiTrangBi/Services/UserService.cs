using Microsoft.AspNetCore.Http;
using QLVuKhiTrangBi.Services;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public bool IsLoggedIn => _httpContextAccessor.HttpContext?.Session.GetString("IsLoggedIn") == "true";
    public string UserName => _httpContextAccessor.HttpContext?.Session.GetString("UserName");
}

