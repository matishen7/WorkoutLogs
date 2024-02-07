using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLogs.Application.Contracts.Features.Difficulties.Queries
{
    public class GetDifficultyByIdQuery : IRequest<DifficultyDto>
    {
        public int Id { get; set; }
    }

}
