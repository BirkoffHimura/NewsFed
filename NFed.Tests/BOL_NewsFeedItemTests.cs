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
    public class BOL_NewsFeedItemTests
    {
        TestTools tt;
        public BOL_NewsFeedItemTests()
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
        public void TestCreateNewsFeedItem()
        {
            string title = Guid.NewGuid().ToString();
            NFedContext nfc = new NFedContext(true);
            NewsFeedItem item = new NewsFeedItem()
            {
                Title = title,
                Body = "test Body",
                CreateDate = DateTime.Now,
                UserID = tt.userOne.ID,
                User = tt.userOne
            };
            nfc.Entry(tt.userOne).State = System.Data.Entity.EntityState.Unchanged;
            nfc.NewsFeedItems.Add(item);
            nfc.SaveChanges();

            NewsFeedItem tmp = nfc.NewsFeedItems.Where(x => x.Title == title).FirstOrDefault();
            Assert.IsNotNull(tmp);
        }
        [TestMethod]
        public void TestUpdateNewsFeedItem()
        {
            NFedContext nfc = new NFedContext(true);
            string newTitle = Guid.NewGuid().ToString();
            tt.userOneFirstNewsFeedItem.Title = newTitle;
            nfc.Entry(tt.userOneFirstNewsFeedItem).State = System.Data.Entity.EntityState.Modified;
            nfc.SaveChanges();

            NewsFeedItem tmp = nfc.NewsFeedItems.Where(x => x.ID == tt.userOneFirstNewsFeedItem.ID).FirstOrDefault();

            Assert.AreEqual(newTitle, tmp.Title);
        }
        [TestMethod]
        public void TestInsertAndDeleteNewsFeedItem()
        {
            string title = Guid.NewGuid().ToString();
            NFedContext nfc = new NFedContext(true);
            NewsFeedItem item = new NewsFeedItem()
            {
                Title = title,
                Body = "test Body",
                CreateDate = DateTime.Now,
                UserID = tt.userOne.ID,
                User = tt.userOne
            };

            nfc.Entry(tt.userOne).State = System.Data.Entity.EntityState.Unchanged;
            nfc.NewsFeedItems.Add(item);
            nfc.SaveChanges();

            NewsFeedItem tmp = nfc.NewsFeedItems.Where(x => x.Title == title).FirstOrDefault();
            Assert.IsNotNull(tmp);

            nfc.NewsFeedItems.Remove(tmp);
            nfc.SaveChanges();

            NewsFeedItem tmp2 = nfc.NewsFeedItems.Where(x => x.ID == tmp.ID).FirstOrDefault();

            Assert.IsNull(tmp2);
        }
    }
}
