using Ferroviario.Common.Models;
using System.Threading.Tasks;

namespace Ferroviario.Common.Services
{
    public interface IApiService
    {
        Task<Response> GetListAsync<T>(string urlBase, string servicePrefix, string controller);

        Task<bool> CheckConnectionAsync(string url);

        Task<Response> GetTokenAsync(string urlBase, string servicePrefix, string controller, TokenRequest request);

        Task<Response> GetUserByEmail(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, EmailRequest request);

        Task<Response> RegisterUserAsync(string urlBase, string servicePrefix, string controller, UserRequest userRequest);

        Task<Response> RecoverPasswordAsync(string urlBase, string servicePrefix, string controller, EmailRequest emailRequest);

        Task<Response> PutAsync<T>(string urlBase, string servicePrefix, string controller, T model, string tokenType, string accessToken);

        Task<Response> ChangePasswordAsync(string urlBase, string servicePrefix, string controller, ChangePasswordRequest changePasswordRequest, string tokenType, string accessToken);

        Task<Response> GetRequestsForUserAsync(string urlBase, string servicePrefix, string controller, RequestsForUserRequest requestsForUserRequest, string tokenType, string accessToken);

        Task<Response> GetShiftsForUserAsync(string urlBase, string servicePrefix, string controller, ShiftsForUserRequest shiftsForUserRequest, string tokenType, string accessToken);

        Task<Response> GetShiftsForChangeAsync(string urlBase, string servicePrefix, string controller, ShiftsForUserRequest shiftsForUserRequest, string tokenType, string accessToken);

        Task<Response> GetChangesForUserAsync(string urlBase, string servicePrefix, string controller, ChangesForUserRequest changesForUserRequest, string tokenType, string accessToken);

        Task<Response> RegisterRequestAsync(string urlBase, string servicePrefix, string controller, RequestRequest requestRequest, string tokenType, string accessToken);

        Task<Response> CreateChangeAsync(string urlBase, string servicePrefix, string controller, ChangeRequest changeRequest, string tokenType, string accessToken);

        Task<Response> GetShiftForChangeAsync(string urlBase, string servicePrefix, string controller, CurrentShiftRequest currentShiftRequest, string tokenType, string accessToken);

        Task<Response> EditChangeAsync(string urlBase, string servicePrefix, string controller, ConfirmChangeRequest confirmChangeRequest, string tokenType, string accessToken);

        Task<Response> GetTokenAsync(string urlBase, string servicePrefix, string controller, FacebookProfile request);

        Task<Response> RegisterReportAsync(string urlBase, string servicePrefix, string controller, ReportRequest reportRequest);
    }

}
