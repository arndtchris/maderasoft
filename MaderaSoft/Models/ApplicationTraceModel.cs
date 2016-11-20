using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaderaSoft.Models
{
    public class ApplicationTraceDTO
    {
        public string utilisateur { get; set; }
        public DateTime date { get; set; }
        public string action { get; set; }
        public string description { get; set; }

        public ApplicationTraceDTO()
        {

        }

    }
}