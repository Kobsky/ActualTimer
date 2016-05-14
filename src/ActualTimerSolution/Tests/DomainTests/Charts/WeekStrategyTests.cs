using System;
using Kobsky.ActualTimer;
using Kobsky.ActualTimer.Charts;
using NUnit.Framework;

namespace DomainTests.Charts
{
	[TestFixture]
	public class WeekStrategyTests
	{
		[Test]
		public void Ctor()
		{
			TimeLineStrategy weekStrategy = new WeekStrategy();

			Assert.IsNotNull(weekStrategy);
		}

		[Test]
		public void AlgorithmTest()
		{
			var states = new[]
			{
				new TimerState {Date = new DateTime(2016,5,12)},
				new TimerState {Date = new DateTime(2016,6,2)}
			};
			TimeLineStrategy weekStrategy = new WeekStrategy();

			ChartEntry[] entries = weekStrategy.Algorithm(states);

			Assert.AreEqual(4,entries.Length);
			Assert.AreEqual(new DateTime(2016,5,9), entries[0].From );
			Assert.AreEqual(new DateTime(2016, 5, 15), entries[0].To);

			Assert.AreEqual(new DateTime(2016, 5, 23), entries[2].From);
			Assert.AreEqual(new DateTime(2016, 5, 29), entries[2].To);
		}
	}
}