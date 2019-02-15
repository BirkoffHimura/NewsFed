using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BOL;
using System.Data.Entity.Validation;

namespace NFed.Tests
{
    [TestClass]
    public class BOL_UserTests
    {
        TestTools tt;
        public BOL_UserTests()
        {
            TestTools.CleanUpDb("Constructor");
            tt = new TestTools();
            tt.SetupInitialUserAccounts();
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
        public void TestInsertUser()
        {

            User user = tt.InsertUser("Test User", "test@user.com", "pass123");
            using (NFedContext sm = new NFedContext(true))
            {
                sm.Users.Add(user);
                sm.SaveChanges();

                Assert.IsNotNull(user.ID);
                User retVal = sm.Users.Find(user.ID);
                Assert.AreEqual(retVal.UserName, user.UserName);
            }
        }
        [DataTestMethod]
        [DataRow("Test User", "testuser.com", "pass123")]
        [DataRow("Test User", "testuser", "pass123")]
        [DataRow("Test User", "testuser@", "pass123")]
        [DataRow("Test User", "testuser@.com", "pass123")]
        public void TestInvalidEmail(string theName, string uName, string pass)
        {
            User user = tt.InsertUser(theName, uName, pass);

            using (NFedContext sm = new NFedContext(true))
            {
                try
                {
                    sm.Users.Add(user);
                    sm.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    Console.Out.WriteLine(ex.Message);
                    Assert.IsTrue(ex.Message.ToUpper().Contains("VALIDATION FAILED"));
                    return;
                }
            }
            Assert.Fail();
        }
        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void TestValidateStateMoreThan2Chars()
        {
            using (NFedContext sm = new NFedContext(true))
            {
                User user = sm.Users.Find(tt.userOne.ID);
                user.State = "ZZZ";
                sm.SaveChanges();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void TestValidateZipCodeMoreThan5Chars()
        {
            using (NFedContext sm = new NFedContext(true))
            {
                User user = sm.Users.Find(tt.userOne.ID);
                user.ZipCode = "123456";
                sm.SaveChanges();
            }
        }
    }
}
