using System;
namespace Contracts.Models.Write
{
    public class ImageWriteModel
    {
        public Guid Id { get; set; }

        public Guid CampgroundId { get; set; }

        public string Url { get; set; }

        public string UserId { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
