using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLogs.Application.Contracts.Features.Difficulties.Queries
{
    public class GetAllDifficultiesQuery : IRequest<IEnumerable<DifficultyDto>>
    {
    }

}
