using AutoMapper;
using Report.Data.Entites;
using Report.Types.DTOs;
using ReportAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemplateAPI.Models;

namespace ReportAPI
{
    public class ApiModelMappings :  Profile
    {
        public ApiModelMappings()
        {
            CreateMap<TemplateDTO, TemplateCreateInputModel>().ReverseMap();
            CreateMap<TemplateCreateInputModel, TemplateCreateOutputModel>().ReverseMap();
            CreateMap<ReportItemDTO, ReportItem>().ReverseMap();
            CreateMap<SortItemDTO, SortItem>().ReverseMap();
            CreateMap<FilterItemDTO, FilterItem>().ReverseMap();
           
        }
    }
}
