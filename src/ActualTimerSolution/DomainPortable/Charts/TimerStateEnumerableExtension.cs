using System;
using System.Collections.Generic;
using System.Linq;

namespace Kobsky.ActualTimer.Charts
{
	/// <summary>to do</summary>
	/// <summary xml:lang="ru">
	/// Содержим методы расширения для IEnumerable TimerState
	/// </summary>
	static class TimerStateEnumerableExtension
	{
		/// <summary>to do</summary>
		/// <summary xml:lang="ru">
		/// Возвращает минимальную дату, из всех TimerState
		/// </summary>
		/// <param name="tStates"></param>
		/// <returns></returns>
		public static DateTime MinDate(this IEnumerable<TimerState> tStates)
		{
			return tStates.Min(state => state.Date).Date;
		}

		/// <summary>to do</summary>
		/// <summary xml:lang="ru">
		/// Возвращает максимальную дату, из всех TimerState
		/// </summary>
		/// <param name="tStates"></param>
		/// <returns></returns>
		public static DateTime MaxDate(this IEnumerable<TimerState> tStates)
		{
			return tStates.Max(state => state.Date).Date;
		}

		/// <summary>to do</summary>
		/// <summary xml:lang="ru">
		/// Создает новый массив <see cref="ChartEntry"/> в соответсвии со значениями указанными в агрументах и распределяем между ними <see cref="TimerState"/>
		/// </summary>
		/// <param name="timerStates"></param>
		/// <param name="chartPeriod"></param>
		/// <returns></returns>
		public static ChartEntry[] ToCharEntryArray(this IEnumerable<TimerState> timerStates, ChartPeriod chartPeriod)
		{
			TimerState[] states = timerStates.ToArray();
			var timeLine = states.CreateTimeLine(chartPeriod);
			return states.Allocate(timeLine);
		}

		/// <summary>to do</summary>
		/// <summary xml:lang="ru">
		/// Создает новый массив <see cref="ChartEntry"/> в соответсвии со значениями указанными в агрументах
		/// </summary>
		/// <param name="timerStates"></param>
		/// <param name="chartPeriod">
		/// to do
		/// <para>
		/// Указывает на длинну каждого периуда
		/// </para>
		/// </param>
		/// <returns></returns>
		public static ChartEntry[] CreateTimeLine(this IEnumerable<TimerState> timerStates, ChartPeriod chartPeriod)
		{
			switch (chartPeriod)
			{
				case ChartPeriod.Day:
				{
					return new DayStrategy().Algorithm(timerStates.ToArray());
				}
				case ChartPeriod.Week:
				{
					return new WeekStrategy().Algorithm(timerStates.ToArray());
				}
				case ChartPeriod.Month:
				{
					return new MonthStrategy().Algorithm(timerStates.ToArray());
				}
				case ChartPeriod.Year:
				{
					return new YearStrategy().Algorithm(timerStates.ToArray());
				}
				default:
				{
					return new ChartEntry[0];
				}
			}
		}

		/// <summary>
		/// Распределяет текущие <see cref="TimerState"/> по переданной в метод временной линейке
		/// </summary>
		/// <param name="timerStates"></param>
		/// <param name="entries"></param>
		/// <returns></returns>
		public static ChartEntry[] Allocate(this IEnumerable<TimerState> timerStates, ChartEntry[] entries)
		{
			var timerStatesArray =  timerStates.ToArray();
			foreach (ChartEntry entry in entries)
			{
				var stateToAllocate = timerStatesArray.Where(state => state.Date <= entry.From && state.Date >= entry.To);
				foreach (var timerState in stateToAllocate)
				{
					entry.States.Add(timerState);
				}
			}
			return entries;
		}
	}
}
