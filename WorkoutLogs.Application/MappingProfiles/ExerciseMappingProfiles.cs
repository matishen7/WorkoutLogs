using AutoMapper;
using WorkoutLogs.Application.Contracts.Features.Exercises.Commands;
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
