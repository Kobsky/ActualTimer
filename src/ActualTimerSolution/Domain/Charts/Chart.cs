namespace Kobsky.ActualTimer.Charts
{
	/// <summary>to do</summary>
	/// <summary xml:lang="ru">
	/// Обект граффика, у которого пользователи могут запрашивать данные
	/// </summary>
	public class Chart
	{
		private readonly TimerState[] _states;

		public Chart(TimerState[] states)
		{
			_states = states;
		}

		/// <summary>to do</summary>
		/// <summary xml:lang="ru">
		/// Возвращает массив <see cref="ChartEntry"/> который предоставляет всю необходимую информацию для посторойки граффика
		/// </summary>
		/// <param name="chartPeriod"></param>
		/// <returns></returns>
		public ChartEntry[] GetData(ChartPeriod chartPeriod)
		{
			return _states.ToCharEntryArray(chartPeriod);
		}
	}
}