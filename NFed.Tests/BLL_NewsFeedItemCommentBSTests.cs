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
    public class BLL_NewsFeedItemCommentBSTests
    {
        TestTools tt;
        NewsFeedItemCommentBs db;
        public BLL_NewsFeedItemCommentBSTests()
        {
            TestTools.CleanUpDb("Constructor");
            tt = new TestTools();
            tt.SetupInitialUserAccounts();
            db = new NewsFeedItemCommentBs(true);
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
        public void TestNewsFeedItemCommentDbInsert()
        {
            string cBody = Guid.NewGuid().ToString();
            NewsFeedItemComment newsFeedItemComment = new NewsFeedItemComment()
            {
                Comment_Body = cBody,
                CommentDate = DateTime.Now,
                CommentUserID = tt.userOne.ID,
                NewsFeedItemID = tt.userOneFirstNewsFeedItem.ID
            };
            db.Insert(newsFeedItemComment);
            List<NewsFeedItemComment> tmp = db.GetByUserName(tt.userOne.UserName);
            bool found = false;
            foreach (NewsFeedItemComment item in tmp)
            {
                if (item.Comment_Body == cBody)
                { found = true; }
            }

            Assert.IsNotNull(tmp);
            Assert.IsTrue(found);
        }

        [TestMethod]
        public void TestNewsFeedItemCommentDbGetAll()
        {
            var item = db.GetAll();
            Assert.IsNotNull(item);
            Assert.IsTrue(item.Count() > 0);
        }

        [TestMethod]
        public void TestNewsFeedItemCommentDbGetByID()
        {
            NewsFeedItemComment newsFeedItemComment;
            newsFeedItemComment = db.GetByID(tt.userOneCommentOnFirstNewsFeedItem.ID);
            Assert.IsNotNull(newsFeedItemComment);
        }

        [TestMethod]
        public void TestNewsFeedItemCommentDbGetByUserName()
        {
            List<NewsFeedItemComment> newsFeedItemComment;
            newsFeedItemComment = db.GetByUserName(tt.userOne.UserName);
            Assert.IsNotNull(newsFeedItemComment);
            Assert.IsTrue(newsFeedItemComment.Count() > 0);
        }


        [TestMethod]
        public void TestNewsFeedItemCommentDbUpdate()
        {
            string newBody = Guid.NewGuid().ToString();
            tt.userOneCommentOnFirstNewsFeedItem.Comment_Body = newBody;
            db.Update(tt.userOneCommentOnFirstNewsFeedItem);

            NewsFeedItemComment temp = db.GetByID(tt.userOneCommentOnFirstNewsFeedItem.ID);

            Assert.IsNotNull(temp);

            Assert.AreEqual(newBody, temp.Comment_Body);
        }

        [TestMethod]
        public void TestNewsFeedItemCommentDbDelete()
        {
            db.Delete(tt.userThreeCommentSecondComment.ID);
            NewsFeedItemComment newsFeedItemComment;
            newsFeedItemComment = db.GetByID(tt.userThreeCommentSecondComment.ID);
            Assert.IsNull(newsFeedItemComment);
        }
    }
}
