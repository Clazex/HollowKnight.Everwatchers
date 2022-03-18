namespace Everwatchers;

public sealed partial class Everwatchers : Mod, ITogglableMod {
	public static Everwatchers? Instance { get; private set; }
	public static Everwatchers UnsafeInstance => Instance!;

	internal static readonly Lazy<string> Version = new(() => Assembly
		.GetExecutingAssembly()
		.GetCustomAttribute<AssemblyInformationalVersionAttribute>()
		.InformationalVersion
#if DEBUG
		+ "-dev"
#endif
	);

	public override string GetVersion() => Version.Value;

	public override void Initialize() {
		if (Instance != null) {
			LogWarn("Attempting to initialize multiple times, operation rejected");
			return;
		}

		Instance = this;

		USceneManager.activeSceneChanged += EditScene;
		ModHooks.LanguageGetHook += Localize;
	}

	public void Unload() {
		USceneManager.activeSceneChanged -= EditScene;
		ModHooks.LanguageGetHook -= Localize;

		Instance = null;
	}

	private static string Localize(string key, string sheet, string orig) => sheet switch {
		"Journal" when key is "NAME_BLACK_KNIGHT" => "Name".Localize(),
		"CP3" when key is "GG_S_WATCHERKNIGHTS" => "Desc".Localize(),
		"Titles" => key switch {
			"BLACK_KNIGHT_SUPER" => "Title/Super".Localize(),
			"BLACK_KNIGHT_MAIN" => "Title/Main".Localize(),
			"BLACK_KNIGHT_SUB" => "Title/Sub".Localize(),
			_ => orig,
		},
		_ => orig,
	};
}
