﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Core;

namespace WorkoutLogs.Application.Persistence
{
    public interface IDifficultyRepository : IGenericRepository<Difficulty>
    {
        public Task<bool> DifficultyExists(int id, CancellationToken cancellationToken);
    }
}
