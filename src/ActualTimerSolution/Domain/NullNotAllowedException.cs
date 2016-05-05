using System;
using System.Runtime.Serialization;

namespace Kobsky.ActualTimer
{
	/// <summary> 
	/// to do
	/// </summary>
	/// <summary xml:lang="ru">
	/// Возбуждается при создании экземпляра ActualTimerAppCore если ITimerRepository.GetTodayOrDefault возвращает null значение
	/// </summary>
	[Serializable]
	public class NullReturnNotAllowedException : Exception
	{
		public NullReturnNotAllowedException()
		{}

		public NullReturnNotAllowedException(string message) : base(message)
		{}

		public NullReturnNotAllowedException(string message, Exception ex): base(message, ex)
		{ }

		protected NullReturnNotAllowedException(SerializationInfo info, StreamingContext context): base(info, context)
		{ }
	}
}