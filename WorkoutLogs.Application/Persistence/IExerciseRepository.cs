using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Core;

namespace WorkoutLogs.Application.Persistence
{
    public interface IExerciseRepository : IGenericRepository<Exercise>
    {
        public Task<bool> ExerciseExists(int id, CancellationToken cancellationToken);
        public Task<IEnumerable<Exercise>> GetByGroupIdAsync(int exerciseGroupId);
    }
}
