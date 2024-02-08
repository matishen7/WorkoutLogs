using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Application.Contracts.Features.ExerciseGroups.Commands;
using WorkoutLogs.Application.Contracts.Features.ExerciseTypes.Commands;
using WorkoutLogs.Core;

namespace WorkoutLogs.Application.MappingProfiles
{
    public class ExerciseGroupMappingProfiles : Profile
    {
        public ExerciseGroupMappingProfiles()
        {
            CreateMap<ExerciseGroup, CreateExerciseGroupCommand>().ReverseMap();
            CreateMap<ExerciseGroup, UpdateExerciseGroupCommand>().ReverseMap();
        }
    }
}
