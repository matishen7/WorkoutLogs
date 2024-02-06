using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLogs.Application.Contracts.Features.Difficulties.Commands
{
    public class CreateDifficultyCommand : IRequest<int>
    {
        public string Level { get; set; }
    }

}
