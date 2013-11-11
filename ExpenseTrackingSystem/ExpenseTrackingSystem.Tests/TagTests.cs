using ExpenseTrackingSystem.Web.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpenseNamespace = ExpenseTrackingSystem.Database.Expense;
using Assert = NUnit.Framework.Assert;

namespace ExpenseTrackingSystem.Tests
{
    [TestClass]
    public class TagTests : SetupData
    {
        [TestMethod]
        public void SuccessfulTest_AddingSharedTag()
        {
            //adding shared tag

            ExpenseNamespace.Tag tagParty = new ExpenseNamespace.Tag()
            {
                Name = "party"
            };

            TagsList.Add(tagParty);

            Assert.AreEqual(3, TagsList.Count);
        }

        [TestMethod]
        public void FailTest_AddingTag_SystemAlreadyHasSame()
        {
            //adding exists tag

            ExpenseNamespace.Tag tagParty = new ExpenseNamespace.Tag()
            {
                Name = "party",
                User = MyAccount
            };

            TagsList.Add(tagParty);

            Assert.AreEqual(3, TagsList.Count);
        }
    }
}
