using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiTest4.Models.ExamsModels;

namespace WebApiTest4.Services
{
    public interface ISchoolService
    {
        School GetSchoolById(int schoolId);
    }
}
