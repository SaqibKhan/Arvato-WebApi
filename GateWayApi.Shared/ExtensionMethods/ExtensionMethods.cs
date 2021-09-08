using System.Collections.Generic;
using System.Linq;
using GateWayApi.DAL.Entity;


namespace GateWayApi.Shared.ExtensionMethods
{
    public static class ExtensionMethods
    {
       public static User WithoutPassword(this User user)
        {
            if (user == null) return null;

            user.Password = null;
            return user;
        }
    }
}
