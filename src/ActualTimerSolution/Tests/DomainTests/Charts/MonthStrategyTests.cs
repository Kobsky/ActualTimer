using System;
using Kobsky.ActualTimer;
using Kobsky.ActualTimer.Charts;
using NUnit.Framework;

namespace DomainTests.Charts
{
	[TestFixture]
	public class MonthStrategyTests
	{
		[Test]
		public void Ctor()
		{
			TimeLineStrategy strategy = new MonthStrategy();

			Assert.IsNotNull(strategy);
		}

		[Test]
		public void AlgorithmTest()
		{
			var states = new[]
			{
				new TimerState {Date = new DateTime(2015,12,17)},
				new TimerState {Date = new DateTime(2016,3,4)}
			};
			TimeLineStrategy strategy = new MonthStrategy();

			ChartEntry[] entries = strategy.Algorithm(states);

			Assert.AreEqual(4, entries.Length);
			Assert.AreEqual(new DateTime(2015,12,1), entries[0].From);
			Assert.AreEqual(new DateTime(2015, 12, 31), entries[0].To);

			Assert.AreEqual(new DateTime(2016, 2, 1), entries[2].From);
			Assert.AreEqual(new DateTime(2016, 2, 29), entries[2].To);
		}

	}
}