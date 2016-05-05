using System;
using System.Threading;
using Kobsky.ActualTimer;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using Timer = Kobsky.ActualTimer.Timer;

namespace DomainTests
{
	[TestFixture]
	public class ActualTimerAppCoreTests
	{

		[Test]
		public void Ctor()
		{
			var timer = new Timer();
			var timerRepository = Substitute.For<ITimerRepository>();
			timerRepository.LoadTodayOrDefault().Returns(timer);

			ActualTimerAppCore core = new ActualTimerAppCore(timerRepository);

			Assert.AreEqual(timerRepository, core.TimerRepository);
			Assert.AreEqual(200, core.TickMilliseconds);
			Assert.AreEqual(timer, core.Timer);
			Assert.AreEqual(new TimeSpan(), core.Value );
		}

		[Test]
		public void CtorWithNullTimerRepository()
		{
			ArgumentNullException ex = Assert.Throws<ArgumentNullException>(delegate
			{
				// ReSharper disable once ObjectCreationAsStatement
				new ActualTimerAppCore(null);
			});

			Assert.IsTrue(ex.Message.Contains("timerRepository"));
		}

		[Test]
		public void CtorGetTodayTimer()
		{
			var timerRepository = Substitute.For<ITimerRepository>();
			timerRepository.LoadTodayOrDefault()
				.Returns(new Timer {Value = new TimeSpan(50)});

			ActualTimerAppCore core = new ActualTimerAppCore(timerRepository);

			Assert.AreEqual(new TimeSpan(50), core.Value);
		}

		[Test]
		public void CtorGetTodayTimerReturnNull()
		{
			NullReturnNotAllowedException ex = Assert.Throws<NullReturnNotAllowedException>(delegate
			{
				var timerRepository = Substitute.For<ITimerRepository>();
				timerRepository.LoadTodayOrDefault()
					.ReturnsNull();

				// ReSharper disable once ObjectCreationAsStatement
				new ActualTimerAppCore(timerRepository);
			});

			Assert.IsTrue(ex.Message.Contains("ITimerRepository.GetTodayOrDefault method return null"));
		}

		[Test]
		public void StartTest()
		{
			var timerRepository = Substitute.For<ITimerRepository>();
			timerRepository.LoadTodayOrDefault().Returns(new Timer());
			ActualTimerAppCore core = new ActualTimerAppCore(timerRepository)
			{
				TickMilliseconds = 100
			};

			core.Start();
			Thread.Sleep(350);
			core.Stop();

			Assert.AreEqual(new TimeSpan(0,0,0,0,300), core.Value );
		}

		[Test]
		public void ChangeTickMilliseconds()
		{
			var timerRepository = Substitute.For<ITimerRepository>();
			timerRepository.LoadTodayOrDefault().Returns(new Timer());
			ActualTimerAppCore core = new ActualTimerAppCore(timerRepository)
			{
				TickMilliseconds = 100
			};

			core.Start();
			Thread.Sleep(350);
			core.Stop();

			core.TickMilliseconds = 200;
			core.Start();
			Thread.Sleep(350);
			core.Stop();

			Assert.AreEqual(new TimeSpan(0, 0, 0, 0, 500), core.Value);
		}

		[Test]
		public void OnTickTest()
		{
			int counter = 0;
			var timerRepository = Substitute.For<ITimerRepository>();
			timerRepository.LoadTodayOrDefault().Returns(new Timer());
			ActualTimerAppCore core = new ActualTimerAppCore(timerRepository)
			{
				TickMilliseconds = 100
			};
			core.OnTick += (sender, args) => { counter++; };

			core.Start();
			Thread.Sleep(350);
			core.Stop();

			Assert.AreEqual(3, counter);
		}


		[Test]
		public void OnStartStopTest()
		{
			bool? startStop = null;
			var timerRepository = Substitute.For<ITimerRepository>();
			timerRepository.LoadTodayOrDefault().Returns(new Timer());
			ActualTimerAppCore core = new ActualTimerAppCore(timerRepository)
			{
				TickMilliseconds = 100
			};

			core.OnStartStop += (sender, b) => { startStop = b; };
			core.Start();
			Assert.AreEqual(true, startStop);
			core.Stop();
			Assert.AreEqual(false,startStop);
		}

		[Test]
		public void SaveWhenMinuteChanges()
		{
			var timer = new Timer
			{
				Value = new TimeSpan(0, 0, 0, 59, 810)
			};
			var timerRepository = Substitute.For<ITimerRepository>();
			timerRepository.LoadTodayOrDefault().Returns(timer);
			ActualTimerAppCore core = new ActualTimerAppCore(timerRepository)
			{
				TickMilliseconds = 100
			};

			core.Start();
			Thread.Sleep(350);
			core.Stop();

			timerRepository.Received(1).SaveOrUpdate(timer);
		}
	}
}
