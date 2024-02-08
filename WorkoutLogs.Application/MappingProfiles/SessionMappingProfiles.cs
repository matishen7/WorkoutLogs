using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Application.Contracts.Features.Sessions.Commands;
using WorkoutLogs.Core;

namespace WorkoutLogs.Application.MappingProfiles
{
    public class SessionMappingProfiles : Profile
    {
        public SessionMappingProfiles()
        {
            CreateMap<Session, CreateSessionCommand>().ReverseMap();
        }
    }
}
