using Madera.Model;
using MaderaSoft.Models.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaderaSoft.Areas.RechercheDeveloppement.Models.DTOs
{
    public class ModuleDTO
    {
        // GET: GestionModule/ModuleDTOs
        public int id { get; set; }
        [DisplayName("Nom du module")]
        public String libe { get; set; }
        [DisplayName("Gamme")]
        public virtual TModule typeModule { get; set; }
        public decimal prix { get; set; }

        public ModuleDTO()
        {
            TModuleDTO typeModule = new TModuleDTO();
        }
    }

    public class CreateModuleDTO : ModuleDTO
    {
        // GET: GestionModule/ModuleDTOs
        public int id { get; set; }
        [DisplayName("Nom du module")]
        [Required(ErrorMessage = "Veuillez renseigner le nom du module")]
        public String libe { get; set; }
        [Required(ErrorMessage = "Veuillez renseigner la gamme du module")]
        [DisplayName("Gamme")]
        public virtual TModule typeModule { get; set; }
        public CreateModuleDTO()
        {
            TModuleDTO typeModule = new TModuleDTO();
        }
    }

    public class EditModuleDTO : ModuleDTO
    {
        // GET: GestionModule/ModuleDTOs
        public int id { get; set; }
        [DisplayName("Nom du module")]
        public String libe { get; set; }
        [DisplayName("Gamme")]
        public virtual TModule typeModule { get; set; }
        public EditModuleDTO()
        {
            TModuleDTO typeModule = new TModuleDTO();
        }
    }


}