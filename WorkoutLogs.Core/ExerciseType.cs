using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLogs.Core
{
    public class ExerciseType : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int PhaseId { get; set; }

        [ForeignKey("PhaseId")]
        public Phase Phase { get; set; }
    }
}
