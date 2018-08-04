using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour {
    public Transform spawnRef;
	public string nextLevel;

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
}
