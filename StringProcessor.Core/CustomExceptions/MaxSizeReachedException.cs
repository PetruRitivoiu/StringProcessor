using System;

namespace StringProcessor.Core.CustomExceptions
{
	public class MaxSizeReachedException : Exception
	{
		public MaxSizeReachedException(string message) : base(message) { }
	}
}
