using WorkoutLogs.Presentation.Models.Exercise;
using WorkoutLogs.Presentation.Services.Base;
using AutoMapper;
using WorkoutLogs.Application.Contracts.Features.Exercises.Queries;

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
