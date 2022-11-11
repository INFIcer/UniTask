using System;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
	public class AsyncEvent : IUniTaskAsyncEnumerable<AsyncUnit>, IDisposable
	{
		AsyncReactiveProperty<AsyncUnit> asyncUnit;
		public AsyncEvent()
		{
			asyncUnit = new AsyncReactiveProperty<AsyncUnit>(AsyncUnit.Default);
		}
		public void Invoke()
		{
			asyncUnit.Value = AsyncUnit.Default;
		}
		public UniTask Task => WaitAsync();
		public UniTask WaitAsync(CancellationToken cancellationToken = default)
		{
			return asyncUnit.WaitAsync(cancellationToken);
		}
		IUniTaskAsyncEnumerator<AsyncUnit> IUniTaskAsyncEnumerable<AsyncUnit>.GetAsyncEnumerator(CancellationToken cancellationToken)
		{
			return asyncUnit.WithoutCurrent().GetAsyncEnumerator(cancellationToken);
		}
		public void Dispose()
		{
			asyncUnit.Dispose();
		}
	}

	public class AsyncReactiveState<T> : IUniTaskAsyncEnumerable<T>, IDisposable
	{
		AsyncReactiveProperty<T> asyncProperty;
		public AsyncReactiveState(T value)
		{
			asyncProperty = new AsyncReactiveProperty<T>(value);
		}
		public T Value
		{
			get => asyncProperty.Value;
			set
			{
				if (!asyncProperty.Value.Equals(value))
					asyncProperty.Value = value;
			}
		}
		public UniTask<T> Task => WaitAsync();
		public UniTask<T> WaitAsync(CancellationToken cancellationToken = default)
		{
			return asyncProperty.WaitAsync(cancellationToken);
		}
		IUniTaskAsyncEnumerator<T> IUniTaskAsyncEnumerable<T>.GetAsyncEnumerator(CancellationToken cancellationToken)
		{
			return asyncProperty.GetAsyncEnumerator(cancellationToken);
		}
		public void Dispose()
		{
			asyncProperty.Dispose();
		}
	}
}