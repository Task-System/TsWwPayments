using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleUpdateHandler;
using SimpleUpdateHandler.CustomFilters;

namespace Payments.Tests
{
    [TestClass()]
    public class ApplyFilterAttributeTests
    {
        [TestMethod()]
        public void ApplyFilterAttributeTest()
        {
            var myType = typeof(MessageTextFilter);

            Assert.IsTrue(myType.IsAssignableTo(typeof(SimpleFilter<>)));
        }
    }
}