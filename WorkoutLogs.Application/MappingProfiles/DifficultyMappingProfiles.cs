using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Application.Contracts.Features.Difficulty.Commands;
using WorkoutLogs.Application.Contracts.Features.ExerciseGroup.Commands;
using WorkoutLogs.Core;

namespace WorkoutLogs.Application.MappingProfiles
{
    public class DifficultyMappingProfiles : Profile
    {
        public DifficultyMappingProfiles()
        {
            CreateMap<Difficulty, CreateDifficultyCommand>().ReverseMap();
        }
    }
}
