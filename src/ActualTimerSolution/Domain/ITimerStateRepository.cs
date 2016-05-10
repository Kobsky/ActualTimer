namespace Kobsky.ActualTimer
{
	/// <summary>to do</summary>
	/// <summary xml:lang="ru">Представляет репозиторий, для сохранения и загрузки данных о таймере</summary>
	public interface ITimerStateRepository
	{
		/// <summary>to do</summary>
		/// <summary xml:lang="ru">
		/// Загружает данные таймера, которые были сохранены сегодня, если сохраненных данных нету, то возвращается новый TimerState, у которого Date установленна сегодняшней датой 
		/// </summary>
		/// <returns><see cref="Timer"/></returns>
		TimerState LoadTodayOrDefault();

		/// <summary>to do</summary>
		/// <summary xml:lang="ru">
		/// Сохраняет таймер, если он был сохранен ранее, то обновляет его значение.
		/// </summary>
		/// <param name="timer"><see cref="Timer"/></param>
		void SaveOrUpdate(TimerState timer);
	}
}