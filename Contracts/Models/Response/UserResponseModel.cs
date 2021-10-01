using System;
using System.Text.Json.Serialization;

namespace Contracts.Models.Response
{
    public class UserResponseModel
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("firebaseId")]
        public string FirebaseId { get; set; }

        [JsonPropertyName("idToken")]
        public string IdToken { get; set; }

        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; }
    }

    public class UserSignInResponseModel : UserResponseModel
    {
        public string UserName { get; set; }
    }

    public class UserSignUpResponseModel : UserResponseModel
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public DateTime DateCreated { get; set; }
    }

    public class PasswordResetResponseModel : UserResponseModel
    {
    }

    public class ChangeEmailResponseModel : UserResponseModel
    {
    }

    public class ChangePasswordResponseModel : UserResponseModel
    {
    }
}
