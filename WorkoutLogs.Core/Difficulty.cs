using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLogs.Core
{
    public class Difficulty : BaseEntity
    {
        [Required]
        public string Level { get; set; }
    }
}
