using System;
using System.Text.Json.Serialization;

namespace CampgroundApp.Clients.FireBaseModels
{
    public class SignInRequest
    {
        public string email { get; set; }

        public string password { get; set; }

        [JsonPropertyName("returnSecureToken")]
        public bool ReturnSecureToken => true;
    }

    public class SignUpRequest : SignInRequest
    {
    }

    public class DeleteRequest
    {
        public string idToken { get; set; }
    }

    public class ResetPasswordRequest
    {
        public string requestType { get; set; }

        public string email { get; set; }
    }

    public class ChangeEmailRequest
    {
        public string idToken { get; set; }

        public string email { get; set; }
    }

    public class ChangePasswordRequest
    {
        public string idToken { get; set; }

        public string password { get; set; }
    }
}
