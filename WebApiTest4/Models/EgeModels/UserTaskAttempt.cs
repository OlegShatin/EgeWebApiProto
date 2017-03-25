using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiTest4.Models.EgeModels
{
    [Table("UserTaskAttempt")]
    public class UserTaskAttempt
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string UserAnswer { get; set; }
        [Required]
        [DefaultValue(0)]
        public int Points { get; set; }
        [Required]
        public virtual EgeTask EgeTask { get; set; }
        [Required]
        public virtual Train Train { get; set; }
    }
}