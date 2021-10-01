using System;
using System.Collections.Generic;
using Contracts.Models.Response;

namespace Contracts.Models.Read
{
    public class CampgroundReadModel
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public DateTime DateCreated { get; set; }

        public string Location { get; set; }

        public string Url { get; set; }

        public Guid ImagesId { get; set; }
    }
}
