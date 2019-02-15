using BOL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFed.Tests
{
    [TestClass]
    public class BOL_UserSubscriptionsTests
    {
        TestTools tt;
        public BOL_UserSubscriptionsTests()
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
        [ExpectedException(typeof(DbUpdateException))]
        public void SubscribeUniqueIndexViolationTest()
        {
            NFedContext nfc = new NFedContext(true);
            UserSubscription item = new UserSubscription()
            {
                User_Feed_ID = tt.userOne.ID,
                User_Sub_ID = tt.userTwo.ID
            };
            nfc.UserSubscriptions.Add(item);
            nfc.SaveChanges();

            UserSubscription item2 = new UserSubscription()
            {
                User_Feed_ID = tt.userOne.ID,
                User_Sub_ID = tt.userTwo.ID
            };

            nfc.UserSubscriptions.Add(item2);
            nfc.SaveChanges();
        }
        [TestMethod]
        public void SubscribeAndUnsubscribeTest()
        {
            NFedContext nfc = new NFedContext(true);
            UserSubscription item = new UserSubscription()
            {
                User_Feed_ID = tt.userTwo.ID,
                User_Sub_ID = tt.userOne.ID
            };
            nfc.UserSubscriptions.Add(item);
            nfc.SaveChanges();
            UserSubscription tmp = nfc.UserSubscriptions.Where(x => x.User_Feed_ID == tt.userTwo.ID && x.User_Sub_ID == tt.userOne.ID).FirstOrDefault();
            Assert.IsNotNull(tmp);

            nfc.UserSubscriptions.Remove(tmp);
            nfc.SaveChanges();

            UserSubscription tmp2 = nfc.UserSubscriptions.Find(tmp.ID);
            Assert.IsNull(tmp2);
        }

    }
}
