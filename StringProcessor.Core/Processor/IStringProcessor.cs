using System.ComponentModel;

namespace StringProcessor.Core.Processor
{
    public delegate void ProcessFinishedEventHandler(object sender, ProcessFinishedEventArgs eventArgs);
	public interface IStringProcessor
	{
		void StartProcessing();

        ISynchronizeInvoke SynchronizingObject { get; set; }

        event ProcessFinishedEventHandler ProcessingFinished;
	}
}
