using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {
    public Transform spawnRef;
	// Use this for initialization

    public Vector2 getSpawn()
    {
        return spawnRef.position;
    }
}
