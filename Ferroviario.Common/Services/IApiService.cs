using Ferroviario.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ferroviario.Common.Services
{
        public interface IApiService
        {
            Task<Response> GetListAsync<T>(string urlBase, string servicePrefix, string controller);
        }

}
