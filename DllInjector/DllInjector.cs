using System.Reflection;

namespace DllInjector {
	public static class DllInjector {
		public static void Main(KCModHelper helper) {
			var ourAssembly = Assembly.GetExecutingAssembly();
			helper.Log($"We are executing from assembly {ourAssembly}");
		}
	}
}