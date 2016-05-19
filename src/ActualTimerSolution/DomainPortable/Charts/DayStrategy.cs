using System.Collections.Generic;

namespace Kobsky.ActualTimer.Charts
{
	/// <summary>to do</summary>
	/// <summary xml:lang="ru">
	/// Алгоритм создания временной линейки, использующий день, как сепаратор
	/// </summary>
	internal sealed class DayStrategy:TimeLineStrategy
	{
		public override ChartEntry[] Algorithm(TimerState[] states)
		{
			var entries = new List<ChartEntry>();
			var delta = states.MaxDate() - states.MinDate();

			var currentDate = states.MinDate();
			for (int day = 0; day <= delta.Days; day++)
			{
				entries.Add(new ChartEntry {From = currentDate, To = currentDate});
				currentDate = currentDate.AddDays(1);
			}

			return entries.ToArray();
		}
	}
}
