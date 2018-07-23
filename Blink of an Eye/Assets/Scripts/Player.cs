using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public Player_Body bodyRef;

    public float life = 100;
    public float lifeDecay;
    public Transform bodyPrefab;

    public Level currentLevel;

    // Use this for initialization
    private void Awake() {
        Transform prefab = Instantiate(bodyPrefab,currentLevel.getSpawn(),Quaternion.identity);
        bodyRef = prefab.GetComponent<Player_Body>();
    }
    void Start () {
        FollowCamera cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowCamera>();
        cam.enabled = true;
        cam.target = this.bodyRef.transform;
        //Time.timeScale = 0.5f; //slow motion
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.R))
        {
            this.SpawnNewBody();
        }
	}

    public void Win()
    {
        currentLevel.LoadNextLevel(currentLevel.nextLevel);
    }

    public void SpawnNewBody()
    {
        bodyRef.Kill();
        Transform prefab = Instantiate(bodyPrefab,currentLevel.getSpawn(),Quaternion.identity);
        bodyRef = prefab.GetComponent<Player_Body>();
    }
}
