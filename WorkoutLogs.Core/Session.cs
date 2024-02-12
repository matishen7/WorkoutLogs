using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLogs.Core
{
    public class Session : BaseEntity
    {
        [Required]
        public DateTime Date { get; set; } = DateTime.Now;
        
        [Required]
        public int MemberId { get; set; }
        public bool Ended { get; set; }
        public IEnumerable<ExerciseLog> ExerciseLogs { get; set; }
    }
}
