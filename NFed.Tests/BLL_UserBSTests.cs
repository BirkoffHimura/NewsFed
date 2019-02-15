using BLL;
using BOL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFed.Tests
{
    [TestClass]
    public class BLL_UserBSTests
    {
        TestTools tt;
        UserBs db;
        public BLL_UserBSTests()
        {
            TestTools.CleanUpDb("Constructor");
            tt = new TestTools();
            tt.SetupInitialUserAccounts();
            db = new UserBs(true);
        }
        [ClassInitialize()]
        public static void ClassTestInitialize(TestContext testContext)
        {

        }
        [ClassCleanup()]
        public static void ClassTestCleanup()
        {
            TestTools.CleanUpDb("Destructor");
        }
        [TestMethod]
        public void TestUserBSInsertUser()
        {

            User user = tt.InsertUser("Test User", Guid.NewGuid().ToString() + "@user.com", "pass123");
            db.Insert(user);
            User tmp = db.GetByUserName(user.UserName);
            Assert.IsNotNull(tmp);
        }
        [DataTestMethod]
        [DataRow("Test User", "testuser.com", "pass123")]
        [DataRow("Test User", "testuser", "pass123")]
        [DataRow("Test User", "testuser@", "pass123")]
        [DataRow("Test User", "testuser@.com", "pass123")]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void TestUserBSInvalidEmail(string theName, string uName, string pass)
        {
            User user = tt.InsertUser(theName, uName, pass);
            db.Insert(user);
        }

        [TestMethod]
        public void TestUserBSGetAll()
        {
            List<User> ret = (List<User>)db.GetAll();
            Assert.IsTrue(ret.Count > 0);
        }
        [TestMethod]
        public void TestUserBSGetRandom()
        {
            List<User> ret = (List<User>)db.GetRandom();
            Assert.IsTrue(ret.Count > 0);
        }
        [TestMethod]
        public void TestUserBSGetByID()
        {
            User tUser;
            tUser = db.GetByID(tt.userOne.ID);
            Assert.AreEqual(tt.userOne.UserName, tUser.UserName);
        }
        [TestMethod]
        public void TestUserBSGetByUserName()
        {
            User tUser;
            tUser = db.GetByUserName(tt.userOne.UserName);
            Assert.AreEqual(tt.userOne.UserName, tUser.UserName);
            Assert.AreEqual(tt.userOne.ID, tUser.ID);
        }

        [TestMethod]
        public void TestUserBSDelete()
        {
            db.Delete(tt.userThree.ID);
            User tUser;
            tUser = db.GetByID(tt.userThree.ID);
            Assert.IsNull(tUser);
        }

        [TestMethod]
        public void TestUserBSUdate()
        {
            User tUser = new User();


            User cUser;
            tt.userTwo.Name = "UserDbUdate test";


            db.Update(tt.userTwo);

            cUser = db.GetByUserName(tt.userTwo.UserName);
            Assert.IsNotNull(cUser);
            Assert.IsTrue(cUser.ID > 0);
            Assert.AreEqual(tt.userTwo.Name, cUser.Name);
        }
    }
}
