using WorkoutLogs.Presentation.Models.Exercise;
using AutoMapper;
using WorkoutLogs.Presentation.Services.Base;

namespace WorkoutLogs.Presentation.MappingProfiles
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<ExerciseDto, ExerciseVM >().ReverseMap();
        }
    }
}
