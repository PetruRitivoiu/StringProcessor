using System;

namespace StringProcessor.Core.ProcessingStage
{
    public interface IProcessingStage
    {
        string Process(string str);

        Func<string, string> ProcessFunc { get; }
    }
}
