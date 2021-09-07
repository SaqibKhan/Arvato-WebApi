using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateWayApi.Services.AzureFunCaller
{
    public class AzureFunctionCallerService: IAzureFunctionCallerService
    {
        public async Task<IEnumerable<string>> GetUserData(string user)
        {
           return  new List<string> {$"User can read the data", $"User can Write the data", $"User can delete the data"};
        }
    }
}
