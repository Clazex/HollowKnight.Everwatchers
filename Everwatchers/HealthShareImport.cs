using MonoMod.ModInterop;

namespace Everwatchers;

internal static class HealthShareImport {
#pragma warning disable CS0649
	[ModImportName(nameof(HealthShare))]
	private static class HealthShare {
		public static Func<IEnumerable<GameObject>, int, string, MonoBehaviour>? ShareHealth;
	}
#pragma warning restore CS0649

	static HealthShareImport() =>
		typeof(HealthShare).ModInterop();

	internal static void ShareHealth(this IEnumerable<GameObject> self, int initialHealth, string name) =>
		HealthShare.ShareHealth?.Invoke(self, initialHealth, name);
}
