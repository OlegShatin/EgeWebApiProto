using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiTest4.Models.ExamsModels
{
    
    public class UserTaskAttempt
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string UserAnswer { get; set; }
        [Required]
        [DefaultValue(0)]
        public int Points { get; set; }
        [Required]
        public virtual ExamTask ExamTask { get; set; }
        [Required]
        public virtual Train Train { get; set; }
    }

    public class UserSimpleTaskAttempt : UserTaskAttempt
    {
        
    }

    public class UserManualCheckingTaskAttempt : UserTaskAttempt
    {
        public DateTime? CheckTime { get; set; }
        [DefaultValue(false)]
        public bool IsChecked { get; set; }
        public virtual User Reviewer { get; set; }
    }

}