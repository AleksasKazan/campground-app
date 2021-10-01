using System;
using System.Collections.Generic;

namespace CampgroundApp.FireBaseModels
{
    public class ErrorResponse
    {
        public Error error { get; set; }
    }

    public class Error2
    {
        public string message { get; set; }
    }

    public class Error
    {
        public List<Error> errors { get; set; }
        public string domain { get; set; }
        public string reason { get; set; }
        public int code { get; set; }
        public string message { get; set; }
    }

}
