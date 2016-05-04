using System;
using Kobsky.ActualTimer;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;

namespace DomainTests
{
	[TestFixture]
	public class ActualTimerAppCoreTests
	{
		[Test]
		public void Ctor()
		{
			var timerRepository = Substitute.For<ITimerRepository>();
			timerRepository.GetTodayOrDefault().Returns(new Timer());

			ActualTimerAppCore core = new ActualTimerAppCore(timerRepository);

			Assert.AreEqual(timerRepository, core.TimerRepository);
		}

		[Test]
		public void CtorWithNullTimerRepository()
		{
			ArgumentNullException ex = Assert.Throws<ArgumentNullException>(delegate
			{
				// ReSharper disable once ObjectCreationAsStatement
				new ActualTimerAppCore(null);
			});

			Assert.IsTrue(ex.Message.Contains("timerRepository"));
		}

		[Test]
		public void CtorGetTodayTimer()
		{
			var timerRepository = Substitute.For<ITimerRepository>();
			timerRepository.GetTodayOrDefault()
				.Returns(new Timer {Value = new TimeSpan(50)});

			ActualTimerAppCore core = new ActualTimerAppCore(timerRepository);

			Assert.AreEqual(new TimeSpan(50), core.Value);
		}

		[Test]
		public void CtorGetTodayTimerReturnNull()
		{
			NullReturnNotAllowedException ex = Assert.Throws<NullReturnNotAllowedException>(delegate
			{
				var timerRepository = Substitute.For<ITimerRepository>();
				timerRepository.GetTodayOrDefault()
					.ReturnsNull();

				// ReSharper disable once ObjectCreationAsStatement
				new ActualTimerAppCore(timerRepository);
			});

			Assert.IsTrue(ex.Message.Contains("ITimerRepository.GetTodayOrDefault method return null"));
		}
	}
}
