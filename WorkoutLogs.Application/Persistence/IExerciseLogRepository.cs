﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Core;

namespace WorkoutLogs.Application.Persistence
{
    public interface IExerciseLogRepository : IGenericRepository<ExerciseLog>
    {
        public Task<IEnumerable<ExerciseLog>> GetExerciseLogsBySessionId(int id);
    }
}
