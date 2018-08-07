using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour {
    public Transform spawnRef;
	public string nextLevel;
    public Text timerText;
    float clock;

    private void Start() {
        this.resetTime();
        Door[] doors = FindObjectsOfType<Door>();
        foreach(Door d in doors)
        {
            d.Lock();
        }
    }

    private void Update() {
        clock +=Time.deltaTime;
        timerText.text = this.getTime().ToString();
    }

    public Vector2 getSpawn()
    {
        return spawnRef.position;
    }

    public void LoadNextLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void QuitGame()
    {
        //save
        //quit
        Application.Quit();
    }

    public float getTime() {
		float num = clock;
		num = (Mathf.Round(num * 100f) / 100f);
		return num;
	}

	public void resetTime() {
		clock = 0.0f;
	}
}

