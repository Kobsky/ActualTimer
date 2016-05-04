namespace Kobsky.ActualTimer
{
	public interface ITimerRepository
	{
		Timer GetTodayOrDefault();
	}
}