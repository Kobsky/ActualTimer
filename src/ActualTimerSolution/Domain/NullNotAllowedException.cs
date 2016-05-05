using System;

namespace Kobsky.ActualTimer
{
	/// <summary> 
	/// to do
	/// </summary>
	/// <summary xml:lang="ru">
	/// Возбуждается при создании экземпляра ActualTimerAppCore если ITimerRepository.GetTodayOrDefault возвращает null значение
	/// </summary>
	public class NullReturnNotAllowedException : Exception
	{
		public NullReturnNotAllowedException(string message) : base(message)
		{}
	}
}