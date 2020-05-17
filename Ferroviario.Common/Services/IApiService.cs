using Ferroviario.Common.Models;
using System.Threading.Tasks;

namespace Ferroviario.Common.Services
{
    public interface IApiService
    {
        Task<Response> GetListAsync<T>(string urlBase, string servicePrefix, string controller);

        Task<bool> CheckConnectionAsync(string url);
    }

}
