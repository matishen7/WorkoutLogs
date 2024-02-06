using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Application.Contracts.Features.Exercise;
using WorkoutLogs.Application.Contracts.Features.ExerciseGroup;
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
