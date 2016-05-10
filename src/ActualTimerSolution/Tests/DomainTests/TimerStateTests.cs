using System;
using Kobsky.ActualTimer;
using NUnit.Framework;

namespace DomainTests
{
	[TestFixture]
	public class TimerStateTests
	{
		[Test]
		public void Ctor()
		{
			TimerState state = new TimerState();

			Assert.AreEqual(new TimeSpan(), state.Value);
			Assert.AreEqual(new DateTime(), state.Date );
		}

		[Test]
		public void EqualsTests()
		{
			TimerState one = new TimerState();
			TimerState two = new TimerState();

			Assert.AreEqual(one, two);
			Assert.IsTrue(one==two);
			Assert.IsFalse(one!=two);
			Assert.AreEqual(one.GetHashCode(), two.GetHashCode());

			two.Value = TimeSpan.MaxValue;

			Assert.AreNotEqual(one, two);
			Assert.IsFalse(one == two);
			Assert.IsTrue(one != two);
			Assert.AreNotEqual(one.GetHashCode(), two.GetHashCode());

			one.Value = TimeSpan.MaxValue;

			Assert.AreEqual(one,two);
			Assert.IsTrue(one == two);
			Assert.IsFalse(one != two);
			Assert.AreEqual(one.GetHashCode(), two.GetHashCode());

			two.Date = DateTime.Today;

			Assert.AreNotEqual(one, two);
			Assert.IsFalse(one == two);
			Assert.IsTrue(one != two);
			Assert.AreNotEqual(one.GetHashCode(), two.GetHashCode());

			one.Date = DateTime.Today;

			Assert.AreEqual(one, two);
			Assert.IsTrue(one == two);
			Assert.IsFalse(one != two);
			Assert.AreEqual(one.GetHashCode(), two.GetHashCode());
		}
	}
}