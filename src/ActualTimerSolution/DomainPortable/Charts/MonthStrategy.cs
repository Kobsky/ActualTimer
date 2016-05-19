using System;
using System.Collections.Generic;

namespace Kobsky.ActualTimer.Charts
{
	/// <summary>to do</summary>
	/// <summary xml:lang="ru">
	/// Алгоритм создания временной линейки, использующий месяц, как сепаратор
	/// </summary>
	internal sealed class MonthStrategy: TimeLineStrategy
	{
		public override ChartEntry[] Algorithm(TimerState[] states)
		{
			DateTime startDate = states.MinDate();
			startDate = new DateTime(startDate.Year, startDate.Month, 1);

			DateTime endDate = GetLastDayOfMonth(states.MaxDate());

			List<ChartEntry> entries = new List<ChartEntry>();
			while (startDate < endDate)
			{
				entries.Add(new ChartEntry {From = startDate, To = GetLastDayOfMonth(startDate)});
				startDate = startDate.AddMonths(1);
			}
			return entries.ToArray();
		}

		private static DateTime GetLastDayOfMonth(DateTime date)
		{
			DateTime result = date;
			int month = result.Month;
			while (month == result.Month)
			{
				result = result.AddDays(1);
			}
			return result.AddDays(-1);
		}
	}
}