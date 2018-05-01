using AspNetBasics.Controllers.Resources;
using AspNetBasics.Models;
using AutoMapper;

namespace AspNetBasics.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile(){
            CreateMap<Make, MakeResource>();
            CreateMap<Model, ModelResource>();
            CreateMap<Feature, FeatureResource>();
        }
    }
}