using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaderaSoft.Models.DTO
{
    public class PermissionGroupeDTO
    {
        public PermissionDTO permission { get; set; }
        public DroitDTO groupeUtilisateur { get; set; }

        public bool create { get; set; }
        public bool delete { get; set; }
        public bool update { get; set; }
        public bool read { get; set; }
        public bool softdelete { get; set; }

        public PermissionGroupeDTO()
        {
            permission = new PermissionDTO();
            groupeUtilisateur = new DroitDTO();
        }
    }
}