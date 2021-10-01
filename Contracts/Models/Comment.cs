using System;
namespace Contracts.Models
{
    public class Comment
    {
        public Guid Id { get; set; }

        public Guid CampgroundId { get; set; }

        public int Rating { get; set; }

        public string Text { get; set; }

        public Guid UserId { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
