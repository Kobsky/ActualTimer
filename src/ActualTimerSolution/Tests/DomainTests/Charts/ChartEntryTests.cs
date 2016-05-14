using System;
using Kobsky.ActualTimer;
using Kobsky.ActualTimer.Charts;
using NUnit.Framework;

namespace DomainTests.Charts
{
	[TestFixture]
	class ChartEntryTests
	{
		[Test]
		public void Ctor()
		{
			ChartEntry entry = new ChartEntry();


			Assert.AreEqual(new DateTime(), entry.From );
			Assert.AreEqual(new DateTime(), entry.To );
			Assert.AreEqual(0, entry.States.Count);
		}

		[Test]
		public void CalculateValueEmptyTest()
		{
			ChartEntry entry = new ChartEntry();

			Assert.AreEqual(0, entry.CalculateValue(ChartMeasure.Hour));
		}

		[Test]
		public void CalculateValueMinuteTest()
		{
			ChartEntry entry = new ChartEntry();
			entry.States.Add(new TimerState {Value = new TimeSpan(0, 1, 10, 5)});
			entry.States.Add(new TimerState {Value = new TimeSpan(0, 0, 5, 40)});

			int value = entry.CalculateValue(ChartMeasure.Minute);

			Assert.AreEqual(75, value);
		}

		[Test]
		public void CalculateValueHourTest()
		{
			ChartEntry entry = new ChartEntry();
			entry.States.Add(new TimerState {Value = new TimeSpan(0, 1, 10, 5)});
			entry.States.Add(new TimerState {Value = new TimeSpan(0, 3, 55, 40)});

			int value = entry.CalculateValue(ChartMeasure.Hour);

			Assert.AreEqual(5, value);
		}
	}
}
