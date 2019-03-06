using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class DemoAnimTest : MonoBehaviour {
	public DragonBones.UnityArmatureComponent ua;

    // Start is called before the first frame update
    void Start() {
		ua = GetComponent<DragonBones.UnityArmatureComponent>();
		//ua.armature.GetSlot("boy-stand").armature = DragonBones.UnityFactory.factory.BuildArmature("girl-stand");
		//ua.animation.Play("boy-stand");
		Debug.Log(Time.realtimeSinceStartup);
		Stop();
    }

    // Update is called once per frame
    void Update() {
        
    }

	private async Task Stop() {
		await Task.Delay(5000);
		ua.animation.Stop("boy-stand");
		Debug.Log(Time.realtimeSinceStartup);
	}
}
