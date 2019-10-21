using System;
using Microsoft.Extensions.Logging;

namespace StringProcessor.Core.ProcessingStage
{
    public class CustomProcessingStage : IProcessingStage
    {
        public Func<string, string> ProcessFunc { get; }
        private readonly string name;
        private readonly ILogger logger;

		public CustomProcessingStage(Func<string, string> processFunc, ILogger logger)
        {
            this.ProcessFunc = processFunc;
            this.logger = logger;
        }

        public CustomProcessingStage(string name, Func<string, string> processFunc, ILogger logger) : this(processFunc, logger)
        {
            this.name = name;
        }

        public string Process(string str)
        {
            try
            {
                logger.LogDebug($"Started processing stage {name ?? string.Empty}; str = {str}");
                str = ProcessFunc(str);
                logger.LogDebug($"Finished processing stage {name ?? string.Empty}; str = {str}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw;
            }

            return str;
        }
    }
}
