using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiTest4.Models.EgeModels;

namespace WebApiTest4.EgeViewModels
{
    public class EgeTaskViewModel
    {
        public EgeTaskViewModel(EgeTask sourseTask)
        {
            Id = sourseTask.Id;
            Text = sourseTask.Text;
            EgeTopicViewModel = new EgeTopicViewModel(sourseTask.Topic);
        }

        public EgeTopicViewModel EgeTopicViewModel { get; set; }
        public int Id { get; private set; }
        public string Text { get; private set; }


    }
}