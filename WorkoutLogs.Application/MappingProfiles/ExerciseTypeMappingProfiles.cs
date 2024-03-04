using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Application.Contracts.Features.ExerciseTypes.Commands;
using WorkoutLogs.Application.Contracts.Features.ExerciseTypes.Queries;
using WorkoutLogs.Core;

namespace WorkoutLogs.Application.MappingProfiles
{
    public class ExerciseTypeMappingProfiles : Profile
    {
        public ExerciseTypeMappingProfiles()
        {
            CreateMap<ExerciseType, CreateExerciseTypeCommand>().ReverseMap();
            CreateMap<ExerciseTypeDto, ExerciseType>().ReverseMap();
        }
    }
}
