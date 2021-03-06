﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;
using System.Web;
using AngleSharp;
using AngleSharp.Extensions;
using WebApiTest4.Models.ExamsModels;

namespace WebApiTest4.Parsing
{
    public class Scanner
    {
        
        public async Task AddNewTasks(ExamAppDbContext examAppDbContext)
        {
            using (examAppDbContext)
            {


                var config = Configuration.Default.WithDefaultLoader();
                const int numberOfVariants = 20;
                for (int i = 1; i <= numberOfVariants; i++)
                {
                    var address = "http://kpolyakov.spb.ru/school/ege/gen.php?B=on&C=on&action=viewVar&varId=" + i;
                    var document = await BrowsingContext.New(config).OpenAsync(address).ConfigureAwait(false);
                    var cellSelector = "td.topicview script";
                    var cells = document.QuerySelectorAll(cellSelector);
                    const string numberPrefix = "№&nbsp;";
                    const string textPrexfix = "changeImageFilePath('";
                    foreach (var cell in cells)
                    {
                        var result = cell.TextContent;
                        var firstOrDefault =
                            cell.ParentElement.ParentElement.QuerySelectorAll("td.egeno span").FirstOrDefault();
                        if (firstOrDefault != null)
                        {
                            int orderNum =
                                int.Parse(firstOrDefault.TextContent);
                            int numStartPoint = result.IndexOf(numberPrefix) + numberPrefix.Length;
                            int textStartPoint = result.IndexOf(textPrexfix) + textPrexfix.Length;
                            var newTopic = new TaskTopic()
                            {
                                Exam = examAppDbContext.Exams.OfType<EgeExam>().FirstOrDefault(),
                                Name = "DefaultName" + orderNum,
                                Number = orderNum,
                                PointsPerTask = 0
                            };
                            if (examAppDbContext.TaskTopics.All(x => x.Number != newTopic.Number))
                            {
                                examAppDbContext.TaskTopics.Add(newTopic);
                            }
                            else
                            {
                                newTopic = examAppDbContext.TaskTopics.FirstOrDefault(x => x.Number == newTopic.Number);
                            }
                            var newTask = new ExamTask()
                            {

                                Topic = newTopic,
                                Number =
                                    int.Parse(result.Substring(numStartPoint,
                                        result.IndexOf(")") - numStartPoint)),
                                Text = result.Substring(textStartPoint, result.LastIndexOf("\') );") - textStartPoint),
                                Answer = document
                                    .QuerySelectorAll("table.varanswer tr td.egeno")
                                    .Where(
                                        x => int.Parse(x.TextContent.Substring(0, x.TextContent.Length - 1)) == orderNum)
                                    .Next("td.answer")
                                    .FirstOrDefault()?.TextContent
                            };
                            if (!examAppDbContext.Tasks.Any(x => x.Number == newTask.Number))
                            {
                                examAppDbContext.Tasks.Add(newTask);
                            }
                        }
                    }
                }
                examAppDbContext.SaveChanges();

                CreateEgeTasksTopics(examAppDbContext);

                examAppDbContext.SaveChanges();

            }

        }

        private const int countOfTopics = 27;
        private const int freeAnswerTasksStartsCount = 24;
        private void CreateEgeTasksTopics(ExamAppDbContext examAppDbContext)
        {
            var rm = new ResourceManager("WebApiTest4.Parsing.Topics", Assembly.GetExecutingAssembly());
            for (int i = 1; i <= countOfTopics; i++)
            {

                var topic = examAppDbContext
                    .TaskTopics
                    .FirstOrDefault(x => x.Id == i);
                if (topic != null)
                {
                    topic.Name = rm.GetString("s" + i);
                    
                    if (i < freeAnswerTasksStartsCount)
                    {
                        topic.PointsPerTask = 1;
                        topic.IsShort = true;
                        topic.Code = "B" + i;
                    }
                    else
                    {

                        topic.Code = "C" + (i - freeAnswerTasksStartsCount + 1);
                        topic.PointsPerTask = 3;
                        if (i == 25)
                        {
                            topic.PointsPerTask = 2;
                        }
                        if (i == 27)
                        {
                            topic.PointsPerTask = 4;
                        }
                    }
                }
                    

            }
        }
    }
}