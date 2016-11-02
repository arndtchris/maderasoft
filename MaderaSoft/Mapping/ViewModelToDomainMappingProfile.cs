using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace MaderaSoft.Mapping
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        protected override void Configure()
        {
        }
    }
}