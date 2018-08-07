using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //modifiables
    public float life = 100;
    public float lifeDecay;
    public Transform bodyPrefab;

    public Level currentLevel;

    //private components
    Player_Body bodyRef;
    public List<int> inventory = new List<int>{};
    //input support
    public TouchButton Jump;
    public TouchButton Left;
    public TouchButton Right;

    // Use this for initialization
    private void Awake() {
        Transform prefab = Instantiate(bodyPrefab,currentLevel.getSpawn(),Quaternion.identity);
        bodyRef = prefab.GetComponent<Player_Body>();
        bodyRef.SetButtons(Jump,Left,Right);
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
        this.life -= lifeDecay * Time.deltaTime;
        if(this.life < 0)
        {
            this.SpawnNewBody();
        }
	}

    public void Win()
    {
        currentLevel.LoadNextLevel(currentLevel.nextLevel);
    }

    public Transform GetBodyRef()
    {
        return this.bodyRef.transform;
    }
    public void SpawnNewBody()
    {
        bodyRef.Kill();
        Transform prefab = Instantiate(bodyPrefab,currentLevel.getSpawn(),Quaternion.identity);
        bodyRef = prefab.GetComponent<Player_Body>();
        bodyRef.SetButtons(Jump,Left,Right);
        this.life = 100;
    }

    public void AddToInventory(int doorNumber)
    {
        inventory.Add(doorNumber);
        Door[] doors =  GameObject.FindObjectsOfType<Door>();
        foreach (Door d in doors)
        {
            if(doorNumber == d.number)
            {
                d.Unlock();
            }
        }
    }
}
