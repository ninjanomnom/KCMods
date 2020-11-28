using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace LoadSystemIOExample {
	public static class IOLoader {
		public static void LoadDlls() {
			var assembly = Assembly.GetEntryAssembly();
			var assemblyLocation = assembly.Location;
			var modLocation = Path.Combine(assemblyLocation, "KingdomsAndCastles_Data/mods/dll-mods");

			foreach (var file in Directory.EnumerateFiles(modLocation, "*.mod.dll")) {
				var dll = Assembly.LoadFile(file);
				var entryName = $"{dll.FullName}.Main";
				var entryType = dll.GetExportedTypes().Single(t => t.FullName == entryName);
				var entry = Activator.CreateInstance(entryType);
				entryType.InvokeMember("Initialize", BindingFlags.InvokeMethod, null, entry, new object[] {""});
			}
		}
	}
}