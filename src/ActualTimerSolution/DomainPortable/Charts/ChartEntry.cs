using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Kobsky.ActualTimer.Charts
{
	/// <summary>to do</summary>
	/// <summary xml:lang="ru">
	/// Единица граффика (например столбец) который может содержать несколько относящихся к нему TimerState за определенное время, между From и To
	/// </summary>
	public sealed class ChartEntry
	{
		/// <summary>to do</summary>
		/// <summary xml:lang="ru">
		/// Дата, указывающая на начало периода, с которого ChartEntry содержит относящиеся к нему TimerState
		/// </summary>
		public DateTime From { get; set; }

		/// <summary>to do</summary>
		/// <summary xml:lang="ru">
		/// Дата, указывающая на конец периода, по который ChartEntry содержит относящиеся к нему TimerState
		/// </summary>
		public DateTime To { get; set; }

		/// <summary>to do</summary>
		/// <summary xml:lang="ru">
		/// TimerState списко, относящийся к периоду дат с From по To
		/// </summary>
		public Collection<TimerState> States { get; } = new Collection<TimerState>();

		/// <summary>to do</summary>
		/// <summary xml:lang="ru">
		/// Расчитывает итоговое значение, используемое граффиком, которое предоставляется пользователю
		/// </summary>
		/// <param name="measure"></param>
		/// <returns></returns>
		public int CalculateValue(ChartMeasure measure)
		{
			TimeSpan total = new TimeSpan();
			total = States.Aggregate(total, (current, state) => current + state.Value);

			switch (measure)
			{
				case ChartMeasure.Minute:
				{
					return (int)total.TotalMinutes;
				}
				case ChartMeasure.Hour:
				{
					return (int) total.TotalHours;
				}
				default:
				{
					return 0;
				}
			}
		}
	}
}
