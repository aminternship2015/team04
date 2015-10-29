using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL;
using DAL.Entities;
using Moq;
namespace Tests
{
    [TestClass]
    public class UserDaoTests
    {
        UserDao userDao;
        User testUser;

        [TestInitialize]
        public void Initialize()
        {
            userDao = new UserDao();
            testUser = new User()
            {
                First_name = "Vlad Guleaev",
                Email = "test@test.com",
                Passwrd = "123",
                Last_name = "testUser"
            };
        }

        [TestMethod]
        public void Add_NewUser_True()
        {
            //act
            bool actual = userDao.Add(testUser);

            //assert
            Assert.AreEqual(true, actual);

            //delete
            userDao.Delete(testUser);
        }

        [TestMethod]
        public void IsNull_UserIsNull_False()
        {
            //arrange
            User Expected = new User();

            //act
            Expected = null;
            var Actual = userDao.Add(Expected);

            //assert
            Assert.IsFalse(Actual);
        }

        [TestMethod]
        public void Delete_UserIsDeleted_True()
        {
            //act
            userDao.Add(testUser);
            var Actual = userDao.Delete(testUser);

            //assert
            Assert.IsTrue(Actual);
        }

        [TestMethod]
        public void Edit_UserIsEdited_True()
        {
            //act
            userDao.Add(testUser);
            testUser.Email = "new@mail.com";

            var Actual = userDao.Update(testUser);

            //assert
            Assert.IsTrue(Actual);

            //delete
            userDao.Delete(testUser);
        }

        [TestMethod]
        public void GetById_returnUser_User()
        {
            //act
            userDao.Add(testUser);
            var Actual = userDao.GetByPublisherId(testUser.Id);

            //assert
            Assert.IsNotNull(Actual);

            //delete
            userDao.Delete(testUser);
        }

    }
}
