using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour {
	public Transform launchObj;
	public Transform body;
	Player_Body player;
	float targetAngle;
	float speed = 3.0f;

	public int maxObjs = 3;
	int objsOnScreen;

	// Use this for initialization
	void Start () {
		objsOnScreen = 0;
		StartCoroutine("Timer");
	}
	
	// Update is called once per frame
	void Update () {
		player = FindObjectOfType<Player>().bodyRef;
		float angle = AngleBetween(body.transform.position,player.transform.position);
		targetAngle = Mathf.Lerp(targetAngle,angle, Time.deltaTime * speed);

		body.transform.rotation = Quaternion.Euler(new Vector3(0,0,targetAngle + 90));
	}

	float AngleBetween(Vector3 a, Vector3 b)
	{
		return Mathf.Atan2(a.y-b.y,a.x-b.x) * Mathf.Rad2Deg;
	}

	public void FireRocket(){
		Transform r = Instantiate(launchObj,body.transform.position,Quaternion.identity);
		r.GetComponent<Rocket>().addLauncher(this);
		objsOnScreen += 1;
	}

	public void DecreaseObjectCount()
	{
		objsOnScreen -= 1;
	}

	IEnumerator Timer(){
		while(true)
		{
			if(objsOnScreen < maxObjs)
			{
				FireRocket();
			}
			yield return new WaitForSeconds(4.0f);
		}
	}
}
