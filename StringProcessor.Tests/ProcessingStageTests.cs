using System;
using System.ComponentModel;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using StringProcessor.Core.ProcessingStage;

namespace StringProcessor.Tests
{
    class ProcessingStageTests
    {
        NullLogger<CustomProcessingStage> nullLogger;
        Func<string, string> func;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            nullLogger = new NullLogger<CustomProcessingStage>();
            func = str => str.ToUpper();
        }

        [Test]
        public void CustomProcessingStage_Constructor_Test()
        {
            IProcessingStage processingStage1 = new CustomProcessingStage(func, nullLogger);
            IProcessingStage processingStage2 = new CustomProcessingStage("stage1", func, nullLogger);

            Assert.That(processingStage1.ProcessFunc == func && processingStage2.ProcessFunc == func);
        }

        [Test]
        public void CustomProcessingStage_ProcessFunc_IsReadOnly_Test()
        {
            IProcessingStage processingStage = new CustomProcessingStage(func, nullLogger);

            var prop = processingStage.GetType().GetProperty(nameof(processingStage.ProcessFunc));

            var attrib = Attribute.GetCustomAttribute(prop, typeof(ReadOnlyAttribute)) as ReadOnlyAttribute;
            bool isProcessFuncReadOnly = !prop.CanWrite || (attrib != null && attrib.IsReadOnly);

            Assert.That(isProcessFuncReadOnly);
        }
    }
}
