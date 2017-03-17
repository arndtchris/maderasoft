using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Madera.Model;
using MaderaSoft.Models.DTO;

namespace MaderaSoft.Mapping
{
    public class DomainToViewModelMappingProfile : Profile
    {
        /*
         * Permet d'établir les correspondances entre les Plain Object (représentant direct d'une table en bdd) 
         * et les DTOs de la couche métier
         * 
         */ 

        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        protected override void Configure()
        {
            CreateMap<Adresse, AdresseDTO>().MaxDepth(1);
            CreateMap<DevisFacture, DevisFactureDTO>().MaxDepth(1);
            CreateMap<ApplicationTrace, ApplicationTraceDTO>().MaxDepth(1);
            CreateMap<Utilisateur, UtilisateurDTO>().MaxDepth(1);

            CreateMap<Personne, PersonneDTO>().MaxDepth(1);
            CreateMap<Personne, PersonneSimpleDTO>().MaxDepth(1);

            CreateMap<Employe, EditEmployeDTO>().MaxDepth(1);
            CreateMap<Employe, EmployeSimpleDTO>().MaxDepth(1);
            CreateMap<Employe, EmployeDTO>().MaxDepth(1)
                .Include<Employe, EditEmployeDTO>().ForMember(g => g.adresse, map => map.MapFrom(vm => vm.adresse)).MaxDepth(1);

            CreateMap<Droit, DroitDTO>().MaxDepth(1);
            CreateMap<Service, ServiceDTO>().MaxDepth(1);
            CreateMap<TEmploye, TEmployeDTO>().MaxDepth(1);

            CreateMap<AffectationService, AffectationServiceDTO>().MaxDepth(1);

            CreateMap<Personne, PEmployeTableauDTO>().MaxDepth(1);
            CreateMap<AffectationService, AffectationServiceDTO>().MaxDepth(1);

            CreateMap<Plan, PlanDTO>();

            CreateMap<Etage, EtageDTO>().MaxDepth(1);

            CreateMap<PositionModule, PositionModuleDTO>().MaxDepth(1);

            CreateMap<Module, ModuleDTO>().MaxDepth(1);
            CreateMap<TModule, TModuleDTO>().MaxDepth(1);
            CreateMap<Composant, ComposantDTO>().MaxDepth(1);
            CreateMap<Composition, CompositionDTO>().MaxDepth(1);

        }
    }
}