using static Modding.IMenuMod;

namespace Everwatchers;

public sealed partial class Everwatchers : IMenuMod {
	bool IMenuMod.ToggleButtonInsideMenu => true;

	List<MenuEntry> IMenuMod.GetMenuData(MenuEntry? toggleButtonEntry) => new() {
		toggleButtonEntry!.Value,
		new(
			"Option/ReanimationLevel".Localize(),
			EnumerableUtil.Range(0, 3)
				.Map(i => $"ReanimationLevel/{i}".Localize())
				.ToArray(),
			"",
			i => GlobalSettings.ReanimationLevel = i,
			() => GlobalSettings.ReanimationLevel
		),
		new(
			"Option/ShareHealth".Localize(),
			new string[] {
				Lang.Get("MOH_OFF", "MainMenu"),
				Lang.Get("MOH_ON", "MainMenu")
			},
			"",
			i => GlobalSettings.shareHealth = Convert.ToBoolean(i),
			() => Convert.ToInt32(GlobalSettings.shareHealth)
		),
		new(
			"Option/ModifyPantheons".Localize(),
			new string[] {
				Lang.Get("MOH_OFF", "MainMenu"),
				Lang.Get("MOH_ON", "MainMenu")
			},
			"",
			i => GlobalSettings.modifyPantheons = Convert.ToBoolean(i),
			() => Convert.ToInt32(GlobalSettings.modifyPantheons)
		)
	};
}
