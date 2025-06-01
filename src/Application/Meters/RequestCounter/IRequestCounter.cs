namespace SKB.App.Application.Meters.RequestCounter;

/// <summary>
/// Provides a Request Counter Mechanism, that analyzes the Requests handled by the Service
/// </summary>
public interface IRequestCounter
{
	/// <summary>
	/// Increments the count with the delta
	/// </summary>
	/// <param name="delta">Number with which the counter should be incremented</param>
	/// <param name="metadata">To add to the counter</param>
	void Add(int delta, params KeyValuePair<string, object?>[] metadata);
}
