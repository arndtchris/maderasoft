using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaderaSoft.Models.DTO
{
    public class DroitDTO
    {
        public int id { get; set; }
        public Boolean create { get; set; }
        public Boolean update { get; set; }
        public Boolean read { get; set; }
        public Boolean delete { get; set; }
        public string libe { get; set; }

        public DroitDTO()
        {

        }
    }
}