using System;
using System.Collections.Generic;

namespace Kobsky.ActualTimer.Charts
{
	/// <summary>
	/// Алгоритм создания временной линейки, использующий год, как сепаратор
	/// </summary>
	internal sealed class YearStrategy: TimeLineStrategy
	{
		public override ChartEntry[] Algorithm(TimerState[] states)
		{
			int startYear = states.MinDate().Year;
			int stopYear = states.MaxDate().Year;

			List<ChartEntry> entries = new List<ChartEntry>();
			while (startYear <= stopYear)
			{
				entries.Add(new ChartEntry {From = new DateTime(startYear,1,1), To = new DateTime(startYear,12,31)});
				startYear ++;
			}

			return entries.ToArray();
		}
	}
}