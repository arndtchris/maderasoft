using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Madera.Model;
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
            CreateMap<AdresseDTO, Adresse>().MaxDepth(1)
               .ForMember(g => g.id, map => map.MapFrom(vm => vm.id))
               .ForMember(g => g.codePostal, map => map.MapFrom(vm => vm.codePostal))
               .ForMember(g => g.nomRue, map => map.MapFrom(vm => vm.nomRue))
               .ForMember(g => g.numRue, map => map.MapFrom(vm => vm.numRue))
               .ForMember(g => g.pays, map => map.MapFrom(vm => vm.pays))
               .ForMember(g => g.ville, map => map.MapFrom(vm => vm.ville));

            CreateMap<AffectationServiceDTO, AffectationService>().MaxDepth(1)
                .ForMember(g => g.id, map => map.MapFrom(vm => vm.id))
                .ForMember(g => g.employe, map => map.MapFrom(vm => vm.employe)).MaxDepth(1)
                .ForMember(g => g.groupe, map => map.MapFrom(vm => vm.groupe)).MaxDepth(1)
                .ForMember(g => g.isPrincipal, map => map.MapFrom(vm => vm.isPrincipal))
                .ForMember(g => g.service, map => map.MapFrom(vm => vm.service)).MaxDepth(1);

            CreateMap<DevisFactureDTO, DevisFacture>().MaxDepth(1)
                .ForMember(g => g.id, map => map.MapFrom(vm => vm.id))
                .ForMember(g => g.isSigned, map => map.MapFrom(vm => vm.isSigned))
                .ForMember(g => g.isDeleted, map => map.MapFrom(vm => vm.isDeleted))
                .ForMember(g => g.projet, map => map.MapFrom(vm => vm.projet)).MaxDepth(1)
                .ForMember(g => g.referent, map => map.MapFrom(vm => vm.employe)).MaxDepth(1);

            CreateMap<ApplicationTraceDTO, ApplicationTrace>().MaxDepth(1)
               .ForMember(g => g.utilisateur, map => map.MapFrom(vm => vm.utilisateur)).MaxDepth(1)
               .ForMember(g => g.date, map => map.MapFrom(vm => vm.date))
               .ForMember(g => g.description, map => map.MapFrom(vm => vm.description))
               .ForMember(g => g.action, map => map.MapFrom(vm => vm.action));

            CreateMap<DroitDTO, Droit>().MaxDepth(1)
                .ForMember(g => g.create, map => map.MapFrom(vm => vm.create))
                .ForMember(g => g.delete, map => map.MapFrom(vm => vm.delete))
                .ForMember(g => g.read, map => map.MapFrom(vm => vm.read))
                .ForMember(g => g.update, map => map.MapFrom(vm => vm.update))
                .ForMember(g => g.libe, map => map.MapFrom(vm => vm.libe));

            CreateMap<ServiceDTO, Service>().MaxDepth(1)
               .ForMember(g => g.libe, map => map.MapFrom(vm => vm.libe));

            CreateMap<TEmployeDTO, TEmploye>().MaxDepth(1)
               .ForMember(g => g.libe, map => map.MapFrom(vm => vm.libe));

            CreateMap<UtilisateurDTO, Utilisateur>().MaxDepth(1)
               .ForMember(g => g.id, map => map.MapFrom(vm => vm.id))
               .ForMember(g => g.dConnexion, map => map.MapFrom(vm => vm.dConnexion))
               .ForMember(g => g.dCreation, map => map.MapFrom(vm => vm.dCreation))
               .ForMember(g => g.isActive, map => map.MapFrom(vm => vm.isActive))
               .ForMember(g => g.isDeleted, map => map.MapFrom(vm => vm.isDeleted))
               .ForMember(g => g.login, map => map.MapFrom(vm => vm.login))
               .ForMember(g => g.password, map => map.MapFrom(vm => vm.password))
               .ForMember(g => g.isFirstConnexion, map => map.MapFrom(vm => vm.isFirstConnexion));

            #region Personne


            CreateMap<PersonneDTO, Personne>().MaxDepth(1)
               .ForMember(g => g.id, map => map.MapFrom(vm => vm.id))
               .ForMember(g => g.isDeleted, map => map.MapFrom(vm => vm.isDeleted))
               .ForMember(g => g.civ, map => map.MapFrom(vm => vm.civ))
               .ForMember(g => g.nom, map => map.MapFrom(vm => vm.nom))
               .ForMember(g => g.prenom, map => map.MapFrom(vm => vm.prenom))
               .ForMember(g => g.email, map => map.MapFrom(vm => vm.email))
               .ForMember(g => g.tel1, map => map.MapFrom(vm => vm.tel1))
               .ForMember(g => g.tel2, map => map.MapFrom(vm => vm.tel2))
               .ForMember(g => g.adresse, map => map.MapFrom(vm => vm.adresse)).MaxDepth(1)
               .ForMember(g => g.utilisateur, map => map.MapFrom(vm => vm.utilisateur)).MaxDepth(1);

            CreateMap<PersonneSimpleDTO, Personne>().MaxDepth(1)
               .ForMember(g => g.id, map => map.MapFrom(vm => vm.id))
               .ForMember(g => g.isDeleted, map => map.MapFrom(vm => vm.isDeleted))
               .ForMember(g => g.civ, map => map.MapFrom(vm => vm.civ))
               .ForMember(g => g.nom, map => map.MapFrom(vm => vm.nom))
               .ForMember(g => g.prenom, map => map.MapFrom(vm => vm.prenom))
               .ForMember(g => g.email, map => map.MapFrom(vm => vm.email))
               .ForMember(g => g.tel1, map => map.MapFrom(vm => vm.tel1))
               .ForMember(g => g.tel2, map => map.MapFrom(vm => vm.tel2));

            CreateMap<PEmployeTableauDTO, Personne>().MaxDepth(1)
               .ForMember(g => g.civ, map => map.MapFrom(vm => vm.civ))
               .ForMember(g => g.nom, map => map.MapFrom(vm => vm.nom))
               .ForMember(g => g.prenom, map => map.MapFrom(vm => vm.prenom));

            #endregion

            #region Employé
            CreateMap<EmployeDTO, Employe>().MaxDepth(1)
               .ForMember(g => g.id, map => map.MapFrom(vm => vm.id))
               .ForMember(g => g.typeEmploye, map => map.MapFrom(vm => vm.typeEmploye))
               .ForMember(g => g.affectationServices, map => map.MapFrom(vm => vm.affectationServices))
               .ForMember(g => g.utilisateur, map => map.MapFrom(vm => vm.utilisateur)).MaxDepth(1)
               .ForMember(g => g.adresse, map => map.MapFrom(vm => vm.adresse)).MaxDepth(1);

            CreateMap<EditEmployeDTO, Employe>().MaxDepth(1)
               .ForMember(g => g.id, map => map.MapFrom(vm => vm.id))
               .ForMember(g => g.typeEmploye, map => map.MapFrom(vm => vm.typeEmploye))
               .ForMember(g => g.affectationServices, map => map.MapFrom(vm => vm.affectationServices))
               .ForMember(g => g.adresse, map => map.MapFrom(vm => vm.adresse));

            CreateMap<EmployeSimpleDTO, Employe>().MaxDepth(1)
               .ForMember(g => g.id, map => map.MapFrom(vm => vm.id))
               .ForMember(g => g.typeEmploye, map => map.MapFrom(vm => vm.typeEmploye));

            #endregion 




        }
    }
}