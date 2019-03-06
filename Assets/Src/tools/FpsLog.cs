using UnityEngine;

public class FpsLog {
	private const float __LOG_DELTA_TIME = 5.0f;
	private float logDeltaTime = 0.0f;

    public void Update(float delta) {
        if (logDeltaTime > __LOG_DELTA_TIME) {
			Debug.Log("=== FPS === " + 1.0 / delta);
			logDeltaTime = 0.0f;
		} else {
			logDeltaTime += delta;
		}
    }
}
