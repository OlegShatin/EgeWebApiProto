using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiTest4.Models.EgeModels;

namespace WebApiTest4.EgeViewModels
{
    public class EgeTopicViewModel
    {
        public EgeTopicViewModel(TaskTopic sourseTopic)
        {
            Id = sourseTopic.Id;
            Name = sourseTopic.Name;
        }
        public int Id { get; private set; }
        public string Name { get; private set; }


    }
}