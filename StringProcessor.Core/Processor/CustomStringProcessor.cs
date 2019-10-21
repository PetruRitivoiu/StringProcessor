using System;
using System.IO;
using System.Text;
using System.Threading;
using StringProcessor.Core.ProcessingStage;

namespace StringProcessor.Core.Processor
{
    public class CustomStringProcessor : BaseStringProcessor
    {
        public static int QueueLimit => 5;
        public class Config
        {
            public string[] Strings { get; set; }

            public IProcessingStage Stage1 { get; set; }
            public IProcessingStage Stage2 { get; set; }
            public IProcessingStage Stage3 { get; set; }
            public IProcessingStage Stage4 { get; set; }
            public IProcessingStage Stage5 { get; set; }
        }

        private CustomStringProcessor(string[] strings, params IProcessingStage[] processingStages)
            : base(strings, processingStages, QueueLimit)
        {
        }

        public override event ProcessFinishedEventHandler ProcessingFinished;

        public static CustomStringProcessor FactoryCreate(Action<Config> initializer)
        {
            var config = new Config();

            initializer(config);

            return new CustomStringProcessor(config.Strings, config.Stage1, config.Stage2, config.Stage3, config.Stage4, config.Stage5);
        }

        private void InvokeProcessFinished(string filepath)
        {
            ProcessingFinished?.Invoke(this, new ProcessFinishedEventArgs(ProcessStatus.Success, filepath));
        }

        public override void StartProcessing()
        {
            var sb = new StringBuilder();
            foreach (var str in strings)
            {
                string tempStr = str;
                foreach (var stage in ProcessorQueue.ToReadOnlyCollection())
                {
                    tempStr = stage.Process(str);
                }

                sb.Append(tempStr).Append(Environment.NewLine);
            }

            var filepath = $"Out_{Thread.CurrentThread.ManagedThreadId}.txt";
            File.WriteAllText(filepath, sb.ToString());

            InvokeProcessFinished(filepath);
        }
    }
}
