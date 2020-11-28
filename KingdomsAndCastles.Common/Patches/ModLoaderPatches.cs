using System.Linq;
using Harmony;

namespace KingdomsAndCastles.Common.Patches {
	public static class ModLoaderPatches {
		[HarmonyPatch(typeof(KCModHelper.ModLoader), "LoadMod")]
		public static class LoadModPatches {
			private static bool Prefix(string mod) {
				if (KCModHelper.ModLoader.localModsLoadedAtStartup.Any(m => m.path == mod)) {
					Common.Instance.Helper.Log($"Mod found at {mod} is already loaded, skipping load...");
					return false;
				}
				
				Common.Instance.DebugOutput(KCModHelper.ModLoader.localModsLoadedAtStartup.First());
				
				Common.Instance.Helper.Log($"Loading mod at {mod} with patched load process");
				return true;
			}
		}
	}
}