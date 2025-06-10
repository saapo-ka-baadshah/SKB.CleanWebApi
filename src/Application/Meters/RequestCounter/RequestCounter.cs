using System.Diagnostics.Metrics;
using System.Reflection;

namespace SKB.App.Application.Meters.RequestCounter;

/// <summary>
/// Actual Injectable Request Counter
/// </summary>
/// <typeparam name="TAssembly">Type Assembly for which the Counter is created</typeparam>
public class RequestCounter<TAssembly>: IRequestCounter
{
	private readonly Counter<int> _requestCounter;

	/// <summary>
	/// Gives the Request Counter for an assembly
	/// </summary>
	/// <param name="meter"></param>
	public RequestCounter(Meter meter)
	{
		this._requestCounter = meter.CreateCounter<int>(
			$"{typeof(TAssembly).Namespace}.RequestCounter",
			"requests",
			"Total number of Requests handled by the assembly"
			);
	}

	///<inheritdoc cref="IRequestCounter.Add"/>
	public void Add(int delta, params KeyValuePair<string, object?>[] metadata)
	{
		this._requestCounter.Add(delta, metadata);
	}
}
