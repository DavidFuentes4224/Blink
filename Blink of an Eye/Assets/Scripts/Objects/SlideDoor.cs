using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideDoor : ActivateObject {

	public override void Activate(){
		Destroy(this.gameObject);
	}

	public override void Deactivate(){
		return;
	}
}
