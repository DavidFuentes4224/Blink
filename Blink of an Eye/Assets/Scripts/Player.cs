using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public Player_Movement pmRef;

    public float life = 100;
    public float lifeDecay;
    public Transform bodyPrefab;

    public Level currentLevel;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.R))
        {
            this.Kill(pmRef);
        }
	}

    public void Kill(Player_Movement pawn)
    {
        pawn.Kill();
        Transform prefab = Instantiate(bodyPrefab,currentLevel.getSpawn(),Quaternion.identity);
        pmRef = prefab.GetComponent<Player_Movement>();
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowCamera>().setTarget(prefab);
    }
}
