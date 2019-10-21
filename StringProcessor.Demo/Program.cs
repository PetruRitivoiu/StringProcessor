using System;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.Logging;
using StringProcessor.Core.ProcessingStage;
using StringProcessor.Core.Processor;

namespace StringProcessor.Demo
{
    public class Program
    {
        private ILoggerFactory loggerFactory;
        private ILogger logger;

        private const string filepath = "In_999.txt";
        private const int numberOfThreads = 4;

        public Program(ILoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory;
            this.logger = loggerFactory.CreateLogger<Program>();
        }

        public void OnProcessFinished(string message)
        {
            Console.WriteLine(message);
        }

        private string[] readFile(string path)
        {
            return File.ReadAllText(path)
                .Split(Environment.NewLine.ToCharArray())
                .Where(row => !string.IsNullOrWhiteSpace(row))
                .Select(row => row.Trim())
                .ToArray();
        }

        private IStringProcessor[] createMultipleProcessors(int count, string[] strings)
        {
            var processors = new IStringProcessor[4];

            for (int i = 0; i < 4; i++)
            {
                processors[i] = CustomStringProcessor.FactoryCreate(cfg =>
                {
                    cfg.Strings = strings;

                    cfg.Stage1 = new CustomProcessingStage("stage1", str => str + "1"
                        , loggerFactory.CreateLogger<CustomProcessingStage>());
                    cfg.Stage2 = new CustomProcessingStage("stage2", str => str + "2"
                        , loggerFactory.CreateLogger<CustomProcessingStage>());
                    cfg.Stage3 = new CustomProcessingStage("stage3", str => str + "3"
                        , loggerFactory.CreateLogger<CustomProcessingStage>());
                    cfg.Stage4 = new CustomProcessingStage("stage4", str => str + "4"
                        , loggerFactory.CreateLogger<CustomProcessingStage>());
                    cfg.Stage5 = new CustomProcessingStage("stage5", str => str + "5"
                        , loggerFactory.CreateLogger<CustomProcessingStage>());
                });

                processors[i].ProcessingFinished += (sender, eventArgs) =>
                {
                    OnProcessFinished($"Processed finished with status {eventArgs.ProcessStatus} " +
                        $"on thread {Thread.CurrentThread.ManagedThreadId}; " +
                        $"Output File; {eventArgs.Filepath}");
                };
            }

            return processors;
        }

        public void Run(string[] args)
        {
            //Input files have "Copy to Ouput Directory" property set to "Copy if newer"
            string[] strings = readFile(filepath);

            //Create 4 identical processors based on the same strings array
            IStringProcessor[] processors = createMultipleProcessors(numberOfThreads, strings);

            //Start 4 threads based on the 4 string processors created earlier
            var threads = new Thread[4];

            for (int i = 0; i < numberOfThreads; i++)
            {
                threads[i] = new Thread(processors[i].StartProcessing);

                logger.LogInformation($"Started Processing file {filepath} on thread {threads[i].ManagedThreadId}");
                threads[i].Start();
                logger.LogInformation($"Finished Processing file {filepath} on thread {threads[i].ManagedThreadId}");
            }

            //Wait threads to exit
            for(int i = 0; i < numberOfThreads; i++)
            {
                threads[i].Join();
            }
        }
    }
}
