using System;
using Kobsky.ActualTimer;
using Kobsky.ActualTimer.Charts;
using NUnit.Framework;

namespace DomainTests.Charts
{
	[TestFixture]
	public class YearStrategyTests
	{
		[Test]
		public void Ctor()
		{
			TimeLineStrategy strategy = new YearStrategy();

			Assert.IsNotNull(strategy);
		}

		[Test]
		public void AlgorithmTest()
		{
			var states = new[]
			{
				new TimerState {Date = new DateTime(2015, 12, 17)},
				new TimerState {Date = new DateTime(2016, 3, 4)}
			};
			TimeLineStrategy strategy = new YearStrategy();

			ChartEntry[] entries = strategy.Algorithm(states);

			Assert.AreEqual(2, entries.Length);
			Assert.AreEqual(new DateTime(2015, 1, 1), entries[0].From);
			Assert.AreEqual(new DateTime(2015, 12, 31), entries[0].To);

			Assert.AreEqual(new DateTime(2016, 1, 1), entries[1].From);
			Assert.AreEqual(new DateTime(2016, 12, 31), entries[1].To);

		}
	}
}