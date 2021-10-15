using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;
using Telerik.JustMock.Core;
using GoogleCalendarHelper.Models;

namespace GoogleCalendarHelper.Tests.Controllers
{
    [TestClass]
    public class GoogleApiTests
    {
        [TestInitialize]
        public void Setup()
        {
        }

        //ToDo Change these to HttpStatusCode or something
        [TestMethod]
        public async Task<IEnumerable<GoogleTask>> GetTasks_ReturnsCorrectTasks()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void ExpiredCredentials_ThrowsNotAuthorizedException()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void GetTasksFailed_ThrowsGetTasksFailedException()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void GetEvents_ReturnsCorrectEvents()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void GetEventsFailed_ThrowsGetEventsFailedException()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void AddNewTasks_ReturnsSuccess()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void AddNewTasks_IncorrectFormatting_ThrowsIncorrectFormatException()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void AddNewTasks_ServerError_ThrowsServerErrorException()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void AddNewEvents_ReturnsSuccess()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void AddNewEvents_IncorrectFormatting_ThrowsIncorrectFormatError()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void DeleteTasks_ReturnsSuccess()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void DeleteTasksIncorrectId_Throws_TaskListNotFoundException()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void DeleteTasksServerError_Throws_ServerErrorException()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void DeleteEvents_ReturnsSuccess()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void DeleteEventsIncorrectId_Throws_EventsListNotFoundException()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void DeleteEventsServerError_Throws_ServerErrorException()
        {
            throw new NotImplementedException();
        }
    }
}
