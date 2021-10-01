using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using CampgroundApp;
using CampgroundApp.Clients.FireBaseModels;
using Microsoft.Extensions.DependencyInjection;
using CampgroundApp.Options;
using Contracts.Models.Request;
using CampgroundApp.FireBaseModels;

namespace Persistence.Clients
{
    public class FireBaseClient : IFireBaseClient
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _appSettings;

        public FireBaseClient(HttpClient httpClient, IOptions<AppSettings> appSettings)
        {
            _httpClient = httpClient;
            _appSettings = appSettings.Value;

        }

        public async Task<SignUpResponse> SignUp(UserSignUpRequestModel user)
        {
            var url = $"{_httpClient.BaseAddress}v1/accounts:signUp?key={_appSettings.WebAPIkey}";
            var post = new SignUpRequest
            {
                email = user.Email,
                password = user.Password
            };
            var postJson = JsonSerializer.Serialize(post);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(url),
                Content = new StringContent(postJson, Encoding.UTF8, "application/json")
            };
            var response = await _httpClient.SendAsync(request);
            //var response = await _httpClient.PostAsJsonAsync(url, post);

            return await response.Content.ReadFromJsonAsync<SignUpResponse>();
        }

        public async Task<SignInResponse> SignIn(UserSignInRequestModel user)
        {
            var url = $"{_httpClient.BaseAddress}v1/accounts:signInWithPassword?key={_appSettings.WebAPIkey}";
            var post = new SignInRequest
            {
                email = user.Email,
                password = user.Password,
            };
            var postJson = JsonSerializer.Serialize(post);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(url),
                Content = new StringContent(postJson, Encoding.UTF8, "application/json")
            };
            var response = await _httpClient.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<SignInResponse>();
        }

        public async Task<ErrorResponse> DeleteAccount(string idToken)
        {
            var url = $"v1/accounts:delete?key={_appSettings.WebAPIkey}";
            var post = new DeleteRequest
            {
                idToken = idToken
            };
            var postJson = JsonSerializer.Serialize(post);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_httpClient.BaseAddress, url),
                Content = new StringContent(postJson, Encoding.UTF8, "application/json")
            };
            var response = await _httpClient.SendAsync(request);
            string statusCode = response.StatusCode.ToString();
            //return statusCode;
            return await response.Content.ReadFromJsonAsync<ErrorResponse>();

        }

        public async Task<PasswordResetResponse> PasswordReset(PasswordResetRequestModel user)
        {
            var url = $"v1/accounts:sendOobCode?key={_appSettings.WebAPIkey}";
            //var url = $"v1/accounts:resetPassword?key={_appSettings.WebAPIkey}";
            var post = new ResetPasswordRequest
            {
                email = user.Email,
                //requestType = "PASSWORD_RESET"
                requestType = user.RequestType
            };
            var postJson = JsonSerializer.Serialize(post);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_httpClient.BaseAddress, url),
                Content = new StringContent(postJson, Encoding.UTF8, "application/json")
            };
            var response = await _httpClient.SendAsync(request);

            return await response.Content.ReadFromJsonAsync<PasswordResetResponse>();

        }

        public async Task<ChangeEmailResponse> ChangeEmail(string email, string idToken)
        {
            var url = $"v1/accounts:update?key={_appSettings.WebAPIkey}";
            var post = new ChangeEmailRequest
            {
                idToken = idToken,
                email = email,
            };
            var postJson = JsonSerializer.Serialize(post);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_httpClient.BaseAddress, url),
                Content = new StringContent(postJson, Encoding.UTF8, "application/json")
            };
            var response = await _httpClient.SendAsync(request);

            return await response.Content.ReadFromJsonAsync<ChangeEmailResponse>();

        }

        public async Task<ChangePasswordResponse> ChangePassword(string newPassword, string idToken)
        {
            var url = $"v1/accounts:update?key={_appSettings.WebAPIkey}";
            var post = new ChangePasswordRequest
            {
                password = newPassword,
                idToken = idToken,
            };
            var postJson = JsonSerializer.Serialize(post);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_httpClient.BaseAddress, url),
                Content = new StringContent(postJson, Encoding.UTF8, "application/json")
            };
            var response = await _httpClient.SendAsync(request);

            return await response.Content.ReadFromJsonAsync<ChangePasswordResponse>();

        }
    }
}
