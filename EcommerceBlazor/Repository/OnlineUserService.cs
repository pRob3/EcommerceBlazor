using EcommerceBlazor.Repository.IRepository;

namespace EcommerceBlazor.Repository;

public class OnlineUserService : IOnlineUserService
{
    private readonly HashSet<string> _onlineUserIds = new HashSet<string>();

    public List<string> GetLoggedInUsers() => _onlineUserIds.ToList();

    public void UserLoggedIn(string userId)
    {
        _onlineUserIds.Add(userId);
        // Optionally update the database
    }

    public void UserLoggedOut(string userId)
    {
        _onlineUserIds.Remove(userId);
        // Optionally update the database
    }
}
