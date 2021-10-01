using System;
using CampgroundApp.FireBaseModels;

namespace CampgroundApp.Clients.FireBaseModels
{
    public class SignInResponse
    {
        public string IdToken { get; set; }

        public string Email { get; set; }

        public string RefreshToken { get; set; }

        public string ExpiresIn { get; set; }

        public string LocalId { get; set; }

        public bool Registered { get; set; }
    }

    public class SignUpResponse : ErrorResponse
    {
        public string IdToken { get; set; }

        public string Email { get; set; }

        public string RefreshToken { get; set; }

        public string ExpiresIn { get; set; }

        public string LocalId { get; set; }

        public bool Registered { get; set; }
    }

    public class PasswordResetResponse : SignInResponse
    {
    }

    public class ChangeEmailResponse : SignInResponse
    {
    }

    public class ChangePasswordResponse : SignInResponse
    {
    }

}
