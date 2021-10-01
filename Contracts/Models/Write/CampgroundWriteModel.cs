using System;
namespace Contracts.Models.Write
{
    public class CampgroundWriteModel
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public DateTime DateCreated { get; set; }

        public string Location { get; set; }
    }
}
