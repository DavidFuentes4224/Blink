using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
	Player_Body player;
		//Touch input
	TouchButton b_Jump;
	TouchButton b_Left;
	TouchButton b_Right;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		player = this.gameObject.GetComponent<Player>().bodyRef;

		//*******************************IOS*/
		#if UNITY_IOS
		int left = b_Left.Pressed? -1 : 0;
		int right = b_Right.Pressed? 1: 0;
		input = new Vector2(left + right, 0);
		
		//********************WINDOWS */
		#elif UNITY_STANDALONE
		
		Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		#endif

		//NORMAL
		player.SetDirectionalInput(input);

		if(Input.GetKeyDown(KeyCode.Space))
		{
			player.OnJumpInputDown();
			Debug.Log("Space Down");
		}
		if(Input.GetKeyUp(KeyCode.Space)){
			player.OnJumpInputUp();
			Debug.Log("Space Up");
		}
	}
}
