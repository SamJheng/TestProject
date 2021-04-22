
using NUnit.Framework.Internal;
using System;

namespace TestProject
{
    public interface ILogger
    {
        public void LogError(string s);
    }
    class LogAnalyzer
    {
        private ILogger logger;

        public LogAnalyzer(ILogger logger, IWebService mockWebService=null)
        {
            this.logger = logger;
        }
        public int MinNameLength { get; set; }
        public void Analyze(string v)
        {
        }
    }
}