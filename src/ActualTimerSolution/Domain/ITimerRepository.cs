namespace Kobsky.ActualTimer
{
	/// <summary>to do</summary>
	/// <summary xml:lang="ru">Представляет репозиторий, для сохранения и загрузки данных о таймере</summary>
	public interface ITimerRepository
	{
		/// <summary>to do</summary>
		/// <summary xml:lang="ru">
		/// Загружает таймер, который был сохранен сегодня, если сохраненных данных нету, то создает новый таймер
		/// </summary>
		/// <returns><see cref="Timer"/></returns>
		Timer GetTodayOrDefault();

		/// <summary>to do</summary>
		/// <summary xml:lang="ru">
		/// Сохраняет таймер, если он был сохранен ранее, то обновляет его значение.
		/// </summary>
		/// <param name="timer"><see cref="Timer"/></param>
		void SaveOrUpdate(Timer timer);
	}
}