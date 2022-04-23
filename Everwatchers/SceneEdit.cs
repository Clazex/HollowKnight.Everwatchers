namespace Everwatchers;

public sealed partial class Everwatchers {
	private static readonly string[] corpseSets = new[] {
		"corpse_set_01_0012_nail1 (3)",
		"corpse_set_01_0012_nail1",
		"corpse_set_01_0012_nail1 (1)",
		"corpse_set_01_0012_nail1 (2)",
		"corpse_set_01_0006_nail2",
		"rest_frame_03",
		"rest_frame_03 (1)",
		"rest_frame_02",
		"rest_frame_02 (1)"
	};

	internal static readonly (int level, int num, int prefab, float x, bool faceRight)[] extrasInfo = new[] {
		(1, 7, 2, 55.82f, false),
		(1, 8, 0, 26.91f, true),
		(1, 9, 1, 45.17f, true),
		(1, 10, 3, 32.14f, true),
		(2, 11, 4, 61.238f, true),
		(2, 12, 1, 37.362f, false),
		(3, 13, 3, 46.736f, false),
		(3, 14, 5, 50.72f, true),
		(3, 15, 1, 33.29f, false)
	};


	internal static void EditScene(Scene _, Scene next) {
		if (next.name != "GG_Watcher_Knights") {
			return;
		}

		if (BossSequenceController.IsInSequence && !GlobalSettings.modifyPantheons) {
			return;
		}

		GameObject battleCtrl = next.GetRootGameObjects()
			.First(go => go.name == "Battle Control");

		next.GetRootGameObjects()
			.First(go => go.name == "Boss Scene Controller")
			.transform.Find("door_dreamEnter")
			.SetPositionX(42.42f);

		PlayMakerFSM battleCtrlFsm = battleCtrl.LocateMyFSM("Battle Control");
		battleCtrlFsm.ChangeTransition("Start Notify", FsmEvent.Finished.Name, "Knight 6");
		battleCtrlFsm.GetState("Knight 6").Actions[0] = new CustomFsmAction() {
			method = () => PlayMakerFSM.BroadcastEvent("WAKE")
		};

		var knights = new[] { 1, 2, 3, 4, 5, 6 }
			.Map(s => "Black Knight " + s)
			.Map(name => battleCtrl.Child(name)!)
			.ToList();

		if (GlobalSettings.ReanimationLevel > 0) {
			next.GetRootGameObjects()
				.Filter(go => corpseSets.Contains(go.name))
				.ForEach(GameObject.Destroy);

			GameObject[] extraKnights = extrasInfo
				.Filter(info => info.level <= GlobalSettings.ReanimationLevel)
				.Map(info => {
					GameObject prefab = knights[info.prefab];
					var extra = GameObject.Instantiate(prefab, prefab.transform.parent);
					extra.transform.position = prefab.transform.position with { x = info.x };
					extra.name = "Black Knight " + info.num;
					extra.transform.SetScaleX(info.faceRight ? -1 : 1);
					return extra;
				})
				.ToArray();

			knights.AddRange(extraKnights);
			battleCtrlFsm.FsmVariables.FindFsmInt("Battle Enemies").Value = knights.Count;
		}

		knights
			.Map(go => go.LocateMyFSM("Black Knight"))
			.Map(fsm => fsm.GetState("Roar"))
			.ForEach(state => state.Actions = new[] {
				state.Actions[0],
				state.Actions[1],
				state.Actions[8],
				state.Actions[11],
			});

		knights.ForEach(EnemyHPBar.MarkAsNonBoss);

		if (GlobalSettings.shareHealth) {
			knights.ShareHealth(
				BossSceneController.Instance.BossLevel.Clamp(0, 1)
					* knights.Count * (600 - 350),
				nameof(Everwatchers)
			);
		}
	}
}
