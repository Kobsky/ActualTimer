using System;
using System.Threading;
using Kobsky.ActualTimer;
using NUnit.Framework;
using Timer = Kobsky.ActualTimer.Timer;

namespace DomainTests
{
	[TestFixture]
	public class TimerTests
	{
		private int _counter;

		[SetUp]
		public void Init()
		{
			_counter = 0;
		}

		[Test]
		public void Ctor()
		{
			TimerState state = new TimerState();
			state.Date = DateTime.Today;
			state.Value = new TimeSpan(100);

			Timer timer = new Timer(state);

			Assert.AreEqual(new TimeSpan(100), timer.Value );
			Assert.AreEqual(DateTime.Today, timer.Date);
		}

		[Test]
		public void TickTest()
		{
			Timer timer = new Timer(new TimerState());
			timer.OnTick += (sender, args) => { _counter++; };
			timer.Tick(100);

			Assert.AreEqual(1, _counter);
			Assert.AreEqual(new TimeSpan(0,0,0,0,100), timer.Value );
		}

		[Test]
		public void OnMinuteTests()
		{
			Timer timer = new Timer(new TimerState {Value = new TimeSpan(0, 0, 0, 59, 0)});
			timer.OnMinuteTick += (sender, timer1) => { _counter++; };

			timer.Tick(1000);
			timer.Tick(1000);
			timer.Tick(1000);

			Assert.AreEqual(1, _counter);
			Assert.AreEqual(new TimeSpan(0, 0, 1, 2, 0), timer.Value);
		}

		[Test]
		public void StartTest()
		{
			Timer timer = new Timer(new TimerState());
			timer.OnTick += Coutn;

			timer.Start(10);
			Thread.Sleep(150);
			// ReSharper disable once DelegateSubtraction
			timer.OnTick -= Coutn;

			Assert.IsTrue(3 < _counter);
		}
		private void Coutn(object sender, EventArgs eventArgs)
		{
			_counter++;
		}

		[Test]
		public void StopTest()
		{
			Timer timer = new Timer(new TimerState());
			timer.OnTick += (sender, args) => { _counter++; };

			timer.Start(10);
			Thread.Sleep(100);
			timer.Stop();
			int ticks = _counter;
			Thread.Sleep(100);

			Assert.AreEqual(_counter, ticks);
		}

		[Test]
		public void StopBeforeStart()
		{
			Timer timer = new Timer(new TimerState());

			Assert.DoesNotThrow(() => { timer.Stop(); });
		}

		[Test]
		public void DisposableTest()
		{
			IDisposable timer = new Timer(new TimerState());

			timer.Dispose();

			Assert.IsNotNull(timer);
		}

		[Test]
		public void StateProperty()
		{
			Timer timer = new Timer(new TimerState
			{
				Date = DateTime.MaxValue,
				Value = TimeSpan.MaxValue
			});

			TimerState state = timer.State;

			Assert.AreEqual(DateTime.MaxValue, state.Date);
			Assert.AreEqual(TimeSpan.MaxValue, state.Value);
		}
	}
}
