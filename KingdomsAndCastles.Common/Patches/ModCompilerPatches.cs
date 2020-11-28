using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Harmony;

namespace KingdomsAndCastles.Common.Patches {
	public class ModCompilerPatches {
		[HarmonyPatch(typeof(ModCompiler), "LoadMod")]
		public static class LoadModPatches {
			private static bool Prefix() {
				Common.Instance.Helper.Log("Beginning unsecure mod load...");
				return true;
			}
			
			private static void Postfix(object __result, ref object ___domain) {
				var compileResult = ___domain.GetType().GetProperty("CompileResult").GetValue(___domain);
				
				Common.Instance.Helper.Log("Unsecure mod load completed!");
			}
			
			private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions) {
				Common.Instance.Helper.Log("Beginning security bypass...");

				var instructionsList = instructions.ToList();
				
				// First we find a known location to locate the security mode used
				var securityLandmark = instructionsList
					.First(i => i.opcode == OpCodes.Ret);

				var securityIndex = instructionsList.IndexOf(securityLandmark);

				var securityInstruction = instructionsList.First(i => 
					i.opcode == OpCodes.Ldc_I4_0
					&& instructionsList.IndexOf(i) > securityIndex
				);

				Common.Instance.Helper.Log(
					$"Found security mode instruction at index {instructionsList.IndexOf(securityInstruction)}");
				
				securityInstruction.opcode = OpCodes.Ldc_I4_1;

				Common.Instance.Helper.Log("Security mode modified, cleaning of security flag...");
				
				// Now we clean up so that it doesn't break when no security report comes in
				var target = instructionsList.First(i =>
					i.opcode == OpCodes.Leave
					&& instructionsList.IndexOf(i) > instructionsList.IndexOf(securityInstruction));
				
				target = instructionsList.First(i =>
					i.opcode == OpCodes.Leave
					&& instructionsList.IndexOf(i) > instructionsList.IndexOf(target));
				
				target = instructionsList.First(i =>
					i.opcode == OpCodes.Ldc_I4_0
					&& instructionsList.IndexOf(i) > instructionsList.IndexOf(target));
				
				target = instructionsList.First(i =>
					i.opcode == OpCodes.Ldc_I4_0
					&& instructionsList.IndexOf(i) > instructionsList.IndexOf(target));

				Common.Instance.Helper.Log(
					$"Found flag assignment at index {instructionsList.IndexOf(target)}");

				target.opcode = OpCodes.Ldc_I4_1;

				Common.Instance.Helper.Log("Security has been bypassed, future mods will not be restricted");
				
				return instructionsList;
			}
		}
	}
}