using System;
using System.Collections.Generic;
using System.Linq;

namespace AglDevelopersTest
{
    public interface IDataReportWriter
    {
        void WriteReport(IEnumerable<(Gender gender, string[] names)> report);
    }

    public class DataReportWriter : IDataReportWriter
    {
        public void WriteReport(IEnumerable<(Gender gender, string[] names)> report)
        {
            foreach (var (gender, names) in report.OrderByDescending(x => x.gender))
            {
                Console.WriteLine(gender);
                foreach (var name in names.OrderBy(x => x))
                    Console.WriteLine("  - " + name);
            }
        }
    }
}
