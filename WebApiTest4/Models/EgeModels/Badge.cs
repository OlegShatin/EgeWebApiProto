using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApiTest4.Models.EgeModels
{
    [Table("Badge")]
    public class Badge
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ImageSrc { get; set; }
        [DefaultValue("")]
        public string Description { get; set; }

        public virtual List<User> Owners { get; set; }

    }
}