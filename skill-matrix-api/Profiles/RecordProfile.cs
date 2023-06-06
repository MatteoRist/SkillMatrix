using AutoMapper;

namespace skill_matrix_api.Profiles
{
    public class RecordProfile : Profile
    {
        public RecordProfile() 
        { 
            CreateMap<Entities.Record, Models.RecordGetDto>();
            CreateMap<Models.RecordPostDto, Entities.Record>();
        }
    }
}
