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
            Console.Read();

        }


        public bool IsPastDue(Task task)
        {
            return DateTime.Parse(task.Due) < DateTime.UtcNow;
        }


        public HttpStatusCode MovePastDueTasksToToday()
        {
            var taskList = GetTaskList();
            var list_of_tasks = _service.Tasks.List(taskList.Id).Execute().Items;

            foreach(Task task in list_of_tasks)
            {
                if (IsPastDue(task))
                {
                    task.Due = DateTime.Now.AddHours(6).ToLongDateString();
                    //ToDo: Remove Task
                    AddTask(task, taskList.Id);
                }
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
            try
            {
                var credential = SetupCredential();
                return new TasksService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Credentials error");
            }
        }

        public UserCredential SetupCredential()
        {
            UserCredential credential;

            using (var stream =
                new FileStream("C:/Code/GoogleCalendarHelper/GoogleCalendarHelper/credentials.json", FileMode.Open, FileAccess.Read))
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
        }
    }
}
