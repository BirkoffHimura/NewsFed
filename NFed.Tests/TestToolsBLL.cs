using BLL;
using BOL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFed.Tests
{
    public class TestToolsBLL
    {        
        public UserDTO userOne;
        public UserDTO userTwo;
        public UserDTO userThree;
        public NewsFeedItemDTO userOneFirstNewsFeedItem;
        public NewsFeedItemDTO userTwoFirstNewsFeedItem;
        public NewsFeedItemDTO userThreeFirstNewsFeedItem;
        public NewsFeedItemCommentDTO userOneCommentOnFirstNewsFeedItem;
        public NewsFeedItemCommentDTO userOneCommentOnFirstNewsFeedItem2;
        public NewsFeedItemCommentDTO userThreeCommentOnFirstNewsFeedItem;
        public NewsFeedItemCommentDTO userThreeCommentSecondComment;
        public UserSubscriptionDTO userTwoSubscription;
        public UserSubscriptionDTO userThreeSubscription;
        public const string base64ImageTestData = "iVBORw0KGgoAAAANSUhEUgAAAEAAAABACAIAAAAlC+aJAAAABGdBTUEAALGPC/xhBQAAAAlwSFlzAAAOwwAADsMBx2+oZAAAAdJJREFUaEPtl9GtgzAMRTOXB8o8mYZlGCa95OZZJrQPVKGaSD4/TYyhPiQtJtXJCQFvQsCbEPAmBLwJAW9CwJsQ8OZcIKWc9mC+1DVJ6fPGWgTJeUF+py6YvT+9Vhy5h6srUNYqSepaOF1xphSUyClBTkJOC6LAIhggsR+yp9/IzQKfqpxDgEHBDjkU+lCBvqn32zrlBZFBY44VsEhZrcN8AkPCHAK46/wzBfgHnWAF+AMgKN0GH/cbeCwh4E0IeBMC3oSAN5cE0GAuecvUZ+3QRPBFDJ10S+mwLT32S3gqtyd1hwlvH+3M/59vBES2rn+oNedsg+itJWXJOLD7CqqqFdJywnjhlY+2p3wjgAmKs41aToIUK4BCMUV3h0P6qoAPvDu8rfKnAvgaRHSJ0YS2wFaxCjCB+bZF/fRG/2sBLYVbBathBWzv2ervcSuAOPc6I7wyI+CT5xHknXMU0M2gdVsBW7SVsQJEI3rlfuAyOPmcowDG3DncKpiqAHP6nfyDOaqNMfEU4L+h3lEVwGB4m4GqpnHnaKGeAhwzDlRAk5Xhxg/PAS4Or9ZDDcZPuSTwZELAmxDwJgS8CQFvQsCbEPAmBLwJAW9CwJsQ8GZygVpfagSBBcEuQ38AAAAASUVORK5CYII=";
        #region Gobal
        public static void CleanUpDb(string ConsDes)
        {
            try
            {
                using (NFedContext sm = new NFedContext(true))
                {
                    Console.Out.WriteLine("Executing clean up from [" + ConsDes + "]...");

                    var listOfTables = new List<string> { "NewsFeedItemComments", "NewsFeedItems", "UserSubscriptions", "Users" };
                    #region CommentedOut
                    foreach (var tableName in listOfTables)
                    {
                        try
                        {
                            sm.Database.ExecuteSqlCommand("DELETE FROM [" + tableName + "]");
                            sm.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            Console.Out.WriteLine("Delete records from Tables[" + tableName + "]...: " + ex.Message);
                        }
                    }
                    #endregion

                }
            }
            catch { }

        }
        public void SetupInitialUserAccounts()
        {
            using (NFedContext sm = new NFedContext(true))
            {
                try
                {
                    if (!sm.Database.CreateIfNotExists())
                    {
                        Console.Out.WriteLine("Could not create Database...");
                    }

                    #region Users
                    UserBs ubs = new UserBs(true);
                    userOne = InsertUser("The first User", "one@email.com", "12345");
                    userTwo = InsertUser("The second User", "two@email.com", "12345");
                    userThree = InsertUser("The third User", "three@email.com", "12345");

                    ubs.Insert(userOne);
                    ubs.Insert(userTwo);
                    ubs.Insert(userThree);

                    userOne = ubs.GetByUserName(userOne.UserName);
                    userTwo = ubs.GetByUserName(userTwo.UserName);
                    userThree = ubs.GetByUserName(userThree.UserName);
                    #endregion

                    #region UserSubcriptions
                    UserSubscriptionBs usbs = new UserSubscriptionBs(true);
                    userTwoSubscription = new UserSubscriptionDTO()
                    {
                        User_Feed_ID = userOne.ID,
                        User_Sub_ID = userTwo.ID
                    };
                    usbs.Insert(userTwoSubscription);
                    userTwoSubscription = usbs.GetByFeedAndSubID(userOne.ID, userTwo.ID);
                    
                    userThreeSubscription = new UserSubscriptionDTO()
                    {
                        User_Feed_ID = userOne.ID,
                        User_Sub_ID = userThree.ID
                    };
                    usbs.Insert(userThreeSubscription);
                    userThreeSubscription = usbs.GetByFeedAndSubID(userOne.ID, userThree.ID);
                    #endregion

                    #region NewsFeedItem
                    NewsFeedItemBs nfiBs = new NewsFeedItemBs(true);
                    userOneFirstNewsFeedItem = new NewsFeedItemDTO();
                    userOneFirstNewsFeedItem.CreateDate = DateTime.Now;
                    userOneFirstNewsFeedItem.Title = Guid.NewGuid().ToString();
                    userOneFirstNewsFeedItem.Body = Guid.NewGuid().ToString();
                    userOneFirstNewsFeedItem.User = userOne;
                    nfiBs.Insert(userOneFirstNewsFeedItem);

                    userOneFirstNewsFeedItem = nfiBs.GetAll().Where(x => x.Title == userOneFirstNewsFeedItem.Title).FirstOrDefault();

                    userTwoFirstNewsFeedItem = new NewsFeedItemDTO();
                    userTwoFirstNewsFeedItem.CreateDate = DateTime.Now;
                    userTwoFirstNewsFeedItem.Title = Guid.NewGuid().ToString();
                    userTwoFirstNewsFeedItem.Body = Guid.NewGuid().ToString();
                    userTwoFirstNewsFeedItem.User = userTwo;
                    nfiBs.Insert(userTwoFirstNewsFeedItem);

                    userTwoFirstNewsFeedItem = nfiBs.GetAll().Where(x => x.Title == userTwoFirstNewsFeedItem.Title).FirstOrDefault();

                    userThreeFirstNewsFeedItem = new NewsFeedItemDTO();
                    userThreeFirstNewsFeedItem.CreateDate = DateTime.Now;
                    userThreeFirstNewsFeedItem.Title = Guid.NewGuid().ToString();
                    userThreeFirstNewsFeedItem.Body = Guid.NewGuid().ToString();
                    userThreeFirstNewsFeedItem.User = userThree;
                    nfiBs.Insert(userThreeFirstNewsFeedItem);

                    userThreeFirstNewsFeedItem = nfiBs.GetAll().Where(x => x.Title == userThreeFirstNewsFeedItem.Title).FirstOrDefault();
                    #endregion

                    #region NewsFeedItemComments
                    NewsFeedItemCommentBs nficBs = new NewsFeedItemCommentBs(true);
                    userOneCommentOnFirstNewsFeedItem = new NewsFeedItemCommentDTO();
                    userOneCommentOnFirstNewsFeedItem.CommentDate = DateTime.Now;
                    userOneCommentOnFirstNewsFeedItem.Comment_Body = Guid.NewGuid().ToString();
                    userOneCommentOnFirstNewsFeedItem.CommentUserID = userOne.ID;
                    userOneCommentOnFirstNewsFeedItem.NewsFeedItemID = userOneFirstNewsFeedItem.ID;
                    nficBs.Insert(userOneCommentOnFirstNewsFeedItem);

                    userOneCommentOnFirstNewsFeedItem = nficBs.GetAll().Where(x => x.Comment_Body == userOneCommentOnFirstNewsFeedItem.Comment_Body).FirstOrDefault();

                    userOneCommentOnFirstNewsFeedItem2 = new NewsFeedItemCommentDTO();
                    userOneCommentOnFirstNewsFeedItem2.CommentDate = DateTime.Now;
                    userOneCommentOnFirstNewsFeedItem2.Comment_Body = Guid.NewGuid().ToString();
                    userOneCommentOnFirstNewsFeedItem2.CommentUserID = userOne.ID;
                    userOneCommentOnFirstNewsFeedItem2.NewsFeedItemID = userOneFirstNewsFeedItem.ID;
                    nficBs.Insert(userOneCommentOnFirstNewsFeedItem2);

                    userOneCommentOnFirstNewsFeedItem2 = nficBs.GetAll().Where(x => x.Comment_Body == userOneCommentOnFirstNewsFeedItem2.Comment_Body).FirstOrDefault();


                    userThreeCommentOnFirstNewsFeedItem = new NewsFeedItemCommentDTO();
                    userThreeCommentOnFirstNewsFeedItem.CommentDate = DateTime.Now;
                    userThreeCommentOnFirstNewsFeedItem.Comment_Body = Guid.NewGuid().ToString();
                    userThreeCommentOnFirstNewsFeedItem.CommentUserID = userThree.ID;
                    userThreeCommentOnFirstNewsFeedItem.NewsFeedItemID = userOneFirstNewsFeedItem.ID;
                    nficBs.Insert(userThreeCommentOnFirstNewsFeedItem);

                    userThreeCommentOnFirstNewsFeedItem = nficBs.GetAll().Where(x => x.Comment_Body == userThreeCommentOnFirstNewsFeedItem.Comment_Body).FirstOrDefault();

                    userThreeCommentSecondComment = new NewsFeedItemCommentDTO();
                    userThreeCommentSecondComment.CommentDate = DateTime.Now;
                    userThreeCommentSecondComment.Comment_Body = Guid.NewGuid().ToString();
                    userThreeCommentSecondComment.CommentUserID = userThree.ID;
                    userThreeCommentSecondComment.NewsFeedItemID = userThreeFirstNewsFeedItem.ID;
                    nficBs.Insert(userThreeCommentSecondComment);

                    userThreeCommentSecondComment = nficBs.GetAll().Where(x => x.Comment_Body == userThreeCommentSecondComment.Comment_Body).FirstOrDefault();
                    #endregion
                }
                catch (Exception ex)
                {
                    Console.Out.WriteLine("Creating initial records...: " + ex.Message);
                }
            }
        }
        #endregion

        #region Users
        public UserDTO InsertUser(string Name, string UserName, string Password, string AddressLine1, string AddressLine2, string Country, string City, string State, string Zipcode)
        {
            UserDTO tmp = new UserDTO();
            tmp.Name = Name;
            tmp.UserName = UserName;
            tmp.Password = Password;
            tmp.SignupDate = DateTime.Now;
            tmp.BirthDate = DateTime.Now;
            tmp.AddressLine1 = AddressLine1;
            tmp.AddressLine2 = AddressLine2;
            tmp.Country = Country;
            tmp.City = City;
            tmp.State = State;
            tmp.ZipCode = Zipcode;
            tmp.AllowPost = false;
            tmp.AdminAcct = false;
            tmp.ProfilePic = "noimage.png";
            return tmp;
        }
        public UserDTO InsertUser(string Name, string UserName, string Password, DateTime SignupDate, DateTime BirthDate, string AddressLine1, string AddressLine2, string Country, string City, string State, string Zipcode)
        {
            UserDTO tmp = new UserDTO();
            tmp.Name = Name;
            tmp.UserName = UserName;
            tmp.Password = Password;
            tmp.SignupDate = SignupDate;
            tmp.BirthDate = BirthDate;
            tmp.AddressLine1 = AddressLine1;
            tmp.AddressLine2 = AddressLine2;
            tmp.Country = Country;
            tmp.City = City;
            tmp.State = State;
            tmp.ZipCode = Zipcode;
            tmp.AllowPost = false;
            tmp.AdminAcct = false;
            tmp.ProfilePic = "noimage.png";
            return tmp;
        }
        public UserDTO InsertUser(string Name, string UserName, string Password)
        {
            UserDTO tmp = new UserDTO();
            tmp.Name = Name;
            tmp.UserName = UserName;
            tmp.Password = Password;
            tmp.SignupDate = DateTime.Now;
            tmp.BirthDate = DateTime.Now;
            tmp.AllowPost = false;
            tmp.AdminAcct = false;
            tmp.ProfilePic = "noimage.png";
            return tmp;
        }
        public UserDTO InsertUser(string Name, string UserName, string Password, DateTime SignupDate, DateTime BirthDate)
        {
            UserDTO tmp = new UserDTO();
            tmp.Name = Name;
            tmp.UserName = UserName;
            tmp.Password = Password;
            tmp.SignupDate = SignupDate;
            tmp.BirthDate = BirthDate;
            tmp.AllowPost = false;
            tmp.AdminAcct = false;
            tmp.ProfilePic = "noimage.png";
            return tmp;
        }
        #endregion
        public string CreateTestImage()
        {

            string testFolderPath = System.Configuration.ConfigurationManager.AppSettings["TestImageFolder"];
            if (!Directory.Exists(testFolderPath))
            {
                Directory.CreateDirectory(testFolderPath);
            }
            string ImageName = Guid.NewGuid().ToString() + ".png";
            string fileName = Path.Combine(testFolderPath, ImageName);
            var bytes = Convert.FromBase64String(base64ImageTestData);
            using (var imageFile = new FileStream(fileName, FileMode.Create))
            {
                imageFile.Write(bytes, 0, bytes.Length);
                imageFile.Flush();
            }
            return ImageName;
        }
        public void CleanupTestFolder()
        {
            string testFolderToCleanup = System.Configuration.ConfigurationManager.AppSettings["TestFolder"];
            deleteFolderContent(testFolderToCleanup);
        }
        private void deleteFolderContent(string path)
        {
            string[] fileList = Directory.GetFiles(path);
            foreach (string file in fileList)
            {
                File.Delete(file);
            }
            string[] directories = Directory.GetDirectories(path);
            foreach (string directory in directories)
            {
                deleteFolderContent(directory);
                Directory.Delete(directory);
            }
        }
    }
}
