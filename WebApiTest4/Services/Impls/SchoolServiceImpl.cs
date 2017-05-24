using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiTest4.Models.ExamsModels;

namespace WebApiTest4.Services.Impls
{
    public class SchoolServiceImpl : ISchoolService
    {
        private readonly ExamAppDbContext _context;
        public SchoolServiceImpl(ExamAppDbContext context)
        {
            _context = context;
        }
        public School GetSchoolById(int schoolId)
        {
            return _context.Schools.FirstOrDefault(x => x.Id == schoolId);
        }
    }
}