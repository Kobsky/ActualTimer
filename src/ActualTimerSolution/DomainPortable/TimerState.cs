using System;

namespace Kobsky.ActualTimer
{
	/// <summary>to do</summary>
	/// <summary xml:lang="ru">
	/// Представляет из себя state для <see cref="Timer"/> с помощью которого он может сохранять и восстанавливать свои значения
	/// </summary>
	public struct TimerState: IEquatable<TimerState>
	{
		private TimeSpan _value;
		private DateTime _date;

		// ReSharper disable once ConvertToAutoProperty
		public TimeSpan Value
		{
			get { return _value; }
			set { _value = value; }
		}

		// ReSharper disable once ConvertToAutoProperty
		public DateTime Date
		{
			get { return _date; }
			set { _date = value; }
		}

		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		public bool Equals(TimerState other)
		{
			return Value.Equals(other.Value) && Date.Equals(other.Date);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (Value.GetHashCode()*397) ^ Date.GetHashCode();
			}
		}

		public static bool operator ==(TimerState state1, TimerState state2)
		{
			return state1.Equals(state2);
		}

		public static bool operator !=(TimerState state1, TimerState state2)
		{
			return !state1.Equals(state2);
		}

	}
}