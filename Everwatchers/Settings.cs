using Newtonsoft.Json;

namespace Everwatchers;

public sealed partial class Everwatchers : IGlobalSettings<GlobalSettings> {
	public static GlobalSettings GlobalSettings { get; private set; } = new();
	public void OnLoadGlobal(GlobalSettings s) => GlobalSettings = s;
	public GlobalSettings OnSaveGlobal() => GlobalSettings;
}

public sealed class GlobalSettings {
	[JsonIgnore]
	private int reanimationLevel = 0;

	[JsonProperty(PropertyName = nameof(reanimationLevel))]
	public int ReanimationLevel {
		get => reanimationLevel;
		set => reanimationLevel = value.Clamp(0, 3);
	}

	public bool shareHealth = false;
	public bool modifyPantheons = true;
}
