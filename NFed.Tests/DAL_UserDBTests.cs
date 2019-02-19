using BOL;
using DAL;
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
    public class DAL_UserDBTests
    {
        TestTools tt;
        UserDb db;
        public DAL_UserDBTests()
        {
            TestTools.CleanUpDb("Constructor");
            tt = new TestTools();
            tt.SetupInitialUserAccounts();
            db = new UserDb(true);
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
        public void TestUserDBInsertUser()
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
        public void TestUserDBInvalidEmail(string theName, string uName, string pass)
        {
            User user = tt.InsertUser(theName, uName, pass);
            db.Insert(user);            
        }

        [TestMethod]
        public void TestUserDbGetAll()
        {
            List<User> ret = (List<User>)db.GetAll();
            Assert.IsTrue(ret.Count > 0);
        }

        [TestMethod]
        public void TestUserDbGetAllWithPassword()
        {
            List<User> ret = (List<User>)db.GetAllWithPass();
            Assert.IsTrue(ret.Count > 0);
        }

        [TestMethod]
        public void TestUserDbGetRandom()
        {
            List<User> ret = (List<User>)db.GetRandom();
            Assert.IsTrue(ret.Count > 0);
        }

        [TestMethod]
        public void TestUserDbGetRandomWithUserName()
        {
            List<User> ret = (List<User>)db.GetRandom(tt.userTwo.UserName);
            Assert.IsTrue(ret.Count > 0);
            bool found = false;
            foreach(User item in ret)
            {
                if(item.ID == tt.userTwo.ID)
                {
                    found = true;
                }
            }
            Assert.IsFalse(found);
        }

        [TestMethod]
        public void TestUserDbGetByID()
        {
            User tUser;
            tUser = db.GetByID(tt.userOne.ID);
            Assert.AreEqual(tt.userOne.UserName, tUser.UserName);
        }
        [TestMethod]
        public void TestUserDbGetByUserName()
        {
            User tUser;
            tUser = db.GetByUserName(tt.userOne.UserName);
            Assert.AreEqual(tt.userOne.UserName, tUser.UserName);
            Assert.AreEqual(tt.userOne.ID, tUser.ID);
        }
              
        [TestMethod]
        public void TestUserDbDelete()
        {
            db.Delete(tt.userThree.ID);
            User tUser;
            tUser = db.GetByID(tt.userThree.ID);
            Assert.IsNull(tUser);
        }

        [TestMethod]
        public void TestUserDbUdate()
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
