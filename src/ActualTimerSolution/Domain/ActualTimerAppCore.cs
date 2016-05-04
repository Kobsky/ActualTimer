﻿using System;

namespace Kobsky.ActualTimer
{
	public sealed class ActualTimerAppCore
	{
		public Timer Timer { get; }
		public ITimerRepository TimerRepository { get; }

		public TimeSpan Value => Timer.Value;

		public ActualTimerAppCore(ITimerRepository timerRepository)
		{
			if(null == timerRepository)
				throw new ArgumentNullException(nameof(timerRepository));

			TimerRepository = timerRepository;

			var tempTimer = timerRepository.GetTodayOrDefault();
			if (null == tempTimer) throw new NullReturnNotAllowedException("ITimerRepository.GetTodayOrDefault method return null");
			Timer = tempTimer;

		}
	}
}