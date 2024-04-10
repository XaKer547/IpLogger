using IpLogger.Application.Services;
using IpLogger.DataAccess.Data;
using IpLogger.Domain.Filters;
using IpLogger.Domain.Interfaces;
using IpLogger.Tests.Helpers;
using Microsoft.Extensions.Logging;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Net;

namespace IpLogger.Tests
{
    [TestClass]
    public class LoggerServiceTests
    {
        private readonly ILogService _logService;
        private readonly LogProvider _provider;
        private readonly ILogger _logger;
        private readonly IFileSystem _fileSystem;

        public LoggerServiceTests()
        {
            var fileSystem = new MockFileSystem();

            var path = @"C:\test\file.txt";

            var mockData = MockDataHelper.GetMockData();

            fileSystem.AddFile(path, mockData);

            _fileSystem = fileSystem;

            _provider = new LogProvider(path, _fileSystem);

            var factory = new LoggerFactory();

            _logger = factory.CreateLogger("TestLogger");

            _logService = new LogService(_provider, _logger);
        }

        [TestMethod]
        public void GetLogsWithoutFilter()
        {
            var logs = _logService.GetLogs();

            Assert.IsNotNull(logs);
        }

        [TestMethod]
        public void GetLogsWithFilter()
        {
            var logs = _logService.GetLogs();

            var filter = new LogFilter()
            {
                AddressStart = IPAddress.Parse("123.51.62.143"),
                TimeStart = new DateOnly(2024, 04, 03)
            };

            var filteredLogs = _logService.FilterLogs(logs, filter);

            Assert.IsNotNull(filteredLogs);
        }

        [TestMethod]
        public void GetLogsWithBrokenFilter()
        {
            var logs = _logService.GetLogs();

            var filter = new LogFilter()
            {
                Cidr = 1,
                TimeStart = new DateOnly(2024, 04, 03)
            };

            var filteredLogs = _logService.FilterLogs(logs, filter);

            Assert.IsNotNull(filteredLogs);
        }
    }
}