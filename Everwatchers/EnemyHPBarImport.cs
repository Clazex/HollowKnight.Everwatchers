using MonoMod.ModInterop;

namespace Everwatchers;

internal static class EnemyHPBar {
	[ModImportName(nameof(EnemyHPBar))]
	private static class EnemyHPBarImport {
		public static Action<GameObject> MarkAsNonBoss = null!;
	}

	static EnemyHPBar() =>
		typeof(EnemyHPBarImport).ModInterop();

	internal static void MarkAsNonBoss(this GameObject self) =>
		EnemyHPBarImport.MarkAsNonBoss?.Invoke(self);
}
