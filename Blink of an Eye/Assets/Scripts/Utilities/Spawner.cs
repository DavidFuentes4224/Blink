using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	public Transform prefab;
	public float timeTillSpawn = 5.0f;
	// Use this for initialization
	void Start () {
		StartCoroutine("BeginSpawn");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Spawn(){
		Instantiate(prefab,transform.position,Quaternion.identity);
	}

	IEnumerator BeginSpawn()
	{
		while(true)
		{
			this.Spawn();
			yield return new WaitForSeconds (timeTillSpawn);
		}

	}
}
