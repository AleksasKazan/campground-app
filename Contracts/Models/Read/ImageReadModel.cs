using System;
namespace Contracts.Models.Read
{
    public class ImageReadModel
    {
        public Guid Id { get; set; }

        public Guid CampgroundId { get; set; }

        public string Url { get; set; }

        public Guid UserId { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
