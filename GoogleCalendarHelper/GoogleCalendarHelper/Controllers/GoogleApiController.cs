using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoogleCalendarHelper.Controllers
{
    public class GoogleApiController : GoogleCalendarHelperController
    {
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
