using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace QuickAnswer
{
    public class Pet
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
    public class Person
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public Pet[] Pets { get; set; }
    }
    
    class Program
    {
        public static async Task Main(string[] args)
        {
            var data = await new WebClient().DownloadStringTaskAsync("http://agl-developer-test.azurewebsites.net/people.json");
            var results = JsonConvert.DeserializeObject<List<Person>>(data);

            foreach(var r in results
                .Where(x => x.Pets != null)
                .SelectMany(x => x.Pets
                    .Where(p => p.Type == "Cat")
                    .Select(p => new { x.Gender, p.Name }))
                .GroupBy(x => x.Gender)
                .OrderByDescending(x => x.Key))
            {
                Debug.WriteLine(r.Key);
                Debug.Indent();
                foreach (var z in r.OrderBy(x => x.Name))
                    Debug.WriteLine(z.Name);
                Debug.Unindent();
            }
        }
    }
}
