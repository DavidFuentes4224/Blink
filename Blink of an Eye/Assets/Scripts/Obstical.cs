using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstical : MonoBehaviour {
    public void OnTriggerEnter2D(Collider2D other)
    {
		if (other.tag == "Player")
        {
			Debug.Log ("Kill player");
			Player player = GameObject.FindObjectOfType<Player> ().GetComponent<Player> ();
			player.Kill(player.pmRef);
        }
    }
}
