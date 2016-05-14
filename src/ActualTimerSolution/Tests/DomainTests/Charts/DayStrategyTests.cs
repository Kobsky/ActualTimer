using System;
using Kobsky.ActualTimer;
using Kobsky.ActualTimer.Charts;
using NUnit.Framework;

namespace DomainTests.Charts
{
	[TestFixture]
	public class DayStrategyTests
	{
		[Test]
		public void Ctor()
		{
			TimeLineStrategy strategy = new DayStrategy();

			Assert.IsNotNull(strategy);
		}

		[Test]
		public void AlgorithmTest()
		{
			var states = new[]
			{
				new TimerState {Date = DateTime.Today},
				new TimerState {Date = DateTime.Today.AddDays(5)}
			};
			TimeLineStrategy strategy = new DayStrategy();

			ChartEntry[] entries = strategy.Algorithm(states);

			Assert.AreEqual(6,entries.Length);
			Assert.AreEqual(DateTime.Today,entries[0].From);
			Assert.AreEqual(DateTime.Today, entries[0].To);

			Assert.AreEqual(DateTime.Today.AddDays(3), entries[3].From);
			Assert.AreEqual(DateTime.Today.AddDays(3), entries[3].To);
		}
	}
}
