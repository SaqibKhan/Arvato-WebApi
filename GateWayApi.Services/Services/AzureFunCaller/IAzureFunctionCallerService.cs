using System.Collections.Generic;
using System.Threading.Tasks;

namespace GateWayApi.Services.AzureFunCaller
{
    public interface IAzureFunctionCallerService
    {
        Task<IEnumerable<string>> GetUserData(string user);
    }
}
