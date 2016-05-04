using System;
using Kobsky.ActualTimer;
using NUnit.Framework;

namespace DomainTests
{
	[TestFixture]
	public class TimerTests
	{
		[Test]
		public void Ctor()
		{
			Timer timer = new Timer
			{
				Value = new TimeSpan(100)
			};

			Assert.AreEqual(new TimeSpan(100), timer.Value );
		}
	}
}
