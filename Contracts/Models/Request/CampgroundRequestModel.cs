using System;
namespace Contracts.Models.Request
{
    public class CampgroundRequestModel
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public string Url { get; set; }
    }
}
