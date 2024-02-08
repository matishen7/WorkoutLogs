using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLogs.Application.Contracts.Features.Sessions.Commands
{
   public class CreateSessionCommand : IRequest<int>
    {
        public int MemberId { get; set; }
    }

}
