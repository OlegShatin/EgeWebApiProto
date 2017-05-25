using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using WebApiTest4.ApiViewModels;
using WebApiTest4.Models.ExamsModels;
using WebApiTest4.Util;

namespace WebApiTest4.Services.Impls
{
    public class UserServiceImpl : IUserService
    {
        private readonly ExamAppDbContext _context;

        public UserServiceImpl(ExamAppDbContext context)
        {
            _context = context;
        }

        public UserViewModel GetUser(int id)
        {
            //if this user is teacher return vm without ratings and points
            return
                _context.Users.Where(x => x.Id == id)
                    .OfRole("teacher")
                    .ToList()
                    .Select(x => new UserViewModel(x, null, null, 0))
                    .FirstOrDefault() ??
                GetRatedUserViewModels().FirstOrDefault(x => x.id == id);
        }

        public IEnumerable<UserViewModel> GetRatingForUser(int userId)
        {
            return GetRatedUserViewModels();
        }

        private IEnumerable<UserViewModel> GetRatedUserViewModels()
        {
            return _context.Users
                .OfRole("student")
                .Select(
                    x => new
                    {
                        user = x,
                        points = x.Trains.Any()
                            ? ((x.Trains.OfType<ExamTrain>().Any()
                                   ? x.Trains.OfType<ExamTrain>()
                                       .SelectMany(train => train.TaskAttempts)
                                       .Sum(attempt => attempt.Points)
                                   : 0)
                               + (x.Trains.OfType<FreeTrain>().Any()
                                   ? x.Trains.OfType<FreeTrain>()
                                       .SelectMany(ftr => ftr.TaskAttempts)
                                       .GroupBy(ftta => ftta.ExamTask.Id, (z, y) => new {id = z, attempts = y})
                                       .Select(g => g.attempts.Max(t => t.Points))
                                       .Sum()
                                   : 0)
                            )
                            : 0
                    }
                )
                .OrderByDescending(res => res.points)
                .ToList()
                .Select(
                    (res, i) =>
                        new UserViewModel(res.user, (i + 1), res.points,
                            Helpers.GetEgePoints(
                                res.user.Trains.OfType<ExamTrain>()
                                    .Max(train => train.TaskAttempts.Sum(attempt => attempt.Points))))
                );
        }

        public void AddCurrentExam(User user, Type type)
        {
            //to be sure users exist in the same context
            var dbUser = _context.Users.FirstOrDefault(x => x.Id == user.Id);
            var exam = _context.Exams.AsEnumerable().FirstOrDefault(x => x.GetType() == type);
            dbUser.CurrentExam = _context.Exams.FirstOrDefault(x => x.Id == exam.Id);
            _context.SaveChanges();
        }

        public User GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public TeacherViewModel GetTeacherVMById(int id)
        {
            return new TeacherViewModel(GetUserById(id));
        }

        public IEnumerable<TeacherViewModel> GetTeachers()
        {
            return _context
                .Users
                .OfRole("teacher")
                .ToList()
                .Select(x => new TeacherViewModel(x));
        }

        public bool UserExists(int id)
        {
            return GetUserById(id) != null;
        }
    }
}