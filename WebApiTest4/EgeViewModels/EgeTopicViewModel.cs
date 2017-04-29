using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiTest4.Models.ExamsModels;

namespace WebApiTest4.EgeViewModels
{
    public class EgeTopicViewModel
    {
        public EgeTopicViewModel(TaskTopic sourseTopic)
        {
            id = sourseTopic.Id;
            title = sourseTopic.Name;
        }
        public int id { get; private set; }
        public string title { get; private set; }


    }
}