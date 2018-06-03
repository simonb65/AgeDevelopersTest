using System;
using System.Collections.Generic;
using System.Linq;

namespace AglDevelopersTest
{
    // Perform any data processing required to extract required report data from raw 

    public interface IDataProcessor
    {
        IEnumerable<(Gender, string[])> ProcessReport(IList<Owner> owners);
    }

    public class DataProcessor : IDataProcessor
    {
        public IEnumerable<(Gender, string[])> ProcessReport(IList<Owner> owners)
        {
            if (owners == null)
                throw new ArgumentNullException(nameof(owners));

            var noPets = new Pet[0];

            var reportData = owners
                .SelectMany(x => (x.Pets ?? noPets)
                    .Where(p => p.Type == PetType.Cat)          // Only want Cats
                    .Select(p => new { x.Gender, p.Name }))     // Project to Owner Gender and Pet Name (Could also tak Owner/Pet ...)
                .GroupBy(x => x.Gender, x => x.Name)            // Group by Owners Gender taking Pet Name
                .Select(x => (x.Key, x.ToArray()));             

            return reportData;
        }
    }
}
