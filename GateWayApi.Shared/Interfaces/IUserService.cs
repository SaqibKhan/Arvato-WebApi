using GateWayApi.DAL.Entity;

namespace GateWayApi.Shared.Interfaces.UserService
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
    }
}