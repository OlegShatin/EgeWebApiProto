using System;
using System.Collections.Generic;
using System.Linq;
using WebApiTest4.EgeViewModels;
using WebApiTest4.EgeViewModels.BindingModels;
using WebApiTest4.Models.ExamsModels;
using WebGrease.Css.Extensions;

namespace WebApiTest4.Services.Impls
{
    public class TaskServiceImpl : ITaskService
    {

        public TaskServiceImpl(ExamAppDbContext context)
        {
            _dbContext = context;
        }
        private ExamAppDbContext _dbContext;

        public EgeTaskViewModel GetTask(int id)
        {
            return _dbContext
                .Tasks
                .Where(x => x.Id == id)
                .ToList()
                .Select(x => new EgeTaskViewModel(x))
                .FirstOrDefault();
        }

        public IEnumerable<EgeTaskViewModel> GenerateNewExamTrain(int userId)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Id == userId);
            if (user != null)
            {
                var unfinishedTrain = user.Trains.OfType<ExamTrain>().TrainsOfUsersCurrentExamType().FirstOrDefault(x => x.FinishTime == null);
                if (unfinishedTrain != null)
                {
                    unfinishedTrain.FinishTime = DateTime.Now;
                }
                ExamTrain train = new ExamTrain();
                train.Exam = user.CurrentExam;
                train.User = user;
                train.StartTime = DateTime.Now;
                Random rnd = new Random();
                //get one task from each topic at fixed random examNumber position
                var examNumber = rnd.Next(
                    1, 
                    _dbContext
                        .TaskTopics
                        .First(x=> x.Exam.GetType() == user.CurrentExam.GetType())
                        .ExamTasks
                        .Count()
                    );

                train.TaskAttempts = _dbContext
                    .TaskTopics
                    .Where(x => x.Exam.GetType() == user.CurrentExam.GetType())
                    .ToList()
                    .Select(x => new UserTaskAttempt()
                    {
                        ExamTask = x.ExamTasks.ElementAt(examNumber)
                    })
                    .ToList();
                    
                //save to db
                _dbContext.Trains.Add(train);
                _dbContext.SaveChanges();

                return train
                    .TaskAttempts
                    .ToList()
                    .Select(x => new EgeTaskViewModel(x.ExamTask));
            }
            else
            {
                return null;
            }
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

        public dynamic CheckAnswers(string trainType, IEnumerable<TaskAnswerBindingModel> answers, int userId)
        {
                       
            
            var correctIds = new List<int>();
            var user = _dbContext.Users.FirstOrDefault(x => x.Id == userId);
            if (user != null)
            {
                var train = GetTrainByType(trainType, user);
                if (train == null) return new List<int>();
                
                train.User = user;
                _dbContext.Trains.Add(train);

                return CheckAnswersByTrain(train, answers);
                
            }
            
            return correctIds;
            
        }

        private IEnumerable<int> CheckAnswersByTrain(Train rawTrain, IEnumerable<TaskAnswerBindingModel> answers)
        {
            if (rawTrain.GetType() == typeof(FreeTrain))
            {
                return CheckFreeTrainAnswers((FreeTrain) rawTrain, answers);

            }
            else
            {
                if (rawTrain.GetType() == typeof(ExamTrain))
                {
                    return CheckEgeTrainAnswers((ExamTrain)rawTrain, answers);
                }
                else
                {
                    return new List<int>();
                }
            }
        }

        private dynamic CheckEgeTrainAnswers(ExamTrain train, IEnumerable<TaskAnswerBindingModel> answers)
        {
            //make answers join train attempts
            var matchesAnswers = train.TaskAttempts
                .Join(
                    answers,
                    attempt => attempt.ExamTask.Id,
                    ans => ans.id,
                    ((ta, ua) => new
                        {
                            answer = ua.answer,
                            attempt = ta,
                            task = ta.ExamTask
                        })
                    );
            //get only short tasks
            //todo: add teacher checking system for long type of task
            var shortAnswers = matchesAnswers
                .Where(x => x.task.Topic.IsShort);
            //rate answers
            shortAnswers
                .ForEach(x => x.attempt.UserAnswer = x.answer);
            shortAnswers
                .ForEach(x =>x.attempt.Points = RateAnswer(x.task, x.attempt.UserAnswer));
            //get right answers
            //todo: debug it
            _dbContext.SaveChanges();
            return shortAnswers
                .Select(x => new {task_id = x.task.Id, right_answer = x.task.Answer });
        }

        private IEnumerable<int> CheckFreeTrainAnswers(FreeTrain train, IEnumerable<TaskAnswerBindingModel> answers)
        {
            var correctIds = new List<int>();
            foreach (var answer in answers)
            {
                var task = _dbContext.Tasks.FirstOrDefault(x => x.Id == answer.id);
                if (task.Topic.IsShort)
                {
                    var attempt = new UserTaskAttempt()
                    {
                        ExamTask = task,
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
            return correctIds;

        }

        private static Train GetTrainByType(string trainType, User user)
        {
            Train train;
            if (trainType.ToLower().Equals("free"))
            {
                train = new FreeTrain();
                train.Exam = user.CurrentExam;
                train.StartTime = DateTime.Now;
                train.FinishTime = DateTime.Now;
            }
            else
            {
                if (trainType.ToLower().Equals("ege"))
                {
                    //searching unfinished ege trains
                    train = user.Trains.OfType<ExamTrain>().TrainsOfUsersCurrentExamType().FirstOrDefault(x => x.FinishTime == null);
                    if (train != null)
                    {
                        train.FinishTime = DateTime.Now;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            return train;
        }

        private bool RatedAnswerIsCorrect(ExamTask task, UserTaskAttempt attempt)
        {
            return task.Topic.PointsPerTask == attempt.Points;
        }
        

        private int RateAnswer(ExamTask task, string answer)
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