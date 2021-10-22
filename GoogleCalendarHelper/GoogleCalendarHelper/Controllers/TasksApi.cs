using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Tasks.v1;
using Google.Apis.Tasks.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Task = Google.Apis.Tasks.v1.Data.Task;

namespace GoogleCalendarHelper.Controllers
{
    public class TasksApi
    {
        private readonly TasksService _service;
        public TasksApi()
        {
            _service = SetupService();
        }

        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/tasks-dotnet-quickstart.json
        static string[] Scopes = { TasksService.Scope.Tasks };
        static string ApplicationName = "Google Tasks API .NET Quickstart";

        public TaskList GetTaskList(bool isTest = false)
        {
            // Define parameters of request.
            TasklistsResource.ListRequest listRequest = _service.Tasklists.List();
            listRequest.MaxResults = 10;

            // List task lists.
            IList<TaskList> taskLists = listRequest.Execute().Items;
            Console.WriteLine("Task Lists:");
            if (taskLists != null && taskLists.Count > 0)
            {
                return isTest ? taskLists[1] : taskLists[0];
            }
            else
            {
                Console.WriteLine("No task lists found.");
                return new TaskList(); //ToDO: Make this properly
            }
        }

        public IList<Task> GetListOfTasks(string taskListId)
        {
            return _service.Tasks.List(taskListId).Execute().Items;
        }


        public bool IsPastDue(Task task)
        {
            return DateTime.Parse(task.Due).DayOfYear < DateTime.UtcNow.DayOfYear;
        }

        public HttpStatusCode RemoveTask(string taskListId, string taskId)
        {
            try
            {
                _service.Tasks.Delete(taskListId, taskId).Execute();
                return HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return HttpStatusCode.InternalServerError;
            }

        }

        public HttpStatusCode MovePastDueTasksToToday(bool isTest = false)
        {
            var taskList = GetTaskList(isTest);
            var list_of_tasks = GetListOfTasks(taskList.Id);

            try
            {

                foreach (Task task in list_of_tasks)
                {
                    if (IsPastDue(task))
                    {
                        var updatedTask = task;
                        updatedTask.Due = DateTime.UtcNow.AddHours(6).ToString("s") + "Z";
                        RemoveTask(taskList.Id, task.Id);
                        AddTask(updatedTask, taskList.Id);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.Write(ex);
                return HttpStatusCode.InternalServerError;
            }


            return HttpStatusCode.OK;
        }

        public HttpStatusCode AddTask(Task task, string taskListId)
        {
            try
            {
                _service.Tasks.Insert(task, taskListId).Execute();
                return HttpStatusCode.OK;
            }
            catch(Exception e)
            {
                Console.Write(e);
                return HttpStatusCode.InternalServerError;
            }
        }

        public TasksService SetupService()
        {
            var credential = SetupCredential();
            return new TasksService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }

        public UserCredential SetupCredential()
        {
            UserCredential credential;
            //try
            //{


                using (var stream =
                    new FileStream("C:/Code/GoogleCalendarHelper/GoogleCalendarHelper/credentials.json", FileMode.Open,
                        FileAccess.Read))
                {
                    // The file token.json stores the user's access and refresh tokens, and is created
                    // automatically when the authorization flow completes for the first time.
                    string credPath = "token.json";
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        Scopes,
                        "user",
                        CancellationToken.None,
                        new FileDataStore(credPath, true)).Result;
                }

                return credential;
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("Error setting up validation certificate");
            //    //should make inherited UserCredential class with status variable
            //    return new UserCredential();
            //}

        }
    }
}
