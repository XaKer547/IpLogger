using IpLogger.Console.Commands.Enums;
using IpLogger.Tests.Helpers;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;

namespace IpLogger.Tests
{
    [TestClass]
    public class ConsoleApplicationTests
    {
        private readonly IFileSystem _fileSystem;
        private const string FileOutput = @"C:\Users\user\Desktop\logs1.txt";
        private const string FileLog = @"C:\Users\user\Desktop\logs.txt";

        public ConsoleApplicationTests()
        {
            var fileSystem = new MockFileSystem();

            var mockData = MockDataHelper.GetMockData();

            fileSystem.AddFile(FileLog, mockData);

            _fileSystem = fileSystem;
        }

        [TestMethod]
        public async Task RunApplicationSuccessfuly()
        {
            var args = new string[]
            {
                "--file-log",
                FileLog,
                "--file-output",
                FileOutput,
                "--address-start",
                "192.168.1.0",
                "--address-mask",
                "5",
                "--time-start",
                "10.12.2004",
                "--time-end",
                "10.12.2055"
            };

            var exitValue = await Console.Program.Main(args);

            var exitCode = (ExitCodes)exitValue;

            Assert.AreEqual(ExitCodes.Success, exitCode);
        }

        [TestMethod]
        public async Task RunApplicationWithoutLogPathOption()
        {
            var args = new string[]
            {
                "--file-output",
                FileOutput,
                "--address-start",
                "192.168.1.0",
                "--address-mask",
                "5",
                "--time-start",
                "10.12.2004",
                "--time-end",
                "10.12.2055"
            };

            var exitValue = await Console.Program.Main(args);

            var exitCode = (ExitCodes)exitValue;

            Assert.IsTrue(exitCode.HasFlag(ExitCodes.InvalidData));
        }

        [TestMethod]
        public async Task RunApplicationWithFakeLogPathOption()
        {
            var args = new string[]
            {
                "--file-log",
                @"M:\ae\ae.txt",
                "--file-output",
                @"M:\ae\ae.txt",
                "--address-start",
                "192.168.1.0",
                "--address-mask",
                "5",
                "--time-start",
                "10.12.2004",
                "--time-end",
                "10.12.2055"
            };

            var exitValue = await Console.Program.Main(args);

            var exitCode = (ExitCodes)exitValue;

            Assert.IsTrue(exitCode.HasFlag(ExitCodes.FileNotFound));
        }

        [TestMethod]
        public async Task RunApplicationWithoutOutputLogPathOption()
        {
            var args = new string[]
           {
                "--file-log",
                FileLog,
                "--address-start",
                "192.168.1.0",
                "--address-mask",
                "5",
                "--time-start",
                "10.12.2004",
                "--time-end",
                "10.12.2055"
           };

            var exitValue = await Console.Program.Main(args);

            var exitCode = (ExitCodes)exitValue;

            Assert.IsTrue(exitCode.HasFlag(ExitCodes.InvalidData));
        }

        [TestMethod]
        public async Task RunApplicationWithAddreesMaskOptionWithoutAddressStart()
        {
            var args = new string[]
            {
                "--file-log",
                FileLog,
                "--file-output",
                FileOutput,
                "--address-mask",
                "5",
                "--time-start",
                "10.12.2004",
                "--time-end",
                "10.12.2055"
            };

            var exitValue = await Console.Program.Main(args);

            var exitCode = (ExitCodes)exitValue;

            Assert.IsTrue(exitCode.HasFlag(ExitCodes.ArgumentError));
        }

        [TestMethod]
        public async Task RunApplicationWithBadOptions()
        {
            var args = new string[]
            {
                "--file-log",
                "--time-start",
                "--file-output",
                "5",
                "5",
                "--time-start",
                "10.12.2004",
                "--time-end",
                "10.12.2055"
            };

            var exitValue = await Console.Program.Main(args);

            var exitCode = (ExitCodes)exitValue;

            Assert.IsTrue(exitCode.HasFlag(ExitCodes.InvalidData));
        }
    }
}
