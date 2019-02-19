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
        TestToolsBLL tt;
        NewsFeedItemCommentBs db;
        public BLL_NewsFeedItemCommentBSTests()
        {
            TestTools.CleanUpDb("Constructor");
            tt = new TestToolsBLL();
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
        public void TestNewsFeedItemCommentBsInsert()
        {
            string cBody = Guid.NewGuid().ToString();
            NewsFeedItemCommentDTO newsFeedItemComment = new NewsFeedItemCommentDTO()
            {
                Comment_Body = cBody,
                CommentDate = DateTime.Now,
                CommentUserID = tt.userOne.ID,
                NewsFeedItemID = tt.userOneFirstNewsFeedItem.ID
            };
            db.Insert(newsFeedItemComment);
            List<NewsFeedItemCommentDTO> tmp = db.GetByUserName(tt.userOne.UserName);
            bool found = false;
            foreach (NewsFeedItemCommentDTO item in tmp)
            {
                if (item.Comment_Body == cBody)
                { found = true; }
            }

            Assert.IsNotNull(tmp);
            Assert.IsTrue(found);
        }

        [TestMethod]
        public void TestNewsFeedItemCommentBsGetAll()
        {
            var item = db.GetAll();
            Assert.IsNotNull(item);
            Assert.IsTrue(item.Count() > 0);
        }

        [TestMethod]
        public void TestNewsFeedItemCommentBsGetByID()
        {
            NewsFeedItemCommentDTO newsFeedItemComment;
            newsFeedItemComment = db.GetByID(tt.userOneCommentOnFirstNewsFeedItem.ID);
            Assert.IsNotNull(newsFeedItemComment);
        }

        [TestMethod]
        public void TestNewsFeedItemCommentBsGetByUserName()
        {
            List<NewsFeedItemCommentDTO> newsFeedItemComment;
            newsFeedItemComment = db.GetByUserName(tt.userOne.UserName);
            Assert.IsNotNull(newsFeedItemComment);
            Assert.IsTrue(newsFeedItemComment.Count() > 0);
        }


        [TestMethod]
        public void TestNewsFeedItemCommentBsUpdate()
        {
            string newBody = Guid.NewGuid().ToString();
            tt.userOneCommentOnFirstNewsFeedItem.Comment_Body = newBody;
            db.Update(tt.userOneCommentOnFirstNewsFeedItem);

            NewsFeedItemCommentDTO temp = db.GetByID(tt.userOneCommentOnFirstNewsFeedItem.ID);

            Assert.IsNotNull(temp);

            Assert.AreEqual(newBody, temp.Comment_Body);
        }

        [TestMethod]
        public void TestNewsFeedItemCommentBsDelete()
        {
            db.Delete(tt.userThreeCommentSecondComment.ID);
            NewsFeedItemCommentDTO newsFeedItemComment;
            newsFeedItemComment = db.GetByID(tt.userThreeCommentSecondComment.ID);
            Assert.IsNull(newsFeedItemComment);
        }
    }
}
