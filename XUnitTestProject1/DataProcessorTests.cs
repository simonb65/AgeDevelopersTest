using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AglDevelopersTest;
using Xunit;

namespace XUnitTestProject1
{
    public class DataProcessorTests
    {
        [Fact]
        public void ProcessReport_ForNullOwners_Throws()
        {
            var reportParser = new DataProcessor();

            Assert.Throws<ArgumentNullException>(() => reportParser.ProcessReport(null));
        }

        [Fact]
        public void ProcessReport_ForEmptyOwners_ReturnsEmpty()
        {
            var reportParser = new DataProcessor();

            var result = reportParser.ProcessReport(Enumerable.Empty<Owner>().ToList());
        }

        public static IEnumerable<object[]> ProcessReportSampleData()
        {
            yield return new object[]
            {
                new List<Owner>
                {
                    new Owner
                    {
                        Gender = Gender.Male,
                        Pets = new [] { new Pet { Type = PetType.Cat, Name = "One" }, new Pet { Type = PetType.Dog, Name = "Two" }, new Pet { Type = PetType.Fish, Name = "Three" } }
                    }
                },
                1,
                0
            };

            yield return new object[]
            {
                new List<Owner>
                {
                    new Owner
                    {
                        Gender = Gender.Female,
                        Pets = new [] { new Pet { Type = PetType.Cat, Name = "One" }, new Pet { Type = PetType.Dog, Name = "Two" }, new Pet { Type = PetType.Cat, Name = "Three" } }
                    },
                    new Owner
                    {
                        Gender = Gender.Female,
                        Pets = new [] { new Pet { Type = PetType.Cat, Name = "Four" }, new Pet { Type = PetType.Cat, Name = "Five" } }
                    },
                    new Owner
                    {
                        Gender = Gender.Male,
                        Pets = new [] { new Pet { Type = PetType.Cat, Name = "Six" } }
                    }
                },
                1,
                4
            };
        }

        [Theory]
        [MemberData(nameof(ProcessReportSampleData))]
        public void ProcessReport_ForSampleData_ReturnsCorrectCounts(IList<Owner> owners, int malePets, int femalePets)
        {
            var reportParser = new DataProcessor();

            var result = reportParser.ProcessReport(owners).ToList();

            Assert.NotNull(result);
            Assert.Equal(result.Count(x => x.Gender == Gender.Male), malePets);
            Assert.Equal(result.Count(x => x.Gender == Gender.Female), femalePets);
        }
    }
}
