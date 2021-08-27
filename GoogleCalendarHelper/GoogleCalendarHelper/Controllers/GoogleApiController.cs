using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
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
using Microsoft.AspNetCore.Mvc;


namespace GoogleCalendarHelper.Controllers
{
    public class GoogleApiController : GoogleCalendarHelperController
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/calendar-dotnet-quickstart.json
        static string[] Scopes = {CalendarService.Scope.CalendarReadonly};
        static string ApplicationName = "Google Calendar API .NET Quickstart";



        //===========================================================================================================
        //Example
        //Delete when done
         //===========================================================================================================
        static void Example(string[] args)
        {
            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
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
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Calendar API service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define parameters of request.
            EventsResource.ListRequest request = service.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 10;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            // List events.
            Events events = request.Execute();
            Console.WriteLine("Upcoming events:");
            if (events.Items != null && events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    string when = eventItem.Start.DateTime.ToString();
                    if (String.IsNullOrEmpty(when))
                    {
                        when = eventItem.Start.Date;
                    }

                    Console.WriteLine("{0} ({1})", eventItem.Summary, when);
                }
            }
            else
            {
                Console.WriteLine("No upcoming events found.");
            }

            Console.Read();

        }
        //===========================================================================================================
       
        
        //ToDo Change these to HttpStatusCode or something
        [HttpGet]
        public List<string> GetTasks()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public List<string> GetEvents()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public HttpStatusCode AddNewTasks()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public HttpStatusCode AddNewEvents()
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public HttpStatusCode DeleteTasks()
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public HttpStatusCode DeleteEvents()
        {
            throw new NotImplementedException();

        }
    }
}
