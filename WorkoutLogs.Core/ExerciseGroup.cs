﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLogs.Core
{
    public class ExerciseGroup : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public int ExerciseTypeId { get; set; }
        
        [ForeignKey("ExerciseTypeId")]
        public ExerciseType ExerciseType { get; set; }
    }
}
