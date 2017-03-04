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
            CreateMap<Adresse, AdresseDTO>();
            CreateMap<DevisFacture, DevisFactureDTO>();
            CreateMap<ApplicationTrace, ApplicationTraceDTO>();
            CreateMap<Utilisateur, UtilisateurDTO>();

            CreateMap<Personne, PersonneDTO>();
            CreateMap<Personne, PersonneSimpleDTO>();
            CreateMap<Module, Areas.GestionModule.Models.DTOs.ModuleDTO>();

            CreateMap<Employe, EditEmployeDTO>().MaxDepth(1);
            CreateMap<Employe, EmployeSimpleDTO>();
            CreateMap<Employe, EmployeDTO>()
                .Include<Employe, EditEmployeDTO>().MaxDepth(1);

            CreateMap<Droit, DroitDTO>();
            CreateMap<Service, ServiceDTO>();
            CreateMap<TEmploye, TEmployeDTO>();

            CreateMap<AffectationService, AffectationServiceDTO>().MaxDepth(1);

            CreateMap<Personne, PEmployeTableauDTO>();
        }
    }
}