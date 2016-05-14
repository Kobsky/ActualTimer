using System;
using Kobsky.ActualTimer;
using Kobsky.ActualTimer.Charts;
using NUnit.Framework;

namespace DomainTests.Charts
{
	[TestFixture]
	public class ChartTests
	{
		[Test]
		public void Ctor()
		{
			Chart chart = new Chart(new TimerState[0]);

			Assert.IsNotNull(chart);
		}

		[Test]
		public void GetDataTest()
		{
			var states = new[]
			{
				new TimerState {Date = DateTime.Today, Value = new TimeSpan(0,1,0,0)},
				new TimerState {Date = DateTime.Today.AddDays(2), Value = new TimeSpan(0,0,12,0)}
			};

			Chart chart = new Chart(states);

			ChartEntry[] entries = chart.GetData(ChartPeriod.Day);

			Assert.AreEqual(3, entries.Length);
			Assert.AreEqual(1,entries[0].States.Count);
			Assert.AreEqual(0, entries[1].States.Count);
			Assert.AreEqual(1, entries[2].States.Count);
		}
	}
}