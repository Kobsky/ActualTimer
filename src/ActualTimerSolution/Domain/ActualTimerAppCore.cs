using System;
using Kobsky.ActualTimer.Charts;

namespace Kobsky.ActualTimer
{
	/// <summary>to do</summary>
	/// <summary xml:lang="ru">
	/// Представляет собой фасад для библиотеки, все что нужно пользователю это реализовать в своем коде <see cref="ITimerStateRepository"/> и создать экземпляр данного класса
	/// </summary>
	public sealed class ActualTimerAppCore:IDisposable
	{
		private readonly Timer _timer;
		private readonly ITimerStateRepository _timerRepository;

		/// <summary>to do</summary>
		/// <summary xml:lang="ru">
		/// Время в миллисекундах, определяющие интервал срабатывания таймера
		/// </summary>
		public int TickMilliseconds
		{
			get { return _timer.TickMilliseconds; }
			set { _timer.TickMilliseconds = value; }
		}

		/// <summary>to do</summary>
		/// <summary xml:lang="ru">
		/// Текущее значение таймера
		/// </summary>
		public TimeSpan Value => _timer.Value;

		/// <summary>to do</summary>
		/// <summary xml:lang="ru">
		/// Предоставляет объект граффика
		/// </summary>
		public Chart Chart => new Chart(_timerRepository.LoadAll());

		/// <summary>to do</summary>
		/// <summary xml:lang="ru">
		/// Событие генерирущееся при каждом изменении таймера
		/// </summary>
		public event EventHandler OnTick;

		/// <summary>to do</summary>
		/// <summary xml:lang="ru">
		/// Событие генерирущиееся при каждом запуске и остановке таймера
		/// </summary>
		public event EventHandler<bool> OnStartStop;

		/// <summary>Ctor</summary>
		/// <summary xml:lang="ru">
		/// Конструктор
		/// </summary>
		/// <param name="timerRepository">
		/// to do
		/// <para>Экземпляр <see cref="ITimerStateRepository"/> который должен предоставить пользователь, не может быть <c>null</c></para>
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// to do
		/// <para>Возникает если в аргумент <paramref name="timerRepository"/> был передан <c>null</c></para>
		/// </exception>
		public ActualTimerAppCore(ITimerStateRepository timerRepository)
		{
			if(null == timerRepository)
				throw new ArgumentNullException(nameof(timerRepository));

			_timerRepository = timerRepository;

			_timer = new Timer(_timerRepository.LoadTodayOrDefault());

		}

		/// <summary>to do </summary>
		/// <summary xml:lang="ru">
		/// Запускает таймер
		/// </summary>
		public void Start()
		{
			if (OnTick != null) _timer.OnTick += OnTick;
			_timer.OnMinuteTick += SaveTimer;
			_timer.Start();
			OnStartStop?.Invoke(this,true);
		}

		/// <summary>
		/// Сохранияет значение таймера в постоянную память
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="timer"></param>
		private void SaveTimer(object sender, Timer timer)
		{
			TimerState state = timer.State;
			_timerRepository.SaveOrUpdate(state);
		}

		/// <summary>to do </summary>
		/// <summary xml:lang="ru">
		/// Ставит таймер на паузу
		/// </summary>
		public void Stop()
		{
			_timer.Stop();
			OnStartStop?.Invoke(this,false);
		}

		/// <summary>to do </summary>
		/// <summary xml:lang="ru">
		/// Освобождает внутренние ресурсы
		/// </summary>
		public void Dispose()
		{
			_timer.Dispose();
		}
	}
}
