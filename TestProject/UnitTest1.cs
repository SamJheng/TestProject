
using NSubstitute;
using NUnit.Framework;
using System;

namespace TestProject
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var calculator = Substitute.For<ICalculator>();
            calculator.Add(1, 2).Returns(3);
            Assert.That(calculator.Add(1, 2), Is.EqualTo(3));
            Console.WriteLine(calculator.Add(1, 2));

           
        }
        [Test]
        public void Analyze_TooShortFileName_CallLogger()
        {
            // 惠砞﹚ILogger interface
            ILogger logger = Substitute.For<ILogger>();
            LogAnalyzer analyzer = new LogAnalyzer(logger);
            analyzer.MinNameLength = 6;
            analyzer.Analyze("a.txt");
            // 高拜硂安ン琌砆㊣筁
            //logger.Received().LogError("FileName too short:a.txt");
        }
        [Test]
        public void Returns_ByDefault_WorksForHardCodeArgument()
        {
            IFileNameRule fakeRules = Substitute.For<IFileNameRule>();
            //fakeRules.IsValidLogFileName("strict.txt").Returns(true);
            // ┛菠把计ず甧
            fakeRules.IsValidLogFileName(Arg.Any<String>()).Returns(true);
            Assert.IsTrue(fakeRules.IsValidLogFileName("strict.txt"));
        }
        [Test]
        public void Analye_LoggerThrows_CallsWebServiceWithNSubObjectCompare()
        {
            var mockWebService = Substitute.For<IWebService>();
            var stubLogger = Substitute.For<ILogger>();

            stubLogger
                .When(logger => logger.LogError(Arg.Any<string>()))
                .Do(info => throw new Exception("fake Exception") );
            var analyzer = new LogAnalyzer(stubLogger, mockWebService);
            analyzer.MinNameLength = 10;
            analyzer.Analyze("Short.txt");
            mockWebService.Received().Wrtie(Arg.Is<String>(s => s.Contains("fake Exception")));
            //mockWebService.Received().Wrtie(Arg.Is<string>(info =>
            //{
            //    return info.Severity == 1000 && info.Message.Contains("fake Exception");
            //}));

            //var expected = new ErrorInfo(1000, "fake Exception");
            //mockWebService.Received().ErrorInfoWrtie(expected);
        }
        [Test]
        public void Analye_LoggerThrows_CallsWebService()
        {
            var mockWebService = Substitute.For<IWebService>();
            var stubLogger = Substitute.For<ILogger>();

            stubLogger
                .When(logger => logger.LogError(Arg.Any<string>()))
                .Do(info => throw new Exception("fake Exception"));
            var analyzer = new LogAnalyzer(stubLogger, mockWebService);
            analyzer.MinNameLength = 10;
            analyzer.Analyze("Short.txt");
            mockWebService.Received().Wrtie(Arg.Is<String>(s => s.Contains("fake Exception")));
        }
    }

    
}