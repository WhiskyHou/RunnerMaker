using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {

	public float fireDelta;

	public GameObject bullet;

	public GameObject firePoint;

	public ObjPool pool;

	private float fireTime = 0f;

    void Start() {
		pool = new ObjPool(bullet);
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
		GameObject bulletLeft = pool.Get();
		bulletLeft.SetActive(true);
		bulletLeft.GetComponent<SharkBullet>().Init(firePoint.transform, true);
		bulletLeft.transform.SetParent(gameObject.transform);

		GameObject bulletRight = pool.Get();
		bulletRight.SetActive(true);
		bulletRight.GetComponent<SharkBullet>().Init(firePoint.transform, false);
		bulletRight.transform.SetParent(gameObject.transform);
	}
}
