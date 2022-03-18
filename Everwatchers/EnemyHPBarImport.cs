using MonoMod.ModInterop;

namespace Everwatchers;

internal static class EnemyHPBar {
#pragma warning disable CS0649
	[ModImportName(nameof(EnemyHPBar))]
	private static class EnemyHPBarImport {
		public static Action<GameObject>? MarkAsNonBoss;
	}
#pragma warning restore CS0649

	static EnemyHPBar() =>
		typeof(EnemyHPBarImport).ModInterop();

	internal static void MarkAsNonBoss(this GameObject self) =>
		EnemyHPBarImport.MarkAsNonBoss?.Invoke(self);
}
