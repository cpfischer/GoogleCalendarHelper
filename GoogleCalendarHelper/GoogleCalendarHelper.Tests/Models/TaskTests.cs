using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoogleCalendarHelper.Tests.Models
{
    [TestClass]
    public class TaskTests
    {
        [TestInitialize]
        public void Setup()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void MoveYesterdayTasks_ToNextDay_MovesOnlySpecifiedTasks()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void MoveYesterdayTasks_ToNextDay_NoTasks_DoesNotCallService()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void MoveYesterdayTasks_ToNextDay_ServerError_ThrowsServerErrorException()
        {
            throw new NotImplementedException();
        }
    }
}
