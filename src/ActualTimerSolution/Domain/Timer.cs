using System;

namespace Kobsky.ActualTimer
{
	/// <summary>to do </summary>
	/// <summary xml:lang="ru">
	/// Таймер который использует <see cref="ActualTimerAppCore"/>
	/// </summary>
	public sealed class Timer
	{
		/// <summary>to do </summary>
		/// <summary xml:lang="ru">
		/// Событие которое генерируется при каждом тике таймера
		/// </summary>
		internal Action OnTick;

		/// <summary>to do </summary>
		/// <summary xml:lang="ru">
		/// Событие которое генериуется при приросте таймера на минуту
		/// </summary>
		internal Action<Timer> OnMinuteTick;

		/// <summary>to do </summary>
		/// <summary xml:lang="ru">
		/// Текущее значение таймера
		/// </summary>
		public TimeSpan Value;

		/// <summary>to do </summary>
		/// <summary xml:lang="ru">
		/// Значение таймера при предыдущем тике
		/// </summary>
		private TimeSpan _previous;

		/// <summary>to do </summary>
		/// <summary xml:lang="ru">
		/// <see cref="System.Threading.Timer"/> используемый <see cref="Timer"/> за кулисами, для работы
		/// </summary>
		private System.Threading.Timer _timer;

		/// <summary>to do </summary>
		/// <summary xml:lang="ru">
		/// Метод вызывается для изменения текущего значения таймера
		/// </summary>
		/// <param name="milliseconds">
		/// to do
		/// <para>
		/// <see cref="int"/> значение миллисекунд, на которое надо изменить текущее значение таймера
		/// </para>
		/// </param>
		internal void Tick(int milliseconds)
		{
			_previous = Value;
			Value = Value.Add(new TimeSpan(0,0,0,0,milliseconds));

			OnTick?.Invoke();

			if(Value.Minutes > _previous.Minutes)
				OnMinuteTick?.Invoke(this);
		}

		/// <summary>to do </summary>
		/// <summary xml:lang="ru">
		/// Запускает таймер на выполнение
		/// </summary>
		/// <param name="milliseconds">
		/// to do
		/// <para>
		/// <see cref="int"/> значение миллисекунд, указывающее интервал выполнения таймера
		/// </para>
		/// </param>
		public void Start(int milliseconds)
		{
			_timer = new System.Threading.Timer(state => { Tick(milliseconds); },null, milliseconds, milliseconds);
		}

		/// <summary>to do </summary>
		/// <summary xml:lang="ru">
		/// Ставит выполнение таймера на паузу
		/// </summary>
		public void Stop()
		{
			_timer?.Dispose();
		}
	}
}
