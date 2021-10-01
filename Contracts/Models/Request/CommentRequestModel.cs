using System;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Models.Request
{
    public class CommentRequestModel
    {
        [Range(1, 5, ErrorMessage = "Value for {0} must be between {1} and {5}.")]
        public int Rating { get; set; }

        public string Text { get; set; }
    }
}
