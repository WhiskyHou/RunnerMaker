using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killer : MonoBehaviour {

	private GameManager manager;

    void Start() {
        
    }

    void Update() {
        
    }

	void OnCollisionEnter2D(Collision2D collision) {
		if(collision.gameObject.tag == "Player") {
			manager.GameOver();
		}
	}

	public void Init(GameManager manager) {
		this.manager = manager;
	}
}
