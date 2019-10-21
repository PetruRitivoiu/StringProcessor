using System;
using NUnit.Framework;
using StringProcessor.Core.CustomCollections;
using StringProcessor.Core.CustomExceptions;
using StringProcessor.Core.ProcessingStage;
using System.ComponentModel;

namespace StringProcessor.Tests
{
    class ProcessorQueueTests
    {
        [Test]
        public void ProcessorQueue_LimitProperty_IsReadOnly_Test()
        {
            var processorQueue = new ProcessorQueue<IProcessingStage>(5);

            var prop = processorQueue.GetType().GetProperty(nameof(processorQueue.Limit));

            var attrib = Attribute.GetCustomAttribute(prop, typeof(ReadOnlyAttribute)) as ReadOnlyAttribute;
            bool isLimitReadOnly = !prop.CanWrite || (attrib != null && attrib.IsReadOnly);

            Assert.That(isLimitReadOnly);
        }

        [TestCase(0)]
        [TestCase(5)]
        public void ProcessorQueue_LimitProperty_IsSetCorrectly_Test(int limit)
        {
            var processorQueue = new ProcessorQueue<IProcessingStage>(limit);

            Assert.That(limit == processorQueue.Limit);
        }

        [TestCase(0)]
        [TestCase(5)]
        public void ProcessorQueue_MaxSizeException_Test(int limit)
        {
            bool causedException = false;

            try
            {
                var processorQueue = new ProcessorQueue<IProcessingStage>(limit);

                for (int i = 0; i < limit + 1; i++)
                {
                    processorQueue.Enqueue(new CustomProcessingStage(null, null));
                }
            }
            catch (MaxSizeReachedException)
            {
                causedException = true;
            }

            Assert.That(causedException);
        }

        [Test]
        public void ProcessorQueue_EnqueueDequeue_KeepsOrder_Test()
        {
            IProcessingStage[] processingStages = {
                new CustomProcessingStage("stage1", null, null),
                new CustomProcessingStage("stage2", null, null),
                new CustomProcessingStage("stage3", null, null)
            };

            var processorQueue = new ProcessorQueue<IProcessingStage>(3);

            foreach (var stage in processingStages)
            {
                processorQueue.Enqueue(stage);
            }

            var stage1 = processorQueue.Dequeue();
            var stage2 = processorQueue.Dequeue();
            var stage3 = processorQueue.Dequeue();

            Assert.That(stage1.Equals(processingStages[0])
                && stage2.Equals(processingStages[1])
                && stage3.Equals(processingStages[2]));
        }

        [Test]
        public void ProcessorQueue_EmptyDequeue_InvalidOperationException_Test()
        {
            bool causedException = false;

            try
            {
                var processorQueue = new ProcessorQueue<IProcessingStage>(0);
                processorQueue.Dequeue();
            }
            catch (InvalidOperationException)
            {
                causedException = true;
            }

            Assert.That(causedException);
        }

        [Test]
        public void ProcessorQueue_Peek_Test()
        {
            var processorQueue = new ProcessorQueue<IProcessingStage>(1);

            var processingStage = new CustomProcessingStage(null, null);
            processorQueue.Enqueue(processingStage);

            var peekResult = processorQueue.Peek();

            Assert.That(processingStage.Equals(peekResult) && processorQueue.Count == 1);
        }

        [Test]
        public void ProcessorQueue_Any_Test()
        {
            var processorQueue = new ProcessorQueue<IProcessingStage>(1);
            bool initial = processorQueue.Any();

            processorQueue.Enqueue(new CustomProcessingStage(null, null));
            bool mid = processorQueue.Any();

            processorQueue.Dequeue();
            bool final = processorQueue.Any();

            Assert.That(!initial && mid && !final);
        }

        [Test]
        public void ProcessorQueue_Count_Test()
        {
            var processorQueue = new ProcessorQueue<IProcessingStage>(1);
            int initial = processorQueue.Count;

            processorQueue.Enqueue(new CustomProcessingStage(null, null));
            int mid = processorQueue.Count;

            processorQueue.Dequeue();
            int final = processorQueue.Count;

            Assert.That(initial == 0 && mid == 1 && final == 0);
        }

        [Test]
        public void ProcessorQueue_ToReadOnlyCollection_KeepsOrder()
        {
            IProcessingStage[] processingStages = {
                new CustomProcessingStage("stage1", null, null),
                new CustomProcessingStage("stage2", null, null),
                new CustomProcessingStage("stage3", null, null)
            };

            var processorQueue = new ProcessorQueue<IProcessingStage>(3);

            foreach (var stage in processingStages)
            {
                processorQueue.Enqueue(stage);
            }

            var stagesToReadOnlyCollection = processorQueue.ToReadOnlyCollection();

            for (int i = 0; i < 3; i++)
            {
                Assert.That(processingStages[i].Equals(stagesToReadOnlyCollection[i]));
            }
        }
    }
}
