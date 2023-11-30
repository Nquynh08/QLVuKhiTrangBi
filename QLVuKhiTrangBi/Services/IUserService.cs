namespace QLVuKhiTrangBi.Services
{
    public interface IUserService
    {
        bool IsLoggedIn { get; }
        string UserName { get; }
    }
}
