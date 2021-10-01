using System;
namespace Contracts.Models.Response
{
    public class CommentResponseModel
    {
        public Guid Id { get; set; }

        public Guid CampgroundId { get; set; }

        public int Rating { get; set; }

        public string Text { get; set; }

        public string UserId { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
