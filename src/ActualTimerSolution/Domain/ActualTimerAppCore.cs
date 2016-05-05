using System;

namespace Kobsky.ActualTimer
{
	/// <summary>to do</summary>
	/// <summary xml:lang="ru">
	/// Представляет собой фасад для библиотеки, все что нужно пользователю это реализовать в своем коде <see cref="ITimerRepository"/> и создать экземпляр данного класса
	/// </summary>
	public sealed class ActualTimerAppCore
	{
		internal Timer Timer { get; set; }
		internal ITimerRepository TimerRepository { get; set; }

		/// <summary>to do</summary>
		/// <summary xml:lang="ru">
		/// Время в миллисекундах, определяющие интервал срабатывания таймера
		/// </summary>
		public int TickMilliseconds { get; set; } = 200;

		/// <summary>to do</summary>
		/// <summary xml:lang="ru">
		/// Текущее значение таймера
		/// </summary>
		public TimeSpan Value => Timer.Value;

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
		/// <para>Экземпляр <see cref="ITimerRepository"/> который должен предоставить пользователь, не может быть <c>null</c></para>
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// to do
		/// <para>Возникает если в аргумент <paramref name="timerRepository"/> был передан <c>null</c></para>
		/// </exception>
		/// <exception cref="NullReturnNotAllowedException">
		/// to do
		/// <para>Возникает если <c>timerRepository.GetTodayOrDefault()</c> возвращает <c>null</c></para>
		/// </exception>
		public ActualTimerAppCore(ITimerRepository timerRepository)
		{
			if(null == timerRepository)
				throw new ArgumentNullException(nameof(timerRepository));

			TimerRepository = timerRepository;

			var tempTimer = timerRepository.LoadTodayOrDefault();
			if (null == tempTimer)
				throw new NullReturnNotAllowedException("ITimerRepository.GetTodayOrDefault method return null");
			Timer = tempTimer;

		}

		/// <summary>to do </summary>
		/// <summary xml:lang="ru">
		/// Запускает таймер
		/// </summary>
		public void Start()
		{
			if (OnTick != null) Timer.OnTick += OnTick;
			Timer.OnMinuteTick += (sender, timer) => { TimerRepository.SaveOrUpdate(timer); };
			Timer.Start(TickMilliseconds);
			OnStartStop?.Invoke(this,true);
		}

		/// <summary>to do </summary>
		/// <summary xml:lang="ru">
		/// Ставит таймер на паузу
		/// </summary>
		public void Stop()
		{
			Timer.Stop();
			OnStartStop?.Invoke(this,false);
		}
	}
}
