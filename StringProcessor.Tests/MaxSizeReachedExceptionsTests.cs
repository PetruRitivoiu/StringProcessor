using NUnit.Framework;
using StringProcessor.Core.CustomExceptions;

namespace StringProcessor.Tests
{
    class MaxSizeReachedExceptionsTests
    {
        [Test]
        public void MaxSizeReachedException_Constructor_Test()
        {
            const string testMessage = "test-message";

            var exception = new MaxSizeReachedException(testMessage);

            Assert.That(exception.Message == testMessage);
        }
    }
}
