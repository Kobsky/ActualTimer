using System;
using System.Collections.Generic;

namespace Kobsky.ActualTimer.Charts
{
	/// <summary>to do</summary>
	/// <summary xml:lang="ru">
	/// Алгоритм создания временной линейки, использующий неделю, как сепаратор
	/// </summary>
	internal sealed class WeekStrategy:TimeLineStrategy
	{
		public override ChartEntry[] Algorithm(TimerState[] states)
		{
			DateTime startMonday = states.MinDate();
			while (startMonday.DayOfWeek != DayOfWeek.Monday)
			{
				startMonday = startMonday.AddDays(-1);
			}

			DateTime endSunday = states.MaxDate();
			while (endSunday.DayOfWeek != DayOfWeek.Sunday)
			{
				endSunday = endSunday.AddDays(1);
			}

			int weeks = ((int)(endSunday - startMonday).TotalDays + 1) / 7;

			List<ChartEntry> entries = new List<ChartEntry>();
			for (int i = 0; i < weeks; i++)
			{
				entries.Add(new ChartEntry {From = startMonday, To = startMonday.AddDays(6)});
				startMonday = startMonday.AddDays(7);
			}

			return entries.ToArray();
		}
	}
}