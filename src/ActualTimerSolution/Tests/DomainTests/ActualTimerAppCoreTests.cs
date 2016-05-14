using System;
using System.Threading;
using Kobsky.ActualTimer;
using Kobsky.ActualTimer.Charts;
using NSubstitute;
using NUnit.Framework;

namespace DomainTests
{
	[TestFixture]
	public class ActualTimerAppCoreTests
	{

		[Test]
		public void Ctor()
		{
			var state = new TimerState
			{
				Value = TimeSpan.MaxValue
			};
			var timerRepository = Substitute.For<ITimerStateRepository>();
			timerRepository.LoadTodayOrDefault().Returns(state);

			ActualTimerAppCore core = new ActualTimerAppCore(timerRepository);

			Assert.AreEqual(200, core.TickMilliseconds);
			Assert.AreEqual(TimeSpan.MaxValue, core.Value);
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
		public void StartTest()
		{
			var timerRepository = Substitute.For<ITimerStateRepository>();
			timerRepository.LoadTodayOrDefault().Returns(new TimerState());
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
			var timerRepository = Substitute.For<ITimerStateRepository>();
			timerRepository.LoadTodayOrDefault().Returns(new TimerState());
			ActualTimerAppCore core = new ActualTimerAppCore(timerRepository)
			{
				TickMilliseconds = 100
			};

			core.Start();
			Thread.Sleep(350);
			core.Stop();
			Thread.Sleep(350);

			core.TickMilliseconds = 200;
			core.Start();
			Thread.Sleep(350);
			core.Stop();
			Thread.Sleep(350);

			Assert.AreEqual(new TimeSpan(0, 0, 0, 0, 800), core.Value);
		}

		[Test]
		public void OnTickTest()
		{
			int counter = 0;
			var timerRepository = Substitute.For<ITimerStateRepository>();
			timerRepository.LoadTodayOrDefault().Returns(new TimerState());
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
			var timerRepository = Substitute.For<ITimerStateRepository>();
			timerRepository.LoadTodayOrDefault().Returns(new TimerState());
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
			var state = new TimerState
			{
				Value = new TimeSpan(0, 0, 0, 59, 810)
			};
			var timerRepository = Substitute.For<ITimerStateRepository>();
			timerRepository.LoadTodayOrDefault().Returns(state);
			ActualTimerAppCore core = new ActualTimerAppCore(timerRepository)
			{
				TickMilliseconds = 100
			};

			core.Start();
			Thread.Sleep(350);
			core.Stop();

			timerRepository.Received(1).SaveOrUpdate(Arg.Any<TimerState>());
		}

		[Test]
		public void ChartTest()
		{
			var timerRepository = Substitute.For<ITimerStateRepository>();
			ActualTimerAppCore core = new ActualTimerAppCore(timerRepository);

			Chart chart = core.Chart;

			Assert.IsNotNull(chart);
			timerRepository.Received().LoadAll();
		}

		[Test]
		public void DisposableTest()
		{
			var timerRepository = Substitute.For<ITimerStateRepository>();
			timerRepository.LoadTodayOrDefault().Returns(new TimerState());

			IDisposable core = new ActualTimerAppCore(timerRepository);

			Assert.DoesNotThrow(()=> {core.Dispose();});
		}
	}
}
