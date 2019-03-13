using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ModelStoneButton : MonoBehaviour, IPointerDownHandler {
	public void OnPointerDown(PointerEventData eventData) {
		Debug.Log("===== on click stone image =====");
	}

	void Start() {
        
    }

    void Update() {
        
    }
}
