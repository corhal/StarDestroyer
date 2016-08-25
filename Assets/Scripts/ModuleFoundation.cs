using UnityEngine;
using System.Collections;

public class ModuleFoundation : MonoBehaviour {
	
	public string[] AllowedModules;
	public Module MyModule;

	void Awake() {
		MyModule = GetComponentInChildren<Module> ();
		Debug.Log (MyModule.GetType ());
	}
}
