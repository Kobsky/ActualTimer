using System;
using System.Linq;
using Kobsky.ActualTimer;
using Kobsky.ActualTimer.Charts;
using NUnit.Framework;

namespace DomainTests.Charts
{
	// ReSharper disable InvokeAsExtensionMethod
	[TestFixture]
	public class TimerStateEnumerableExtensionTests
	{
		[Test]
		public void MinDateTest()
		{
			var states = new[]
			{
				new TimerState {Date = DateTime.Today},
				new TimerState {Date = DateTime.Today.AddDays(10)}
			};

			DateTime minDate = TimerStateEnumerableExtension.MinDate(states.AsEnumerable());

			Assert.AreEqual(DateTime.Today, minDate);
		}

		[Test]
		public void MaxDateTest()
		{
			var states = new[]
			{
				new TimerState {Date = DateTime.Today},
				new TimerState {Date = DateTime.Today.AddDays(10)}
			};

			DateTime maxDate = TimerStateEnumerableExtension.MaxDate(states.AsEnumerable());

			Assert.AreEqual(DateTime.Today.AddDays(10), maxDate);
		}

		[Test]
		public void CreateTimeLineDay()
		{
			var states = new[]
			{
				new TimerState {Date = DateTime.Today},
				new TimerState {Date = DateTime.Today.AddDays(5)}
			};

			ChartEntry[] entries = TimerStateEnumerableExtension.CreateTimeLine(states.AsEnumerable(), ChartPeriod.Day);

			Assert.AreEqual(6,entries.Length);

		}

		[Test]
		public void CreateTimeLineWeek()
		{
			var states = new[]
			{
				new TimerState {Date = new DateTime(2016,5,5)},
				new TimerState {Date = new DateTime(2016,5,19)}
			};

			ChartEntry[] entries = TimerStateEnumerableExtension.CreateTimeLine(states.AsEnumerable(), ChartPeriod.Week);

			Assert.AreEqual(3, entries.Length);

		}

		[Test]
		public void CreateTimeLineMonth()
		{
			var states = new[]
			{
				new TimerState {Date = new DateTime(2014,1,1)},
				new TimerState {Date = new DateTime(2016,1,1)}
			};

			ChartEntry[] entries = TimerStateEnumerableExtension.CreateTimeLine(states.AsEnumerable(), ChartPeriod.Month);

			Assert.AreEqual(25, entries.Length);

		}

		[Test]
		public void CreateTimeLineYear()
		{
			var states = new[]
			{
				new TimerState {Date = new DateTime(2014,1,1)},
				new TimerState {Date = new DateTime(2016,1,1)}
			};

			ChartEntry[] entries = TimerStateEnumerableExtension.CreateTimeLine(states.AsEnumerable(), ChartPeriod.Year);

			Assert.AreEqual(3, entries.Length);

		}

		[Test]
		public void AllocateTest()
		{
			var states = new[]
			{
				new TimerState {Date = DateTime.Today},
				new TimerState {Date = DateTime.Today.AddDays(1)}
			};

			ChartEntry[] entries = new ChartEntry[]
			{
				new ChartEntry
				{
					From = DateTime.Today.AddDays(-1),
					To = DateTime.Today.AddDays(-1)
				},
				new ChartEntry
				{
					From = DateTime.Today,
					To = DateTime.Today
				},
				new ChartEntry
				{
					From = DateTime.Today.AddDays(1),
					To = DateTime.Today.AddDays(1)
				}
			};

			entries = TimerStateEnumerableExtension.Allocate(states.AsEnumerable(), entries);

			Assert.AreEqual(0,entries[0].States.Count);
			Assert.AreEqual(1, entries[1].States.Count);
			Assert.AreEqual(1, entries[2].States.Count);
		}
	}
}
