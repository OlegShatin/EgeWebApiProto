using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiTest4.EgeViewModels;

namespace WebApiTest4.Services
{
    public interface ITopicService
    {
        IEnumerable<EgeTopicViewModel> GetTopics();
    }
}
