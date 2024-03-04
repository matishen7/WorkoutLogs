using AutoMapper;
using WorkoutLogs.Application.Contracts.Features.ExerciseLogs.Commands;
using WorkoutLogs.Application.Contracts.Features.ExerciseLogs.Queries;
using WorkoutLogs.Core;

namespace WorkoutLogs.Application.MappingProfiles
{
    public class ExerciseLogMappingProfiles : Profile
    {
        public ExerciseLogMappingProfiles()
        {
            CreateMap<ExerciseLog, CreateExerciseLogCommand>().ReverseMap();
            CreateMap<ExerciseLog, ExerciseLogDto>().ReverseMap();
        }
    }
}
