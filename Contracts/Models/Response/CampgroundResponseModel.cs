using System;
using System.Collections.Generic;

namespace Contracts.Models.Response
{
    public class CampgroundResponseModel
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public DateTime DateCreated { get; set; }

        public string Location { get; set; }

        public IEnumerable<ImageResponseModel> Images { get; set; }

        public IEnumerable<CommentResponseModel> Comments { get; set; }

        public string DefaultImageUrl { get; set; }
    }
}
