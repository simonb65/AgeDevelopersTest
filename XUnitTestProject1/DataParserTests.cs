using System;
using AglDevelopersTest;
using Xunit;

namespace XUnitTestProject1
{
    public class DataParserTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("  ")]
        public void ParseData_WithEmptyData_Throws(string data)
        {
            var dataParser = new DataParser();

            Assert.Throws<ArgumentNullException>(() => dataParser.ParseData(data));
        }
    }
}
