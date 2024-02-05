using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLogs.Core
{
    public class Exercise : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public string? TutorialUrl { get; set; }
        [Required]
        public int ExerciseGroupId { get; set; }
        
        [ForeignKey("ExerciseGroupId")]
        public ExerciseGroup ExerciseGroup { get; set; }
    }
}
