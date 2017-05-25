using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiTest4.ApiViewModels;
using WebApiTest4.Models.ExamsModels;

namespace WebApiTest4.Services.Impls
{
    public class SolvedTasksServiceImpl : ISolvedTasksService
    {
        private readonly ExamAppDbContext _context;
        public SolvedTasksServiceImpl(ExamAppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<AttemptViewModel> GetUncheckedAttemptsOfMyStudents(int teacher_id, int offset, int limit)
        {
            var teacher = _context.Users.OfRole("teacher").FirstOrDefault(x => x.Id == teacher_id);
            if (teacher != null)
            {
                return
                    teacher.Students.SelectMany(
                            student =>
                                student.Trains.SelectMany(
                                    train =>
                                        train.TaskAttempts.OfType<UserManualCheckingTaskAttempt>()
                                            .Where(attempt => !attempt.IsChecked && attempt.UserAnswer != null)))
                                            .OrderByDescending(attempt => attempt.Train.FinishTime)
                                            .Skip(offset)
                                            .Take(limit)
                        .ToList()
                        .Select(attempt => new AttemptViewModel(attempt));
            }
            return null;
        }
    }
}