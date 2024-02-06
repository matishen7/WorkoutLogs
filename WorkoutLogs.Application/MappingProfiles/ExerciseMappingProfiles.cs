using AutoMapper;
using WorkoutLogs.Application.Contracts.Features.Exercise.Commands;
using WorkoutLogs.Core;

namespace WorkoutLogs.Application.MappingProfiles
{
    public class ExerciseMappingProfiles : Profile
    {
        public ExerciseMappingProfiles()
        {
            CreateMap<Exercise, CreateExerciseCommand>().ReverseMap();
        }
    }
}
