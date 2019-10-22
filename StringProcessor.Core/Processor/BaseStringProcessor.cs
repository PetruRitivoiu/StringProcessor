using System;
using StringProcessor.Core.CustomCollections;
using StringProcessor.Core.ProcessingStage;

namespace StringProcessor.Core.Processor
{
    public abstract class BaseStringProcessor : IStringProcessor
    {
        protected string[] strings;

        private readonly IProcessingStage[] processingStages;

        public abstract event ProcessFinishedEventHandler ProcessingFinished;

        public IProcessorQueue<IProcessingStage> ProcessorQueue { get; }

        protected BaseStringProcessor(string[] strings, IProcessingStage[] processingStages, int limit)
        {
            this.strings = strings;
            this.processingStages = processingStages;

            ProcessorQueue = new ProcessorQueue<IProcessingStage>(limit);
            InitQueue();
        }

        protected void InitQueue()
        {
            foreach (var stage in processingStages)
            {
                if (stage != null)
                {
                    ProcessorQueue.Enqueue(stage);
                }
            }
        }

        protected void Validate()
        {
            if (strings.Length >= 1000)
            {
                throw new Exception($"Maximum number of strings to be processed is 999." +
                    $"{strings.Length} have been passed for procesing.");
            }
        }

        public abstract void StartProcessing();
    }
}
