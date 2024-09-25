namespace EcommerceBlazor.Repository.IRepository;
public interface IOnlineUserService
{
    List<string> GetLoggedInUsers();
    void UserLoggedIn(string userId);
    void UserLoggedOut(string userId);
}
