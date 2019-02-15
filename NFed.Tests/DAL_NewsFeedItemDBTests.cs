using BOL;
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
    public class DAL_NewsFeedItemDBTests
    {
        TestTools tt;
        NewsFeedItemDb db;
        public DAL_NewsFeedItemDBTests()
        {
            TestTools.CleanUpDb("Constructor");
            tt = new TestTools();
            tt.SetupInitialUserAccounts();
            db = new NewsFeedItemDb(true);
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
        public void TestNewsFeedItemDbInsert()
        {
            string title = Guid.NewGuid().ToString();
            NewsFeedItem newsFeedItem = new NewsFeedItem()
            {
                Body = Guid.NewGuid().ToString(),
                Title = title,
                CreateDate = DateTime.Now,
                User = tt.userOne,
                UserID = tt.userOne.ID
            };
            db.Insert(newsFeedItem);
            List<NewsFeedItem> tmp = db.GetByUserName(tt.userOne.UserName).ToList();
            bool found = false;
            foreach (NewsFeedItem item in tmp)
            {
                if (item.Title == title)
                { found = true; }
            }

            Assert.IsNotNull(tmp);
            Assert.IsTrue(found);
        }

        [TestMethod]
        public void TestNewsFeedItemDbGetAll()
        {
            var item = db.GetAll();
            Assert.IsNotNull(item);
            Assert.IsTrue(item.Count() > 0);
        }

        [TestMethod]
        public void TestNewsFeedItemDbGetByID()
        {
            NewsFeedItem newsFeedItem;
            newsFeedItem = db.GetByID(tt.userOneFirstNewsFeedItem.ID);
            Assert.IsNotNull(newsFeedItem);
        }

        [TestMethod]
        public void TestNewsFeedItemDbGetByUserName()
        {
            List<NewsFeedItem> newsFeedItem;
            newsFeedItem = db.GetByUserName(tt.userOne.UserName);
            Assert.IsNotNull(newsFeedItem);
            Assert.IsTrue(newsFeedItem.Count() > 0);
        }

        [TestMethod]
        public void TestNewsFeedItemDbSearch()
        {
            List<NewsFeedItem> newsFeedItem;
            newsFeedItem = db.Search(tt.userOneFirstNewsFeedItem.Body);
            Assert.IsNotNull(newsFeedItem);
            Assert.IsTrue(newsFeedItem.Count() > 0);
            bool found = false;
            foreach(NewsFeedItem item in newsFeedItem)
            {
                if(item.ID == tt.userOneFirstNewsFeedItem.ID)
                {
                    found = true;
                }
            }
            Assert.IsTrue(found);
        }

        [TestMethod]
        public void TestNewsFeedItemDbGetNewsFeedItemsFromFeedsBySubscriberUserName()
        {
            List<NewsFeedItem> newsFeedItem;
            newsFeedItem = db.GetNewsFeedItemsFromFeedsBySubscriberUserName(tt.userTwo.UserName);
            Assert.IsNotNull(newsFeedItem);
            Assert.IsTrue(newsFeedItem.Count() > 0);
        }
        
        [TestMethod]
        public void TestNewsFeedItemDbUpdate()
        {
            string newBody = Guid.NewGuid().ToString();
            tt.userTwoFirstNewsFeedItem.Body = newBody;
            db.Update(tt.userTwoFirstNewsFeedItem);

            NewsFeedItem temp = db.GetByID(tt.userTwoFirstNewsFeedItem.ID);

            Assert.IsNotNull(temp);

            Assert.AreEqual(newBody, temp.Body);
        }

        [TestMethod]
        public void TestNewsFeedItemDbDelete()
        {
            db.Delete(tt.userThreeFirstNewsFeedItem.ID);
            NewsFeedItem newsFeedItem;
            newsFeedItem = db.GetByID(tt.userThreeFirstNewsFeedItem.ID);
            Assert.IsNull(newsFeedItem);
        }
    }
}
