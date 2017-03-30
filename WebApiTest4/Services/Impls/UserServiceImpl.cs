using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiTest4.EgeViewModels;
using WebApiTest4.Models.EgeModels;

namespace WebApiTest4.Services.Impls
{
    public class UserServiceImpl : IUserService
    {
        private readonly EgeDbContext _context;
        public UserServiceImpl(EgeDbContext context)
        {
            _context = context;
        }
        public UserViewModel GetUser(int id)
        {

            return
                _context.Users
                    .OrderBy(x => x.Points)
                    .ToList()
                    .Select((x, i) => new UserViewModel(x, i))
                    .FirstOrDefault(x => x.id == id);
        }
    }
}