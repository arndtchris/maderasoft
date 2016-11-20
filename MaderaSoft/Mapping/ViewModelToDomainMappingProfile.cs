using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Madera.Model;
using MaderaSoft.Models;

namespace MaderaSoft.Mapping
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        /*
         * Permet d'établir les correspondances entre les DTOs de la couche métier 
         * et les Plain Object (représentant direct d'une table en bdd) 
         * 
         */

        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        protected override void Configure()
        {
            CreateMap<AdresseDTO, Adresse>()
               .ForMember(g => g.AdresseID, map => map.MapFrom(vm => vm.AdresseID))
               .ForMember(g => g.codePostal, map => map.MapFrom(vm => vm.codePostal))
               .ForMember(g => g.nomRue, map => map.MapFrom(vm => vm.nomRue))
               .ForMember(g => g.numRue, map => map.MapFrom(vm => vm.numRue))
               .ForMember(g => g.pays, map => map.MapFrom(vm => vm.pays))
               .ForMember(g => g.ville, map => map.MapFrom(vm => vm.ville));

            CreateMap<ApplicationTraceDTO, ApplicationTrace>()
               .ForMember(g => g.utilisateur, map => map.MapFrom(vm => vm.utilisateur))
               .ForMember(g => g.date, map => map.MapFrom(vm => vm.date))
               .ForMember(g => g.description, map => map.MapFrom(vm => vm.description))
               .ForMember(g => g.action, map => map.MapFrom(vm => vm.action));
        }
    }
}