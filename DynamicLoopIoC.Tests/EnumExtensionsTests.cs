using DynamicLoopIoC.Common.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicLoopIoC.Tests
{
    public enum EnumDescriptionAttributeTest
    {
        [System.ComponentModel.Description("Test Description Value 1")]
        TestDescription1 = 1,
        [System.ComponentModel.Description("Test Description Value 2")]
        TestDescription2 = 2
    }

    [TestClass]
    public class EnumExtensionsTests
    {
        [TestMethod]
        public void Should_Return_The_Description_Attribute_Value()
        {
            Assert.AreEqual("Test Description Value 1", EnumDescriptionAttributeTest.TestDescription1.GetDescriptionAttributeValue());
            Assert.AreEqual("Test Description Value 2", EnumDescriptionAttributeTest.TestDescription2.GetDescriptionAttributeValue());
        }
    }
}
