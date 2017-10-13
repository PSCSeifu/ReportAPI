using AutoMapper;
using Report.Data.Entites;
using Report.Types.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Report.Service
{
    public class ServiceModelMappings : Profile
    {
        public ServiceModelMappings()
        {
            CreateMap<TemplateEntity, TemplateDTO>().ReverseMap();
            CreateMap<ReportItemEntity, ReportItemDTO>().ReverseMap();
            CreateMap<SortItemEntity, SortItemDTO>().ReverseMap();
            CreateMap<FilterItemEntity, FilterItemDTO>().ReverseMap();
        }
    }
}
