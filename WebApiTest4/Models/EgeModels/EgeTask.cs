using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApiTest4.Models.EgeModels
{
    [Table("EgeTask")]
    public class EgeTask
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int Number { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public string Answer { get; set; }
        [Required]
        public virtual TaskTopic Topic { get; set; }
        

        public virtual List<UserTaskAttempt> UserTaskAttempts { get; set; }

    }
}