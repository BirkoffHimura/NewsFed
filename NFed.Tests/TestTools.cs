using BOL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFed.Tests
{
    public class TestTools
    {
        public User userOne;
        public User userTwo;
        public User userThree;
        public NewsFeedItem userOneFirstNewsFeedItem;
        public NewsFeedItem userTwoFirstNewsFeedItem;
        public NewsFeedItem userThreeFirstNewsFeedItem;
        public NewsFeedItemComment userOneCommentOnFirstNewsFeedItem;
        public NewsFeedItemComment userOneCommentOnFirstNewsFeedItem2;
        public NewsFeedItemComment userThreeCommentOnFirstNewsFeedItem;
        public NewsFeedItemComment userThreeCommentSecondComment;
        public UserSubscription userTwoSubscription;
        public UserSubscription userThreeSubscription;
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
                    userOne = InsertUser("The first User", "one@email.com", "12345");
                    userTwo = InsertUser("The second User", "two@email.com", "12345");
                    userThree = InsertUser("The third User", "three@email.com", "12345");

                    sm.Users.Add(userOne);
                    sm.Users.Add(userTwo);
                    sm.Users.Add(userThree);

                    sm.SaveChanges();
                    #endregion

                    #region UserSubcriptions
                    userTwoSubscription = new UserSubscription()
                    {
                        User_Feed_ID = userOne.ID,
                        User_Sub_ID = userTwo.ID
                    };
                    sm.UserSubscriptions.Add(userTwoSubscription);
                    sm.SaveChanges();

                    userThreeSubscription = new UserSubscription()
                    {
                        User_Feed_ID = userOne.ID,
                        User_Sub_ID = userThree.ID
                    };
                    sm.UserSubscriptions.Add(userThreeSubscription);
                    sm.SaveChanges();
                    #endregion

                    #region NewsFeedItem
                    userOneFirstNewsFeedItem = new NewsFeedItem();
                    userOneFirstNewsFeedItem.CreateDate = DateTime.Now;
                    userOneFirstNewsFeedItem.Title = Guid.NewGuid().ToString();
                    userOneFirstNewsFeedItem.Body = Guid.NewGuid().ToString();
                    userOneFirstNewsFeedItem.User = userOne;
                    sm.NewsFeedItems.Add(userOneFirstNewsFeedItem);
                    sm.SaveChanges();

                    userTwoFirstNewsFeedItem = new NewsFeedItem();
                    userTwoFirstNewsFeedItem.CreateDate = DateTime.Now;
                    userTwoFirstNewsFeedItem.Title = Guid.NewGuid().ToString();
                    userTwoFirstNewsFeedItem.Body = Guid.NewGuid().ToString();
                    userTwoFirstNewsFeedItem.User = userTwo;
                    sm.NewsFeedItems.Add(userTwoFirstNewsFeedItem);
                    sm.SaveChanges();

                    userThreeFirstNewsFeedItem = new NewsFeedItem();
                    userThreeFirstNewsFeedItem.CreateDate = DateTime.Now;
                    userThreeFirstNewsFeedItem.Title = Guid.NewGuid().ToString();
                    userThreeFirstNewsFeedItem.Body = Guid.NewGuid().ToString();
                    userThreeFirstNewsFeedItem.User = userThree;
                    sm.NewsFeedItems.Add(userThreeFirstNewsFeedItem);
                    sm.SaveChanges();
                    #endregion

                    #region NewsFeedItemComments
                    userOneCommentOnFirstNewsFeedItem = new NewsFeedItemComment();
                    userOneCommentOnFirstNewsFeedItem.CommentDate = DateTime.Now;
                    userOneCommentOnFirstNewsFeedItem.Comment_Body = Guid.NewGuid().ToString();
                    userOneCommentOnFirstNewsFeedItem.User = userOne;
                    userOneCommentOnFirstNewsFeedItem.NewsFeedItem = userOneFirstNewsFeedItem;
                    sm.NewsFeedItemComments.Add(userOneCommentOnFirstNewsFeedItem);
                    sm.SaveChanges();

                    userOneCommentOnFirstNewsFeedItem2 = new NewsFeedItemComment();
                    userOneCommentOnFirstNewsFeedItem2.CommentDate = DateTime.Now;
                    userOneCommentOnFirstNewsFeedItem2.Comment_Body = Guid.NewGuid().ToString();
                    userOneCommentOnFirstNewsFeedItem2.User = userOne;
                    userOneCommentOnFirstNewsFeedItem2.NewsFeedItem = userOneFirstNewsFeedItem;
                    sm.NewsFeedItemComments.Add(userOneCommentOnFirstNewsFeedItem2);
                    sm.SaveChanges();

                    userThreeCommentOnFirstNewsFeedItem = new NewsFeedItemComment();
                    userThreeCommentOnFirstNewsFeedItem.CommentDate = DateTime.Now;
                    userThreeCommentOnFirstNewsFeedItem.Comment_Body = Guid.NewGuid().ToString();
                    userThreeCommentOnFirstNewsFeedItem.User = userThree;
                    userThreeCommentOnFirstNewsFeedItem.NewsFeedItem = userOneFirstNewsFeedItem;
                    sm.NewsFeedItemComments.Add(userThreeCommentOnFirstNewsFeedItem);
                    sm.SaveChanges();

                    userThreeCommentSecondComment = new NewsFeedItemComment();
                    userThreeCommentSecondComment.CommentDate = DateTime.Now;
                    userThreeCommentSecondComment.Comment_Body = Guid.NewGuid().ToString();
                    userThreeCommentSecondComment.User = userThree;
                    userThreeCommentSecondComment.NewsFeedItem = userThreeFirstNewsFeedItem;
                    sm.NewsFeedItemComments.Add(userThreeCommentSecondComment);
                    sm.SaveChanges();
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
        public User InsertUser(string Name, string UserName, string Password, string AddressLine1, string AddressLine2, string Country, string City, string State, string Zipcode)
        {
            User tmp = new User();
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
        public User InsertUser(string Name, string UserName, string Password, DateTime SignupDate, DateTime BirthDate, string AddressLine1, string AddressLine2, string Country, string City, string State, string Zipcode)
        {
            User tmp = new User();
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
        public User InsertUser(string Name, string UserName, string Password)
        {
            User tmp = new User();
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
        public User InsertUser(string Name, string UserName, string Password, DateTime SignupDate, DateTime BirthDate)
        {
            User tmp = new User();
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
