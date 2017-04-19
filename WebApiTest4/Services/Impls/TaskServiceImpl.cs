using System;
using System.Collections.Generic;
using System.Linq;
using WebApiTest4.EgeViewModels;
using WebApiTest4.EgeViewModels.BindingModels;
using WebApiTest4.Models.EgeModels;

namespace WebApiTest4.Services.Impls
{
    public class TaskServiceImpl : ITaskService
    {

        public TaskServiceImpl(EgeDbContext context)
        {
            _dbContext = context;
        }
        private EgeDbContext _dbContext;
        public EgeTaskViewModel GetTask(int id)
        {
            return _dbContext
                .Tasks
                .Where(x => x.Id == id)
                .ToList()
                .Select(x => new EgeTaskViewModel(x))
                .FirstOrDefault();
        }

        public IEnumerable<EgeTaskViewModel> GetSortedTasks(int? topicId, int offset, int limit)
        {
            //var user = _dbContext.Users.FirstOrDefault(x => x.Id == userId);

            return _dbContext
                .Tasks
                .Where(x => topicId == null || x.Topic.Id == topicId) //if there is no topic then just first tasks
                .OrderBy(x => x.Id)
                .Skip(offset)
                .Take(limit)
                .ToList()
                .Select(x => new EgeTaskViewModel(x));

            
        }

        public IEnumerable<int> CheckAnswers(string trainType, IEnumerable<TaskAnswerBindingModel> answers, int userId)
        {
                       
            
            var correctIds = new List<int>();
            var user = _dbContext.Users.FirstOrDefault(x => x.Id == userId);
            if (user != null)
            {
                var train = GetTrainByType(trainType);
                if (train == null) return correctIds;

                train.StartTime = DateTime.Now;
                train.FinishTime = DateTime.Now;
                train.User = user;
                _dbContext.Trains.Add(train);


                foreach (var answer in answers)
                {
                    var task = _dbContext.Tasks.FirstOrDefault(x => x.Id == answer.id);
                    if (task.Topic.IsShort)
                    {
                        var attempt = new UserTaskAttempt()
                        {
                            EgeTask = task,
                            Train = train,
                            UserAnswer = answer.answer,
                            Points = RateAnswer(task, answer.answer)
                        };
                        train.TaskAttempts.Add(attempt);
                        if (RatedAnswerIsCorrect(task, attempt))
                        {
                            correctIds.Add(task.Id);
                        }
                    }

                }
            }
            _dbContext.SaveChanges();
            return correctIds;
            
        }

        private static Train GetTrainByType(string trainType)
        {
            Train train;
            if (trainType.ToLower().Equals("free"))
            {
                train = new FreeTrain();
            }
            else
            {
                if (trainType.ToLower().Equals("ege"))
                {
                    train = new EgeTrain();
                }
                else
                {
                    return null;
                }
            }
            return train;
        }

        private bool RatedAnswerIsCorrect(EgeTask task, UserTaskAttempt attempt)
        {
            return task.Topic.PointsPerTask == attempt.Points;
        }
        

        private int RateAnswer(EgeTask task, string answer)
        {
            if (
                task.Answer.Trim()
                    .Replace(" ", "")
                    .ToLowerInvariant()
                    .Equals(answer.Trim().Replace(" ", "").ToLowerInvariant()))
            {
                return task.Topic.PointsPerTask;
            }
            else
            {
                return 0;
            }
        }
    }
}