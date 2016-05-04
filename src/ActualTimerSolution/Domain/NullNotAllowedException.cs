using System;

namespace Kobsky.ActualTimer
{
	/// <summary>
	/// Used when ITimerRepository.GetTodayOrDefault method return null
	/// </summary>
	public class NullReturnNotAllowedException : Exception
	{
		public NullReturnNotAllowedException(string message) : base(message)
		{}
	}
}