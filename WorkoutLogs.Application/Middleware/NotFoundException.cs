using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLogs.Application.Middleware
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string entityName, int entityId)
       : base($"{entityName} with ID {entityId} not found.")
        {
        }
    }
}
