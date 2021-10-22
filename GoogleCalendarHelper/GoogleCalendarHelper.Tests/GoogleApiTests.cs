using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Google.Apis.Tasks.v1.Data;
using GoogleCalendarHelper.Controllers;
using Telerik.JustMock;
using GoogleCalendarHelper.Models;
using Microsoft.AspNetCore.Http;
using Task = Google.Apis.Tasks.v1.Data.Task;

namespace GoogleCalendarHelper.Tests.Controllers
{
    [TestClass]
    public class GoogleApiTests
    {
        private TasksApi _tasksApi;

        [TestInitialize]
        public void Setup()
        {
            _tasksApi = new TasksApi();
        }

        //ToDo Change these to HttpStatusCode or something
        [TestMethod]
        public void GetTaskList_200()
        {
            var expectedTasks =
                new TaskList
                {
                    Id = "MTgxMzg3MTMyNjA4NzczNzQ3MDM6MDow",
                    Title = "My Tasks"
                };

            var actual = _tasksApi.GetTaskList();
            Assert.AreEqual(expectedTasks.Id, actual.Id);
            Assert.AreEqual(expectedTasks.Title, actual.Title);
        }

        [TestMethod]
        public void GetTaskList_200_WrongList_False()
        {
            var expectedTasks =
                new TaskList
                {
                    Id = "MTgxMzg3MTMyNjA4NzczNzQ3MDM6MDow",
                    Title = "Not My Tasks"
                };

            var actual = _tasksApi.GetTaskList();
            Assert.AreEqual(expectedTasks.Id, actual.Id);
            Assert.AreNotEqual(expectedTasks.Title, actual.Title);
        }

        //[TestMethod]
        //public void ExpiredCredentials_ThrowsNotAuthorizedException()
        //{
        //    try
        //    {
        //        var actual = _tasksApi.GetTaskList();
        //    }
        //    catch
        //    {

        //    }

        //    throw new NotImplementedException();
        //}

        //[TestMethod]
        //public void GetTasksFailed_ThrowsGetTasksFailedException()
        //{
        //    throw new NotImplementedException();
        //}

        [TestMethod]
        public void IsPastDue_DateLessThanToday_ReturnsTrue()
        {
            var testDateTime = new DateTime(2020, 10, 01, 12, 12, 12);
            testDateTime = testDateTime.ToUniversalTime();
            var testTask = new Task
            {
                Due = testDateTime.ToLongDateString(),
                Title = "This is a test"
            };
            Assert.IsTrue(_tasksApi.IsPastDue(testTask));
        }

        [TestMethod]
        public void IsPastDue_DateGreaterThanToday_ReturnsFalse()
        {
            var afterTask = new Task
            {
                Due = DateTime.UtcNow.AddDays(1).ToString("s") + "Z",
                Title = "This is a test"
            };

            Assert.IsFalse(_tasksApi.IsPastDue(afterTask));
        }


        [TestMethod]
        public void AddNewTasks_ReturnsSuccess()
        {
            var testTask = new Task
            {
                Due = DateTime.UtcNow.ToString("s") + "Z",
                Title = "TestTask"
            };

            var testListId = _tasksApi.GetTaskList(true).Id;

            var actual = _tasksApi.AddTask(testTask, testListId);
            Assert.AreEqual(HttpStatusCode.OK, actual);
        }

        [TestMethod]
        public void AddNewTasks_CatchesException()
        {
            var testTask = new Task
            {
                Due = DateTime.UtcNow.ToString("s") + "Z",
                Title = "TestTask"
            };

            var testListId = _tasksApi.GetTaskList(true).Id;

            var actual = Mock.Arrange(() => _tasksApi.AddTask(testTask, testListId)).Throws(new Exception());
            Assert.AreEqual(HttpStatusCode.InternalServerError, actual);
        }

        //[TestMethod]
        //public void SetupCredential_InvalidCredentialCert_CatchesException()
        //{
        //    var testTask = new Task
        //    {
        //        Due = DateTime.UtcNow.ToString("s") + "Z",
        //        Title = "TestTask"
        //    };

        //    var actual = Mock.Arrange(() => _tasksApi.SetupCredential()).Throws(new Exception());
            
        //    Assert.AreEqual(HttpStatusCode.InternalServerError, actual);
        //}


        [TestMethod]
        public void MovePastDueTasksToToday_MovesTasks_DeletesOldOne()
        {
            var testDateTime = new DateTime(2020, 10, 01, 12, 12, 12);
            testDateTime = testDateTime.ToUniversalTime();
            var testTask = new Task
            {
                Due = testDateTime.ToString("s") + "Z",
                Title = "This is a test"
            };
            var testTaskList = _tasksApi.GetTaskList(true);

            _tasksApi.AddTask(testTask, testTaskList.Id);
            var actual = _tasksApi.MovePastDueTasksToToday(true);
            
            var listOfTasks = _tasksApi.GetListOfTasks(testTaskList.Id);
            foreach (Task task in listOfTasks)
            {
                Assert.IsFalse(_tasksApi.IsPastDue(task));
            }
            Assert.AreEqual(HttpStatusCode.OK, actual);
        }

        [TestMethod]
        public void RemoveTask_ReturnsSuccess()
        {
            var testTitle = DateTime.UtcNow.ToString("s") + "Z";
            var testTask = new Task
            {
                Due = DateTime.UtcNow.ToString("s") + "Z",
                Title = testTitle
            };

            var testListId = _tasksApi.GetTaskList(true).Id;


            _tasksApi.AddTask(testTask, testListId);
            var testTaskId = _tasksApi.GetListOfTasks(testListId).First(x => x.Title == testTitle).Id;

            var actual = _tasksApi.RemoveTask(testListId, testTaskId);

            var listWithoutTask = _tasksApi.GetListOfTasks(testListId);
            foreach (var task in listWithoutTask)
            {
                Assert.IsTrue(task.Title != testTitle);
            }
            Assert.AreEqual(HttpStatusCode.OK, actual);
        }

        //    [TestMethod]
        //    public void AddNewTasks_IncorrectFormatting_ThrowsIncorrectFormatException()
        //    {
        //        throw new NotImplementedException();
        //    }

        //    [TestMethod]
        //    public void AddNewTasks_ServerError_ThrowsServerErrorException()
        //    {
        //        throw new NotImplementedException();
        //    }

        //    [TestMethod]
        //    public void DeleteTasks_ReturnsSuccess()
        //    {
        //        throw new NotImplementedException();
        //    }

        //    [TestMethod]
        //    public void DeleteTasksIncorrectId_Throws_TaskListNotFoundException()
        //    {
        //        throw new NotImplementedException();
        //    }

        //    [TestMethod]
        //    public void DeleteTasksServerError_Throws_ServerErrorException()
        //    {
        //        throw new NotImplementedException();
        //    }

    }
}
