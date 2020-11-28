using System;
using System.Reflection;
using System.Text;
using Assets;
using Harmony;
using UnityEngine;

namespace KingdomsAndCastles.Common {
	public class Common : MonoBehaviour {
		public static Common Instance;
		
		internal KCModHelper Helper;
		internal HarmonyInstance Harmony;

		private void SceneLoaded(KCModHelper helper) {
			
		}

		private void Preload(KCModHelper helper) {
			Instance = this;

			Helper = helper;

			Helper.Log("Initializing dll mod injection...");

			Harmony = HarmonyInstance.Create("mod.ninjanomnom.InjectionUnlock");
			Harmony.PatchAll();

			Instance.Helper.Log("Second pass on mod loading for insecure mods starting...");
			
			var loadInfo = typeof(KCModHelper.ModLoader).GetMethod("LoadLocalMods",
				BindingFlags.NonPublic | BindingFlags.Instance);
			loadInfo.Invoke(SceneSwitcher.modLoader, new object[] { });
			
			Instance.Helper.Log("Second pass complete!");
		}

		public void DebugOutput(object thing) {
			try {
				var builder = new StringBuilder();
				builder.AppendLine($"Beginning debug output of {thing}<{thing?.GetType()}>");

				builder.AppendLine("Properties:");
				foreach (var prop in thing?.GetType()?.GetProperties()) {
					var value = prop.GetValue(thing);
					builder.AppendLine($"{prop.Name}: {value}");
				}

				builder.AppendLine("Fields:");
				foreach (var prop in thing?.GetType()?.GetFields()) {
					var value = prop.GetValue(thing);
					builder.AppendLine($"{prop.Name}: {value}<{value.GetType()}>");
				}

				Helper.Log(builder.ToString());
			}
			catch (Exception e) {
				Helper.Log($"Error while trying to output debug data ({thing})");
				Helper.Log(e.ToString());
			}
		}
	}
}
