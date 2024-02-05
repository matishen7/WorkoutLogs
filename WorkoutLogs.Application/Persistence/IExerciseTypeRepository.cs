using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Core;

namespace WorkoutLogs.Application.Persistence
{
    public interface IExerciseTypeRepository : IGenericRepository<ExerciseType>
    {
        public Task<bool> ExerciseTypeExists(int exerciseTypeId, CancellationToken cancellationToken);

    }
}
