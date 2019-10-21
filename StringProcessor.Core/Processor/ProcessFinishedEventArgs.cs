using System;

namespace StringProcessor.Core.Processor
{
    public class ProcessFinishedEventArgs : EventArgs
    {
        public ProcessStatus ProcessStatus { get; private set; }

        public string Message { get; private set; }

        public string Filepath { get; private set; }

        public ProcessFinishedEventArgs(ProcessStatus processStatus, string filePath)
        {
            ProcessStatus = processStatus;
            Filepath = filePath;
        }

        public ProcessFinishedEventArgs(ProcessStatus processStatus, string filePath, string message) 
            : this(processStatus, filePath)
        {
            Message = message;
        }
    }
}
