using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiTest4.Models.EgeModels
{
    enum TrainTypes
    {
        EGE, ONE_TOPIC, FREETRAIN
    }
    [Table("TrainType")]
    public class TrainType
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(250)]
        public string Name { get; set; }

        public virtual List<Train> Trains { get; set; }
    }
}