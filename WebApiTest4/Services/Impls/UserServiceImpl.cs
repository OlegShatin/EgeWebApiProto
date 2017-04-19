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
           return  GetRatedUserViewModels().FirstOrDefault(x => x.id == id);
        }

        public IEnumerable<UserViewModel> GetRatingForUser(int userId)
        {
            return GetRatedUserViewModels();
        }

        private  IEnumerable<UserViewModel> GetRatedUserViewModels()
        {
            return
                _context.Users
                    .Select(
                        x => new
                        {
                            user = x,
                            points = x.EgeTrains.Sum(etr => etr.Points)
                                     + x.FreeTrains
                                         .SelectMany(ftr => ftr.TaskAttempts)
                                         .GroupBy(ftta => ftta.EgeTask.Id)
                                         .Select(g => g.Max(t => t.Points))
                                         .Sum()
                        }
                    )
                    .Where(res => res.points != null)
                    .OrderByDescending(res => res.points)
                    .ToList()
                    .Select(
                        (res, i) => new UserViewModel(res.user, (i + 1), res.points.Value)
                    );

        }
    }
}