using UnityEngine;
using System.Collections;

public class ModuleFoundation : MonoBehaviour {
	
	public string[] AllowedModules;
	public ShipModule MyModule;

	void Awake() {
		MyModule = GetComponentInChildren<ShipModule> ();
		if (!UnityEditor.ArrayUtility.Contains(AllowedModules, MyModule.GetType().ToString())) {
			Debug.Log ("Module mismatch: " + MyModule.GetType().ToString());
		}
	}
}
