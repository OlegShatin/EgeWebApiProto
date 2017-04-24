﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiTest4.Models.EgeModels
{
    //[Table("Train")]
    public class Train
    {
        public Train()
        {
            StartTime = DateTime.Now;
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        
        public DateTime? StartTime { get; set; }

        public DateTime? FinishTime { get; set; }

        //[Required]
        //public virtual TrainType Type { get; set; }

        [Required]
        public virtual User User { get; set; }

        public virtual List<UserTaskAttempt> TaskAttempts { get; set; } = new List<UserTaskAttempt>();
    }
    //[Table("EgeTrain")]
    public class EgeTrain : Train
    {
        [DefaultValue(0)]
        public int? Points { get; set; }
    }
    //[Table("FreeTrain")]
    public class FreeTrain : Train
    {
        
    }

}