using GateWayApi.DAL.Entity;

namespace GateWayApi.Services.UserService
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
    }
}