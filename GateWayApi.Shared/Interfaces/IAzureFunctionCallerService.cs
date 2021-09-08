using System.Collections.Generic;
using System.Threading.Tasks;

namespace GateWayApi.Shared.AzureFunCaller
{
    public interface IAzureFunctionCallerService
    {
        Task<IEnumerable<string>> GetUserData(string user);
    }
}
