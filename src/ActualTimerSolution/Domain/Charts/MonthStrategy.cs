using System;
using System.Collections.Generic;
using System.Globalization;

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
			Calendar calendar = new GregorianCalendar();

			DateTime startDate = states.MinDate();
			startDate = new DateTime(startDate.Year, startDate.Month, 1);

			DateTime endDate = states.MaxDate();
			int lastDayNumber = calendar.GetDayOfMonth(endDate);
			endDate = new DateTime(endDate.Year, endDate.Month, lastDayNumber);

			List<ChartEntry> entries = new List<ChartEntry>();
			while (startDate < endDate)
			{
				int dayInMonth = calendar.GetDaysInMonth(startDate.Year, startDate.Month);
				entries.Add(new ChartEntry {From = startDate, To = startDate.AddDays(dayInMonth -1)});
				startDate = startDate.AddMonths(1);
			}
			return entries.ToArray();
		}
	}
}