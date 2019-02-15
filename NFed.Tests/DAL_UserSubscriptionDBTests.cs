﻿using BOL;
using DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFed.Tests
{
    [TestClass]
    public class DAL_UserSubscriptionDBTests
    {
        TestTools tt;
        UserSubscriptionDb db;
        public DAL_UserSubscriptionDBTests()
        {
            TestTools.CleanUpDb("Constructor");
            tt = new TestTools();
            tt.SetupInitialUserAccounts();
            db = new UserSubscriptionDb(true);
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
        public void TestInsertUserSubscription()
        {

            UserSubscription userSubscription =new UserSubscription()
            {
                User_Sub_ID = tt.userOne.ID,
                User_Feed_ID = tt.userTwo.ID
            };
            db.Insert(userSubscription);
            List<UserSubscription> tmp = db.GetSubsByFeedUserName(tt.userTwo.UserName).ToList();
            bool found = false;
            foreach(UserSubscription item in tmp)
            {
                if(item.User_Sub_ID == tt.userOne.ID)
                { found = true; }
            }

            Assert.IsNotNull(tmp);
            Assert.IsTrue(found);
        }
        
        [TestMethod]
        public void TestUserSubscriptionDbGetAll()
        {
            var item = db.GetAll();
            Assert.IsNotNull(item);
            Assert.IsTrue(item.Count() > 0);
        }

        [TestMethod]
        public void TestUserSubscriptionDbGetByID()
        {
            UserSubscription userSubscription;
            userSubscription = db.GetByID(tt.userTwoSubscription.ID);
            Assert.IsNotNull(userSubscription);
            Assert.AreEqual(tt.userTwoSubscription.User_Feed_ID, userSubscription.User_Feed_ID);
            Assert.AreEqual(tt.userTwoSubscription.User_Sub_ID, userSubscription.User_Sub_ID);
        }

        [TestMethod]
        public void TestUserSubscriptionDbGetSubsByFeedID()
        {
            List<UserSubscription> userSubscription;
            userSubscription = db.GetSubsByFeedID(tt.userTwoSubscription.User_Feed_ID).ToList();
            Assert.IsNotNull(userSubscription);
            bool valid = true;
            foreach(UserSubscription item in userSubscription)
            {
                if(item.User_Feed_ID != tt.userTwoSubscription.User_Feed_ID)
                {
                    valid = false;
                }
            }
            Assert.IsTrue(valid);
        }

        [TestMethod]
        public void TestUserSubscriptionDbGetByGetFeedsBySubID()
        {
            List<UserSubscription> userSubscription;
            userSubscription = db.GetFeedsBySubID(tt.userTwoSubscription.User_Sub_ID).ToList();
            Assert.IsNotNull(userSubscription);
            bool valid = true;
            foreach (UserSubscription item in userSubscription)
            {
                if (item.User_Sub_ID != tt.userTwoSubscription.User_Sub_ID)
                {
                    valid = false;
                }
            }
            Assert.IsTrue(valid);
        }

        [TestMethod]
        public void TestUserSubscriptionDbGetSubsByFeedUserName()
        {
            List<UserSubscription> userSubscription;
            userSubscription = db.GetSubsByFeedUserName(tt.userOne.UserName).ToList();
            Assert.IsNotNull(userSubscription);
            bool valid = true;
            foreach (UserSubscription item in userSubscription)
            {
                if (item.User_Feed_ID != tt.userOne.ID)
                {
                    valid = false;
                }
            }
            Assert.IsTrue(valid);
        }

        [TestMethod]
        public void TestUserSubscriptionDbGetByGetFeedsBySubUserName()
        {
            List<UserSubscription> userSubscription;
            userSubscription = db.GetFeedsBySubUserName(tt.userTwo.UserName).ToList();
            Assert.IsNotNull(userSubscription);
            bool valid = true;
            foreach (UserSubscription item in userSubscription)
            {
                if (item.User_Sub_ID != tt.userTwo.ID)
                {
                    valid = false;
                }
            }
            Assert.IsTrue(valid);
        }

        [TestMethod]
        public void TestUserSubscriptionDbDelete()
        {
            db.Delete(tt.userThreeSubscription.ID);
            UserSubscription userSubscription;
            userSubscription = db.GetByID(tt.userThreeSubscription.ID);
            Assert.IsNull(userSubscription);
        }
        
    }
}
