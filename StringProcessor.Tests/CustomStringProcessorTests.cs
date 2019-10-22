using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using StringProcessor.Core.ProcessingStage;
using StringProcessor.Core.Processor;

namespace StringProcessor.Tests
{
    class CustomStringProcessorTests
    {
        private IProcessingStage stage1;
        private IProcessingStage stage2;
        private IProcessingStage stage3;
        private IProcessingStage stage4;
        private IProcessingStage stage5;

        [OneTimeSetUp]
        public void oneTimeSetUp()
        {
            stage1 = new CustomProcessingStage("stage1", str => str + "1", null);
            stage2 = new CustomProcessingStage("stage2", str => str + "2", null);
            stage3 = new CustomProcessingStage("stage3", str => str + "3", null);
            stage4 = new CustomProcessingStage("stage4", str => str + "4", null);
            stage5 = new CustomProcessingStage("stage5", str => str + "5", null);
        }

        [Test]
        public void CustomStringProcessor_Constructor_Test()
        {
            var processor = CustomStringProcessor.FactoryCreate(cfg =>
            {
                cfg.Strings = new string[] { "str1", "str2", "str3"};

                cfg.Stage1 = stage1;
                cfg.Stage2 = stage2;
                cfg.Stage3 = stage3;
                cfg.Stage4 = stage4;
                cfg.Stage5 = stage5;
            });

            bool assert1 = processor.ProcessorQueue.Dequeue() == stage1;
            bool assert2 = processor.ProcessorQueue.Dequeue() == stage2;
            bool assert3 = processor.ProcessorQueue.Dequeue() == stage3;
            bool assert4 = processor.ProcessorQueue.Dequeue() == stage4;
            bool assert5 = processor.ProcessorQueue.Dequeue() == stage5;

            Assert.That(assert1 && assert2 && assert3 && assert4 && assert5);
        }

        [Test]
        public void CustomStringProcessor_Enqueue_Test()
        {
            var processor = CustomStringProcessor.FactoryCreate(cfg =>
            {
                cfg.Strings = new string[] { "str1", "str2", "str3" };
            });

            processor.ProcessorQueue.Enqueue(stage1);
            processor.ProcessorQueue.Enqueue(stage2);
            processor.ProcessorQueue.Enqueue(stage3);
            processor.ProcessorQueue.Enqueue(stage4);
            processor.ProcessorQueue.Enqueue(stage5);

            bool assert1 = processor.ProcessorQueue.Dequeue() == stage1;
            bool assert2 = processor.ProcessorQueue.Dequeue() == stage2;
            bool assert3 = processor.ProcessorQueue.Dequeue() == stage3;
            bool assert4 = processor.ProcessorQueue.Dequeue() == stage4;
            bool assert5 = processor.ProcessorQueue.Dequeue() == stage5;

            Assert.That(assert1 && assert2 && assert3 && assert4 && assert5);
        }

        [Test]
        public void CustomStringProcessor_QueueLimit_Is5_Test()
        {
            Assert.That(CustomStringProcessor.QueueLimit == 5);
        }
    }
}
