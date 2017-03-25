using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Web;

namespace WebApiTest4.Models.EgeModels
{
    [Table("TaskTopic")]
    public class TaskTopic
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(250)]
        public string Name { get; set; }
        [Index(IsUnique = true)]
        public int Number { get; set; }
        [DefaultValue(0)]
        public int PointsPerTask { get; set; }

        public virtual List<EgeTask> EgeTasks { get; set; }

    }
}