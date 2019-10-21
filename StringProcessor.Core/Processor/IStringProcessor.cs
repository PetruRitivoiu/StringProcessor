namespace StringProcessor.Core.Processor
{
    public delegate void ProcessFinishedEventHandler(object sender, ProcessFinishedEventArgs eventArgs);
	public interface IStringProcessor
	{
		void StartProcessing();

        event ProcessFinishedEventHandler ProcessingFinished;
	}
}
