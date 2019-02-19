using BLL;
using BOL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFed.Tests
{
    [TestClass]
    public class BLL_NewsFeedItemBSTests
    {
        TestToolsBLL tt;
        NewsFeedItemBs db;
        public BLL_NewsFeedItemBSTests()
        {
            TestTools.CleanUpDb("Constructor");
            tt = new TestToolsBLL();
            tt.SetupInitialUserAccounts();
            db = new NewsFeedItemBs(true);
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
        public void TestNewsFeedItemBsInsert()
        {
            string title = Guid.NewGuid().ToString();
            NewsFeedItemDTO newsFeedItem = new NewsFeedItemDTO()
            {
                Body = Guid.NewGuid().ToString(),
                Title = title,
                CreateDate = DateTime.Now,
                User = tt.userOne,
                UserID = tt.userOne.ID
            };
            db.Insert(newsFeedItem);
            List<NewsFeedItemDTO> tmp = db.GetByUserName(tt.userOne.UserName).ToList();
            bool found = false;
            foreach (NewsFeedItemDTO item in tmp)
            {
                if (item.Title == title)
                { found = true; }
            }

            Assert.IsNotNull(tmp);
            Assert.IsTrue(found);
        }

        [TestMethod]
        public void TestNewsFeedItemBsGetAll()
        {
            var item = db.GetAll();
            Assert.IsNotNull(item);
            Assert.IsTrue(item.Count() > 0);
        }

        [TestMethod]
        public void TestNewsFeedItemBsGetByID()
        {
            NewsFeedItemDTO newsFeedItem;
            newsFeedItem = db.GetByID(tt.userOneFirstNewsFeedItem.ID);
            Assert.IsNotNull(newsFeedItem);
        }

        [TestMethod]
        public void TestNewsFeedItemDbGetByUserName()
        {
            List<NewsFeedItemDTO> newsFeedItem;
            newsFeedItem = db.GetByUserName(tt.userOne.UserName);
            Assert.IsNotNull(newsFeedItem);
            Assert.IsTrue(newsFeedItem.Count() > 0);
        }

        [TestMethod]
        public void TestNewsFeedItemBsSearch()
        {
            List<NewsFeedItemDTO> newsFeedItem;
            newsFeedItem = db.Search(tt.userOneFirstNewsFeedItem.Body);
            Assert.IsNotNull(newsFeedItem);
            Assert.IsTrue(newsFeedItem.Count() > 0);
            bool found = false;
            foreach (NewsFeedItemDTO item in newsFeedItem)
            {
                if (item.ID == tt.userOneFirstNewsFeedItem.ID)
                {
                    found = true;
                }
            }
            Assert.IsTrue(found);
        }
        [TestMethod]
        public void TestNewsFeedItemBsGetNewsFeedItemsFromFeedsBySubscriberUserName()
        {
            List<NewsFeedItemDTO> newsFeedItem;
            newsFeedItem = db.GetNewsFeedItemsFromFeedsBySubscriberUserName(tt.userTwo.UserName);
            Assert.IsNotNull(newsFeedItem);
            Assert.IsTrue(newsFeedItem.Count() > 0);
        }

        [TestMethod]
        public void TestNewsFeedItemBsUpdate()
        {
            string newBody = Guid.NewGuid().ToString();
            tt.userTwoFirstNewsFeedItem.Body = newBody;
            db.Update(tt.userTwoFirstNewsFeedItem);

            NewsFeedItemDTO temp = db.GetByID(tt.userTwoFirstNewsFeedItem.ID);

            Assert.IsNotNull(temp);

            Assert.AreEqual(newBody, temp.Body);
        }

        [TestMethod]
        public void TestNewsFeedItemBsDelete()
        {
            db.Delete(tt.userThreeFirstNewsFeedItem.ID);
            NewsFeedItemDTO newsFeedItem;
            newsFeedItem = db.GetByID(tt.userThreeFirstNewsFeedItem.ID);
            Assert.IsNull(newsFeedItem);
        }
    }
}
