using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Madera.Model;
using MaderaSoft.Models;
using MaderaSoft.Models.DTO;

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

            CreateMap<DevisFactureDTO, DevisFacture>()
                .ForMember(g => g.id, map => map.MapFrom(vm => vm.id))
                .ForMember(g => g.isSigned, map => map.MapFrom(vm => vm.isSigned))
                .ForMember(g => g.isDeleted, map => map.MapFrom(vm => vm.isDeleted))
                .ForMember(g => g.projet, map => map.MapFrom(vm => vm.projet))
                .ForMember(g => g.referent, map => map.MapFrom(vm => vm.employe));

            CreateMap<ApplicationTraceDTO, ApplicationTrace>()
               .ForMember(g => g.utilisateur, map => map.MapFrom(vm => vm.utilisateur))
               .ForMember(g => g.date, map => map.MapFrom(vm => vm.date))
               .ForMember(g => g.description, map => map.MapFrom(vm => vm.description))
               .ForMember(g => g.action, map => map.MapFrom(vm => vm.action));

            CreateMap<UtilisateurDTO, Utilisateur>()
               .ForMember(g => g.dConnexion, map => map.MapFrom(vm => vm.dConnexion))
               .ForMember(g => g.dCreation, map => map.MapFrom(vm => vm.dCreation))
               .ForMember(g => g.isActive, map => map.MapFrom(vm => vm.isActive))
               .ForMember(g => g.isDeleted, map => map.MapFrom(vm => vm.isDeleted))
               .ForMember(g => g.login, map => map.MapFrom(vm => vm.login))
               .ForMember(g => g.password, map => map.MapFrom(vm => vm.password));
        }
    }
}