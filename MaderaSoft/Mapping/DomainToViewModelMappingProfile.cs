using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Madera.Model;
using MaderaSoft.Models;

namespace MaderaSoft.Mapping
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        protected override void Configure()
        {
            CreateMap<Adresse, AdresseModel>();
        }
    }
}