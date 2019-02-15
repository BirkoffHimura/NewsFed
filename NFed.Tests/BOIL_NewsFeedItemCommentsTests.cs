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
    public class BOIL_NewsFeedItemCommentsTests
    {
        TestTools tt;
        public BOIL_NewsFeedItemCommentsTests()
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
        public void TestCreateNewsFeedItemComment()
        {
            string body = Guid.NewGuid().ToString();
            NFedContext nfc = new NFedContext(true);
            NewsFeedItemComment item = new NewsFeedItemComment()
            {
                Comment_Body = body,
                CommentDate = DateTime.Now,
                CommentUserID = tt.userOne.ID,
                User = tt.userOne,
                NewsFeedItem = tt.userOneFirstNewsFeedItem,
                NewsFeedItemID = tt.userOneFirstNewsFeedItem.ID
            };
            nfc.Entry(tt.userOne).State = System.Data.Entity.EntityState.Unchanged;
            nfc.Entry(tt.userOneFirstNewsFeedItem).State = System.Data.Entity.EntityState.Unchanged;
            nfc.NewsFeedItemComments.Add(item);
            nfc.SaveChanges();

            NewsFeedItemComment tmp = nfc.NewsFeedItemComments.Where(x => x.Comment_Body == body).FirstOrDefault();
            Assert.IsNotNull(tmp);
        }
        [TestMethod]
        public void TestUpdateNewsFeedItemComment()
        {
            NFedContext nfc = new NFedContext(true);
            string newCommentBody = Guid.NewGuid().ToString();
            tt.userOneCommentOnFirstNewsFeedItem.Comment_Body = newCommentBody;
            nfc.Entry(tt.userOneCommentOnFirstNewsFeedItem).State = System.Data.Entity.EntityState.Modified;
            nfc.SaveChanges();

            NewsFeedItemComment tmp = nfc.NewsFeedItemComments.Where(x => x.ID == tt.userOneCommentOnFirstNewsFeedItem.ID).FirstOrDefault();

            Assert.AreEqual(newCommentBody, tmp.Comment_Body);
        }
        [TestMethod]
        public void TestInsertAndDeleteNewsFeedItemComment()
        {
            string body = Guid.NewGuid().ToString();
            NFedContext nfc = new NFedContext(true);
            NewsFeedItemComment item = new NewsFeedItemComment()
            {
                Comment_Body = body,
                CommentDate = DateTime.Now,
                CommentUserID = tt.userOne.ID,
                User = tt.userOne,
                NewsFeedItem = tt.userOneFirstNewsFeedItem,
                NewsFeedItemID = tt.userOneFirstNewsFeedItem.ID
            };
            nfc.Entry(tt.userOne).State = System.Data.Entity.EntityState.Unchanged;
            nfc.Entry(tt.userOneFirstNewsFeedItem).State = System.Data.Entity.EntityState.Unchanged;
            nfc.NewsFeedItemComments.Add(item);
            nfc.SaveChanges();

            NewsFeedItemComment tmp = nfc.NewsFeedItemComments.Where(x => x.Comment_Body == body).FirstOrDefault();
            Assert.IsNotNull(tmp);

            nfc.NewsFeedItemComments.Remove(tmp);
            nfc.SaveChanges();

            NewsFeedItemComment tmp2 = nfc.NewsFeedItemComments.Where(x => x.ID == tmp.ID).FirstOrDefault();

            Assert.IsNull(tmp2);
        }
    }
}
