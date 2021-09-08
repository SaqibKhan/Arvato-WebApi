using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GateWayApi.Shared.AzureFunCaller;

namespace GateWayApi.Services
{
    public class AzureFunctionCallerService: IAzureFunctionCallerService
    {
        public async Task<IEnumerable<string>> GetUserData(string user)
        {
            var list = new List<string> { $"User can read the data", $"User can Write the data", $"User can delete the data" };
            return list.AsEnumerable();
        }
    }
}
