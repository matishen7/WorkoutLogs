using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Application.Persistence;
using WorkoutLogs.Core;
using WorkoutLogs.Persistence.DbContexts;

namespace WorkoutLogs.Persistence.Repositories
{
    public class MemberRepository : GenericRepository<Member>, IMemberRepository
    {
        public MemberRepository(WorkoutLogsDbContext context) : base(context)
        {
        }

        public async Task<bool> MemberExists(int memberId, CancellationToken cancellationToken)
        {
            var member = _context.Members
           .FirstOrDefault(x => x.Id == memberId);
            return member != null;
        }
    }
}
