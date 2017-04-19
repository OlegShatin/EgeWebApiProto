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
            //todo: remove mock
            return new UserViewModel(new User(), 0,0);
            /*
            return
                _context.Users
                    .OrderBy(x => x.Trains.Sum(t => t.TaskAttempts.Sum(a => a.Points)))
                    .ToList()
                    .Select((x, i) => new UserViewModel(x, (i + 1)))
                    .FirstOrDefault(x => x.id == id);
                    */
        }

        public IEnumerable<UserViewModel> GetRatingForUser(int userId)
        {
            //todo: remove mock
            return new List<UserViewModel>();
            /*
            return
                _context.Users
                    .OrderByDescending(x => x.Trains.Sum(t => t.TaskAttempts.Sum(a => a.Points)))
                    .ToList()
                    .Select((x, i) => new UserViewModel(x, (i + 1)));
                    */
        }
    }
}