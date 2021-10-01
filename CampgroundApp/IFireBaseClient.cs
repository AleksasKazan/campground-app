using System;
using System.Threading.Tasks;
using CampgroundApp.Clients.FireBaseModels;
using CampgroundApp.FireBaseModels;
using Contracts.Models.Request;

namespace CampgroundApp
{
    public interface IFireBaseClient
    {
        Task<SignUpResponse> SignUp(UserSignUpRequestModel user);

        Task<SignInResponse> SignIn(UserSignInRequestModel user);

        Task<ErrorResponse> DeleteAccount(string idToken);

        Task<PasswordResetResponse> PasswordReset(PasswordResetRequestModel user);

        Task<ChangeEmailResponse> ChangeEmail(string email, string idToken);

        Task<ChangePasswordResponse> ChangePassword(string newPssword, string idToken);
    }
}
