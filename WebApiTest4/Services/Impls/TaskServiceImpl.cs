﻿using System;
using System.Collections.Generic;
using System.Linq;
using Org.BouncyCastle.Asn1.X509;
using WebApiTest4.ApiViewModels;
using WebApiTest4.ApiViewModels.BindingModels;
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

        public ExamTaskViewModel GetTask(int id)
        {
            return _dbContext
                .Tasks
                .Where(x => x.Id == id)
                .ToList()
                .Select(x => new ExamTaskViewModel(x))
                .FirstOrDefault();
        }

        public IEnumerable<ExamTaskViewModel> GenerateNewExamTrain(int userId)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Id == userId);
            if (user != null)
            {
                var unfinishedTrain = user.Trains.OfType<ExamTrain>().TrainsOfUsersCurrentExamType()?.FirstOrDefault(x => x.FinishTime == null);
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
                var lastIndexOfTasksList = _dbContext
                    .TaskTopics
                    .FirstOrDefault(x => x.Exam.Id == user.CurrentExam.Id)?
                    .ExamTasks
                    .Count()?? 0;
                if (lastIndexOfTasksList == 0)
                {
                    return new List<ExamTaskViewModel>();
                }
                var examNumber = rnd.Next(
                    0, 
                    lastIndexOfTasksList - 1
                    );
                train.TaskAttempts = _dbContext
                    .TaskTopics
                    .Where(x => x.Exam.Id == user.CurrentExam.Id)
                    .ToList()
                    .Select(x => GenerateNewEmptyAttempt(x.ExamTasks.ElementAt(examNumber)))
                    .ToList();
                    
                //save to db
                _dbContext.Trains.Add(train);
                _dbContext.SaveChanges();

                return train
                    .TaskAttempts
                    .ToList()
                    .Select(x => new ExamTaskViewModel(x.ExamTask));
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<ExamTaskViewModel> GetSortedTasks(int? topicId, int offset, int limit)
        {
            //var user = _dbContext.Users.FirstOrDefault(x => x.Id == userId);

            return _dbContext
                .Tasks
                .Where(x => topicId == null || x.Topic.Id == topicId) //if there is no topic then just first tasks
                .OrderBy(x => x.Id)
                .Skip(offset)
                .Take(limit)
                .ToList()
                .Select(x => new ExamTaskViewModel(x));

            
        }

        public IEnumerable<AnswerViewModel> CheckAnswers(string trainType, IEnumerable<TaskAnswerBindingModel> answers, int userId)
        {
            
            var empty = new List<AnswerViewModel>();
            var user = _dbContext.Users.FirstOrDefault(x => x.Id == userId);
            if (user != null)
            {
                var train = GetTrainByType(trainType, user);
                if (train == null) return empty;
                train.User = user;
                var trainWithAttempts = AddAttemptsToTrain(train, answers);
                var result = CheckAnswers(trainWithAttempts);
                _dbContext.SaveChanges();
                return result;

            }
            return empty;
        }

        

        private Train AddAttemptsToTrain(Train rawTrain, IEnumerable<TaskAnswerBindingModel> answers)
        {
            if (rawTrain.GetType() == typeof(FreeTrain))
            {
                return AddAttemptsToFreeTrain((FreeTrain) rawTrain, answers);

            }
            else
            {
                if (rawTrain.GetType() == typeof(ExamTrain))
                {
                    return AddAttemptsToExamTrain((ExamTrain)rawTrain, answers);
                }
                else
                {
                    return null;
                }
            }
        }

        private Train AddAttemptsToExamTrain(ExamTrain train, IEnumerable<TaskAnswerBindingModel> answers)
        {
            //get pairs attempt - answer and add answer value to attempt
            //attempts in exam train defined already 
            
            train.TaskAttempts
                .Join(
                    answers,
                    attempt => attempt.ExamTask.Id,
                    ans => ans.id,
                    ((ta, ua) => new
                        {
                            bindingModel = ua,
                            attempt = ta
                        })
                )
                .ForEach(
                    x =>
                    {
                        var attemptFromTrain = x.attempt;
                        attemptFromTrain.UserAnswer = x.bindingModel.answer;
                        attemptFromTrain = addExtraInfoForType(attemptFromTrain, x.bindingModel);
                    }
                );
            return train;
        }

        private Train AddAttemptsToFreeTrain(FreeTrain train, IEnumerable<TaskAnswerBindingModel> answers)
        {
            //add new attempts in free train for each answer
            answers.ForEach(x =>
                {
                    var attempt = GenerateNewEmptyAttemptByTaskId(x.id);
                    attempt.UserAnswer = x.answer;
                    attempt = addExtraInfoForType(attempt, x);
                    train.TaskAttempts.Add(attempt);
                }
            );
            return train;
        }

        private UserTaskAttempt addExtraInfoForType(UserTaskAttempt attempt, TaskAnswerBindingModel model)
        {
            //add additional params if attempt checking manually
            if (attempt is UserManualCheckingTaskAttempt)
            {
                UserManualCheckingTaskAttempt attemptOfManualCheckingTask = (UserManualCheckingTaskAttempt)attempt;
                attemptOfManualCheckingTask.Comment = model.comment;
                attemptOfManualCheckingTask.ImagesLinks = model.images.ToList();
                return attemptOfManualCheckingTask;
            }
            else
            {
                return attempt;
            }
        }

        //factory method to generate new attempts of difereant types
        private UserTaskAttempt GenerateNewEmptyAttemptByTaskId(int taskId)
        {
            var task = _dbContext.Tasks.FirstOrDefault(x => x.Id == taskId);
            if (task == null) return null;
            return GenerateNewEmptyAttempt(task);
        }

        private UserTaskAttempt GenerateNewEmptyAttempt(ExamTask task)
        {
            UserTaskAttempt result = null;
            if (task.Topic.IsShort)
            {
                result = new UserSimpleTaskAttempt() {ExamTask = task};
            }
            else
            {
                result = new UserManualCheckingTaskAttempt() {ExamTask = task};
            }
            if (result != null)
            {
                _dbContext.UserTaskAttempts.Add(result);
            }
            return result;
        }

        private Train GetTrainByType(string trainType, User user)
        {
            Train train;
            if (trainType.ToLower().Equals("free"))
            {
                train = new FreeTrain();
                train.Exam = user.CurrentExam;
                train.StartTime = DateTime.Now;
                train.FinishTime = DateTime.Now;

                _dbContext.Trains.Add(train);
            }
            else
            {
                if (trainType.ToLower().Equals("exam"))
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

        private IEnumerable<AnswerViewModel> CheckAnswers(Train trainWithAttempts)
        {
            //rate each attempt
            trainWithAttempts.TaskAttempts.OfType<UserSimpleTaskAttempt>().ForEach(x => x.Points = AutoRateAnswer(x));
            //rate manually
            trainWithAttempts.TaskAttempts.OfType<UserManualCheckingTaskAttempt>().ForEach(x => CheckManually(x));
            //convert to answerViewModel
            return trainWithAttempts.TaskAttempts.ToList().Select(x => new AnswerViewModel(x.ExamTask, x.Points));
        }

        private void CheckManually(UserManualCheckingTaskAttempt userManualCheckingTaskAttempt)
        {
            userManualCheckingTaskAttempt.Points = 0;
            userManualCheckingTaskAttempt.IsChecked = false;
            //todo: implement manual check system
        }

        private int AutoRateAnswer(UserTaskAttempt attempt)
        {
            if (attempt.UserAnswer != null 
                && attempt.UserAnswer.Trim()
                    .Replace(" ", "")
                    .ToLowerInvariant()
                    .Equals(attempt.ExamTask.Answer.Trim().Replace(" ", "").ToLowerInvariant()))
            {
                return attempt.ExamTask.Topic.PointsPerTask;
            }
            else
            {
                return 0;
            }
        }

        [Obsolete("Not used anymore", true)]
        private bool RatedAnswerIsCorrect(ExamTask task, UserTaskAttempt attempt)
        {
            return task.Topic.PointsPerTask == attempt.Points;
        }

        public IEnumerable<ExamTaskViewModel> GetTasksByType(int type, int offset, int limit)
        {
            bool isShortType = type == 0;
            return _dbContext
               .Tasks
               .Where(x => x.Topic.IsShort == isShortType)
               .OrderBy(x => x.Id)
               .Skip(offset)
               .Take(limit)
               .ToList()
               .Select(x => new ExamTaskViewModel(x));
        }
    }
}