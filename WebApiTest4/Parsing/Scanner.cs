using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AngleSharp;
using AngleSharp.Extensions;
using WebApiTest4.Models.EgeModels;

namespace WebApiTest4.Parsing
{
    public class Scanner
    {
        public static void Main(string[] args)
        {
            using (var dbcontext=new EgeDbContext())
            {
                
            }
             
        }

        public async Task  AddNewTasks(EgeDbContext egeDbContext)
        {
            
                var config = Configuration.Default.WithDefaultLoader();
                // Load the names of all The Big Bang Theory episodes from Wikipedia
                var address = "http://kpolyakov.spb.ru/school/ege/gen.php?B=on&C=on&action=viewVar&varId=2";
                // Asynchronously get the document in a new context using the configuration
                var document = await BrowsingContext.New(config).OpenAsync(address).ConfigureAwait(false);
                // This CSS selector gets the desired content
                var cellSelector = "td.topicview script";
                // Perform the query to get all cells with the content
                var cells = document.QuerySelectorAll(cellSelector);
                // We are only interested in the text - select it with LINQ
                string numberPrefix = "№&nbsp;";
                string textPrexfix = $"changeImageFilePath('";
                foreach (var cell in cells)
                {
                    var result = cell.TextContent;
                    int orderNum =
                        int.Parse(cell.ParentElement.ParentElement.QuerySelectorAll("td.egeno span").FirstOrDefault().TextContent);
                    int numStartPoint = result.IndexOf(numberPrefix) + numberPrefix.Length;
                    int textStartPoint = result.IndexOf(textPrexfix) + textPrexfix.Length;
                    var newTopic = new TaskTopic() { Name = "DefaultName" + orderNum, Number = orderNum, PointsPerTask = 0 };
                    if (!egeDbContext.TaskTopics.Any(x => x.Number == newTopic.Number))
                    {
                        egeDbContext.TaskTopics.Local.Add(newTopic);
                    }
                    var newTask = new EgeTask()
                    {

                        Topic = newTopic,
                        Number =
                            int.Parse(result.Substring(numStartPoint,
                                result.IndexOf(")") - numStartPoint)),
                        Text = result.Substring(textStartPoint, result.LastIndexOf("\') );") - textStartPoint),
                        Answer = document
                        .QuerySelectorAll("table.varanswer tr td.egeno")
                        .Where(x => int.Parse(x.TextContent.Substring(0, x.TextContent.Length - 1)) == orderNum)
                        .Next("td.answer")
                        .FirstOrDefault()?.TextContent
                    };
                    if (!egeDbContext.Tasks.Any(x => x.Number == newTask.Number))
                    {
                        egeDbContext.Tasks.Add(newTask);
                    }



                }
                egeDbContext.SaveChanges();
           
            
            
        }
    }
}