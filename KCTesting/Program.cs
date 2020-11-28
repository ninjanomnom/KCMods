using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Harmony;
using Harmony.ILCopying;

namespace KCTesting {
	class Program {
		static void Main(string[] args) {
			var instruction = new CodeInstruction(OpCodes.Ldloc_S, "test");
		}
	}
}