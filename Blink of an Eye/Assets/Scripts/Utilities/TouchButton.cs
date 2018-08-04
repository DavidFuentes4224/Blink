using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class TouchButton : MonoBehaviour, IPointerUpHandler , IPointerDownHandler{
	public bool Pressed;
	public bool Held;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		Pressed = true;
		//print("HELLLLLLLD");
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		Pressed = false;
	}

}
