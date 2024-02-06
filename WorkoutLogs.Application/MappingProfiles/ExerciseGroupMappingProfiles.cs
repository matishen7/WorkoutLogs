using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Application.Contracts.Features.ExerciseGroup.Commands;
using WorkoutLogs.Application.Contracts.Features.ExerciseType.Commands;
using WorkoutLogs.Core;

namespace WorkoutLogs.Application.MappingProfiles
{
    public class ExerciseGroupMappingProfiles : Profile
    {
        public ExerciseGroupMappingProfiles()
        {
            CreateMap<ExerciseGroup, CreateExerciseGroupCommand>().ReverseMap();
        }
    }
}
