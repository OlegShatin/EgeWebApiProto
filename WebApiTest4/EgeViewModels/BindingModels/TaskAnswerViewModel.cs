using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApiTest4.Models.EgeModels;

namespace WebApiTest4.EgeViewModels.BindingModels
{
    public class TaskAnswersSetBindingModel
    {
        [Required]
        public List<TaskAnswerBindingModel> list { get; set; }
    }
    public class TaskAnswerBindingModel
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string answer { get;  set; }
    }
}