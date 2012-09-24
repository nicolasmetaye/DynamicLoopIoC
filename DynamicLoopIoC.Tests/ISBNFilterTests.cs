using DynamicLoopIoC.Common.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicLoopIoC.Tests
{
    [TestClass]
    public class ISBNFilterTests
    {
        [TestMethod]
        public void Should_Filter_The_ISBN_Prefix()
        {
            Assert.AreEqual("9781234567897", ISBNFilter.Filter("ISBN9781234567897"));
        }

        [TestMethod]
        public void Should_Filter_The_ISBN_Separators()
        {
            Assert.AreEqual("9781234567897", ISBNFilter.Filter("978-1-234-56789-7"));
        }
    }
}
