using System.IO;
using UnityEngine;

namespace zLoadTesting {
	public class LoadTester : MonoBehaviour {
		private void Preload(KCModHelper helper) {
			helper.Log(Directory.GetParent(helper.modPath).ToString());
		}
	}
}