using System;
using System.Configuration;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace AglDevelopersTest
{
    // Load data from Configured source
    public interface IDataProvider
    {
        Task<string> GetData();
    }

    public class WebDataProvider : IDataProvider, IDisposable
    {
        public const string SourceUrlAppKey = "SourceUrl";

        private readonly IConfiguration _configuration;
        public WebDataProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private WebClient _webClient;

        public async Task<string> GetData()
        {
            var sourceUrl = _configuration[SourceUrlAppKey];
            if (string.IsNullOrWhiteSpace(sourceUrl))
                throw new ConfigurationErrorsException($"Error: No configuration provided for {SourceUrlAppKey}");

            if (_webClient == null)
                _webClient = new WebClient();

            var data = await _webClient.DownloadStringTaskAsync(sourceUrl);

            return data;
        }

        public void Dispose()
        {
            _webClient?.Dispose();
            _webClient = null;
        }
    }
}
