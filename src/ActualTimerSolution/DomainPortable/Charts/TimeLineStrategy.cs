namespace Kobsky.ActualTimer.Charts
{
	/// <summary>to do</summary>
	/// <summary xml:lang="ru">
	/// Представляет базовый класс, для алгоритмов, создания временной линейки
	/// </summary>
	internal abstract class TimeLineStrategy
	{
		public abstract ChartEntry[] Algorithm(TimerState[] states);
	}
}
