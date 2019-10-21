using System.Collections.ObjectModel;

namespace StringProcessor.Core.CustomCollections
{
    public interface IProcessorQueue<T>
    {
        int Limit { get; }

        void Enqueue(T data);

        T Peek();

        T Dequeue();

        ReadOnlyCollection<T> ToReadOnlyCollection();

        int Count { get; }

        bool Any();
    }
}
