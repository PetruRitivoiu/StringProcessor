using NUnit.Framework;
using StringProcessor.Core.Processor;

namespace StringProcessor.Tests
{
    class CustomStringProcessorTests
    {
        [Test]
        public void CustomStringProcessor_Constructor_Test()
        {
            Assert.That(CustomStringProcessor.QueueLimit == 5);
        }
    }
}
