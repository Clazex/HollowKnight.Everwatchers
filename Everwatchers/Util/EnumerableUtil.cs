namespace Everwatchers.Util;

internal static class EnumerableUtil {
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static IEnumerable<U> Map<T, U>(this IEnumerable<T> self, Func<T, U> f) =>
		self.Select(f);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static IEnumerable<T> Filter<T>(this IEnumerable<T> self, Func<T, bool> f) =>
		self.Where(f);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static void ForEach<T>(this IEnumerable<T> self, Action<T> f) {
		foreach (T i in self) {
			f(i);
		}
	}

	internal static IEnumerable<int> Range(int start, int end) {
		for (int i = start; i <= end; i++) {
			yield return i;
		}
	}
}
