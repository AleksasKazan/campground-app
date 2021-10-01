using System;
using System.Collections.Generic;

namespace Contracts.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string FirebaseId { get; set; }

        public string IdToken { get; set; }

        public DateTime DateCreated { get; set; }

        public string UserName { get; set; }
    }
}
