using System;
namespace Contracts.Models
{
    public class Campground
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public string Description { get; set; }

        public DateTime DateCreated { get; set; }

        public string Location { get; set; }
    }
}
