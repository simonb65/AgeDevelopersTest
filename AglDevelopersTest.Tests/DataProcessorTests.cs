using System;
using System.Collections.Generic;
using System.Linq;
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

            var results = reportParser.ProcessReport(Enumerable.Empty<Owner>().ToList());

            Assert.Empty(results);
        }

        public static IEnumerable<object[]> ProcessReportSampleData()
        {
            // Only 1 Gender
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

            // Mixed Gender and pets
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

            // Null pets
            yield return new object[]
            {
                new List<Owner>
                {
                    new Owner
                    {
                        Gender = Gender.Female,
                        Pets = null
                    },
                    new Owner
                    {
                        Gender = Gender.Male,
                        Pets = new [] { new Pet { Type = PetType.Cat, Name = "Six" } }
                    }
                },
                1,
                0
            };
        }

        [Theory]
        [MemberData(nameof(ProcessReportSampleData))]
        public void ProcessReport_ForSampleData_ReturnsCorrectCounts(IList<Owner> owners, int malePets, int femalePets)
        {
            var reportParser = new DataProcessor();
            var results = reportParser.ProcessReport(owners).ToList();

            int PetCountForGender(Gender gender) => results.Any(x => x.Gender == gender) ? results.Single(x => x.Gender == gender).Names?.Length ?? 0 : 0;            
            
            Assert.NotNull(results);
            Assert.Equal(PetCountForGender(Gender.Male), malePets);
            Assert.Equal(PetCountForGender(Gender.Female), femalePets);
        }
    }
}
