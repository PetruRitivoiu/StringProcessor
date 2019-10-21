using StringProcessor.Core.CustomExceptions;
using StringProcessor.Core.ProcessingStage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace StringProcessor.Core.CustomCollections
{
	public class ProcessorQueue<T> : IProcessorQueue<T> where T : IProcessingStage
	{
        private readonly Queue<T> queue;

        public readonly int limit;

		public ProcessorQueue(int limit)
		{
			this.queue = new Queue<T>();
			this.limit = limit;
        }

        public int Limit => limit;

		public void Enqueue(T data)
		{
			if (queue.Count < limit)
			{
                queue.Enqueue(data);
			}
			else
			{
				throw new MaxSizeReachedException("Processor Queue can only store 5 elements");
			}
		}

        public T Peek() => queue.Peek();

        public T Dequeue() => queue.Dequeue();

        public ReadOnlyCollection<T> ToReadOnlyCollection() => Array.AsReadOnly(queue.ToArray());

        public int Count => queue.Count;

        public bool Any() => queue.Any();
	}
}
