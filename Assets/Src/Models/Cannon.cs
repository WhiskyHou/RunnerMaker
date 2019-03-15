using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {

	public float fireDelta;

	public GameObject bullet;

	public GameObject firePoint;

	private float fireTime = 0f;

    void Start() {
        
    }

    void Update() {
		float delta = Time.deltaTime;
		fireTime += delta;
		if (fireTime >= fireDelta) {
			fireTime = 0f;
			OpenFire();
		}
    }

	private void OpenFire() {
		GameObject bulletLeft = Instantiate(bullet);
		bulletLeft.GetComponent<SharkBullet>().isLeft = true;
		bulletLeft.transform.SetParent(gameObject.transform);
		bulletLeft.transform.localPosition = firePoint.transform.localPosition;

		GameObject bulletRight = Instantiate(bullet);
		bulletRight.GetComponent<SharkBullet>().isLeft = false;
		bulletRight.transform.SetParent(gameObject.transform);
		bulletRight.transform.localPosition = firePoint.transform.localPosition;
	}
}
