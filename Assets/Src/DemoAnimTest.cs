using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class DemoAnimTest : MonoBehaviour {
	public DragonBones.UnityArmatureComponent ua;

    // Start is called before the first frame update
    void Start() {
		//ua.armature.replacedTexture
		ua.animation.Play("boy-fall");
		Stop();
    }

    // Update is called once per frame
    void Update() {
        
    }

	private async Task Stop() {
		await Task.Delay(5000);
		ua.animation.Stop("boy-fall");
	}
}
