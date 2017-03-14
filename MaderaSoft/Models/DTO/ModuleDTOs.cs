using Madera.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MaderaSoft.Models.DTO
{
    public class ModuleDTO
    {
        public int id { get; set; }
        [DisplayName("Nom du module")]
        public String libe { get; set; }
        [DisplayName("Gamme")]
        public virtual TModuleDTO typeModule { get; set; }
        [DisplayName("Prix")]
        public decimal prix { get; set; }
        public string coupePrincipe { get; set; }

        public ModuleDTO()
        {
            typeModule = new TModuleDTO();
        }
    }

    public class CreateModuleDTO : ModuleDTO
    {

        [DisplayName("Nom du module")]
        [Required(ErrorMessage = "Veuillez renseigner le nom du module")]
        public String libe { get; set; }
        [Required(ErrorMessage = "Veuillez renseigner la gamme du module")]
        [DisplayName("Gamme")]
        public virtual TModuleDTO typeModule { get; set; }
        [DisplayName("Prix")]
        public decimal prix { get; set; }
        public CreateModuleDTO()
        {
            typeModule = new TModuleDTO();
        }
    }

    public class EditModuleDTO : ModuleDTO
    {

        [DisplayName("Nom du module")]
        public String libe { get; set; }
        [DisplayName("Gamme")]
        public virtual TModuleDTO typeModule { get; set; }
        [DisplayName("Prix")]
        public decimal prix { get; set; }
        public EditModuleDTO()
        {
            typeModule = new TModuleDTO();
        }
    }
}