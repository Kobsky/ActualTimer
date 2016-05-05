using System;
using System.Threading;
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
			Timer timer = new Timer
			{
				Value = new TimeSpan(100)
			};

			Assert.AreEqual(new TimeSpan(100), timer.Value );
		}

		[Test]
		public void TickTest()
		{
			Timer timer = new Timer();
			timer.OnTick += Coutn;
			timer.Tick(100);

			Assert.AreEqual(1, _counter);
			Assert.AreEqual(new TimeSpan(0,0,0,0,100), timer.Value );
		}
		private void Coutn()
		{
			_counter++;
		}

		[Test]
		public void OnMinuteTests()
		{
			Timer timer = new Timer
			{
				Value = new TimeSpan(0, 0, 0, 59, 0)
			};
			timer.OnMinuteTick += OnMinuteTick;

			timer.Tick(1000);
			timer.Tick(1000);
			timer.Tick(1000);

			Assert.AreEqual(1, _counter);
			Assert.AreEqual(new TimeSpan(0, 0, 1, 2, 0), timer.Value);
		}

		private void OnMinuteTick(Timer timer)
		{
			Coutn();
		}

		[Test]
		public void StartTest()
		{
			Timer timer = new Timer();
			timer.OnTick += Coutn;

			timer.Start(10);
			Thread.Sleep(150);
			// ReSharper disable once DelegateSubtraction
			timer.OnTick -= Coutn;

			Assert.IsTrue(3 < _counter);
		}

		[Test]
		public void StopTest()
		{
			Timer timer = new Timer();
			timer.OnTick += Coutn;

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
			Timer timer = new Timer();

			Assert.DoesNotThrow(() => { timer.Stop(); });
		}
	}
}
