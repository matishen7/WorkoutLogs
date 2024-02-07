using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Application.Contracts.Features.Difficulties.Commands;
using WorkoutLogs.Application.Contracts.Features.ExerciseGroups.Commands;
using WorkoutLogs.Core;

namespace WorkoutLogs.Application.MappingProfiles
{
    public class DifficultyMappingProfiles : Profile
    {
        public DifficultyMappingProfiles()
        {
            CreateMap<Difficulty, CreateDifficultyCommand>().ReverseMap();
            CreateMap<Difficulty, UpdateDifficultyCommand>().ReverseMap();
        }
    }
}
