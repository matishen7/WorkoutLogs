using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Core;

namespace WorkoutLogs.Application.Persistence
{
    public interface IMemberRepository : IGenericRepository<Member>
    {
        public Task<bool> MemberExists(int memberId, CancellationToken cancellationToken);

    }
}
