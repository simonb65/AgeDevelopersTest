using System;
using System.Threading.Tasks;

namespace AglDevelopersTest
{
    public interface IDataReport
    {
        Task RunReportAsync();
    }

    public class DataReport : IDataReport
    {
        private readonly IDataProvider _dataProvider;
        private readonly IDataParser _dataParser;
        private readonly IDataProcessor _dataProcessor;
        private readonly IDataReportWriter _dataReportWriter;

        public DataReport(
            IDataProvider dataProvider,
            IDataParser dataParser,
            IDataProcessor dataProcessor,
            IDataReportWriter dataReportWriter)
        {
            _dataProvider = dataProvider;
            _dataParser = dataParser;
            _dataProcessor = dataProcessor;
            _dataReportWriter = dataReportWriter;
        }

        public async Task RunReportAsync()
        {
            var data = await _dataProvider.GetData();
            var records = _dataParser.ParseData(data);
            var report = _dataProcessor.ProcessReport(records);

            _dataReportWriter.WriteReport(report);
        }
    }
}
