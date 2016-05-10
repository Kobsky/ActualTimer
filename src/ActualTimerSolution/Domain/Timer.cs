﻿using System;

namespace Kobsky.ActualTimer
{
	/// <summary>to do </summary>
	/// <summary xml:lang="ru">
	/// Таймер который использует <see cref="ActualTimerAppCore"/>
	/// </summary>
	internal sealed class Timer:IDisposable
	{
		/// <summary>to do </summary>
		/// <summary xml:lang="ru">
		/// Текущее значение таймера
		/// </summary>
		public TimeSpan Value { get; set; }

		/// <summary>to do </summary>
		/// <summary xml:lang="ru">
		/// Календарная дата, которую использует таймер
		/// </summary>
		public DateTime Date { get; set; }

		/// <summary>to do </summary>
		/// <summary xml:lang="ru">
		/// Возвращает TimerState с текущими занчениями таймера
		/// </summary>
		public TimerState State => new TimerState {Date = Date, Value = Value};

		/// <summary>to do </summary>
		/// <summary xml:lang="ru">
		/// Событие которое генерируется при каждом тике таймера
		/// </summary>
		public EventHandler OnTick;

		/// <summary>to do </summary>
		/// <summary xml:lang="ru">
		/// Событие которое генериуется при приросте таймера на минуту
		/// </summary>
		public EventHandler<Timer> OnMinuteTick;

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

		/// <summary>
		/// Конструктор принимающий TimerState для восстановления значений таймера
		/// </summary>
		/// <param name="state"></param>
		public Timer(TimerState state)
		{
			Value = state.Value;
			Date = state.Date;
		}

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
		public void Tick(int milliseconds)
		{
			_previous = Value;
			Value = Value.Add(new TimeSpan(0,0,0,0,milliseconds));

			OnTick?.Invoke(this, null);

			if(Value.Minutes > _previous.Minutes)
				OnMinuteTick?.Invoke(this,this);
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

		/// <summary>to do </summary>
		/// <summary xml:lang="ru">
		/// Так как класс за кулисами использует <see cref="System.Threading.Timer"/> то необходимо реализовать <c>Dispose</c>
		/// </summary>
		public void Dispose()
		{
			// ReSharper disable once UseNullPropagation
			if (_timer != null) _timer.Dispose();
		}
	}
}
