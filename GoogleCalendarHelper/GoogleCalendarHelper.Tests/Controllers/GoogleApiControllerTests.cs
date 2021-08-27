using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoogleCalendarHelper.Tests.Controllers
{
    [TestClass]
    public class GoogleApiControllerTests
    {
        [TestInitialize]
        public void Setup()
        {
        }

        //ToDo Change these to HttpStatusCode or something
        [TestMethod]
        public void GetTasks_ReturnsCorrectTasks()
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
