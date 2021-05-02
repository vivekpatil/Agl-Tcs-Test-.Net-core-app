using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agl_Tcs_Test.Interfaces
{
    public interface IDataSource
    {
        public Task<string> GetDataAsync();
    }
}
