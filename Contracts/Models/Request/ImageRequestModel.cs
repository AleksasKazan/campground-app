using System;
namespace Contracts.Models.Request
{
    public class ImageRequestModel
    {
        public Guid Id { get; set; }

        public Guid CampgroundId { get; set; }

        public string Url { get; set; }

        public Guid UserId { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
