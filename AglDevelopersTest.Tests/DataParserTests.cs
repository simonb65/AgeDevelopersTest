using System;
using NUnit.Framework;
using AglDevelopersTest;

namespace AglDevelopersTest.Tests
{
    [TestFixture]
    public class DataParserTests
    {
        [Test]
        [TestCase(null)]
        [TestCase("  ")]
        public void ParseData_WithEmptyData_Throws(string data)
        {
            var dataParser = new DataParser();

            //dataParser.
        }
    }
}
