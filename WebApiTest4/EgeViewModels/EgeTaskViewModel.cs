using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiTest4.Models.ExamsModels;

namespace WebApiTest4.EgeViewModels
{
    public class EgeTaskViewModel
    {
        public EgeTaskViewModel(ExamTask sourseTask)
        {
            id = sourseTask.Id;
            text = sourseTask.Text;
            topic_id = sourseTask.Topic.Id;
            type = sourseTask.Topic.IsShort ? 0 : 1;
        }

        public int topic_id { get; set; }
        public int type { get; set; }
        public int id { get; private set; }
        public string text { get; private set; }


    }
}