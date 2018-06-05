using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AglDevelopersTest
{
    // Parse data structures from Raw text

    public interface IDataParser
    {
        IList<Owner> ParseData(string data);
    }

    public class DataParser : IDataParser
    {
        public IList<Owner> ParseData(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
                throw new ArgumentNullException(nameof(data));

            var results = JsonConvert.DeserializeObject<List<Owner>>(data);

            return results;
        }
    }
}
