using System;
namespace Contracts.Models.Response
{
    public class ImageResponseModel
    {
        public Guid Id { get; set; }

        public Guid CampgroundId { get; set; }

        public string Url { get; set; }

        public Guid UserId { get; set; }

        public DateTime DateCreated { get; set; }

        public Guid ImagesId { get; set; }
    }
}
