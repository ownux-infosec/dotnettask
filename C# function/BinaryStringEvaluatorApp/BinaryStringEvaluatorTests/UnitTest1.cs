using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BinaryStringEvaluatorApp.Tests
{
    [TestClass]
    public class BinaryStringEvaluatorTests
    {
        [TestMethod]
        public void TestGoodStrings()
        {
            Assert.IsTrue(BinaryStringEvaluator.IsGoodBinaryString("0"));
            Assert.IsTrue(BinaryStringEvaluator.IsGoodBinaryString("010101"));
            Assert.IsTrue(BinaryStringEvaluator.IsGoodBinaryString("1"));
        }

        [TestMethod]
        public void TestBadStrings()
        {
            Assert.IsFalse(BinaryStringEvaluator.IsGoodBinaryString("11"));
            Assert.IsFalse(BinaryStringEvaluator.IsGoodBinaryString("110"));
            Assert.IsFalse(BinaryStringEvaluator.IsGoodBinaryString("1001"));
        }
    }
}
