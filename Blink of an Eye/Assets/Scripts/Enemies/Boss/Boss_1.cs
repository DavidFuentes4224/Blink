using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class Boss_1 : MonoBehaviour {

	public TextMesh a;
	public TextMesh b;
	public TextMesh opp;
	public Transform fallNumPrefab;
	public int solution;

	//private refrences
	int tempValue,tempValue2 = 0;
	int health = 3;
	int currentStage = 1;


	// Use this for initialization
	void Start () {
		GetRandomNumbers(1);
		StartWave();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GetRandomNumbers(int stage)
	{
		switch(stage)
		{	
			case 1:
				this.tempValue = (int) Random.Range(2,9);
				a.text =  this.tempValue.ToString();
				this.tempValue2 = (int) Random.Range(1,this.tempValue);
				b.text =  this.tempValue2.ToString();
				solution = this.tempValue-this.tempValue2;
				opp.text = "-";
				break;
			case 2:
				this.tempValue = (int) Random.Range(10,19);
				a.text =  this.tempValue.ToString();
				this.tempValue2 = (int) Random.Range(1,9);
				b.text =  this.tempValue2.ToString();
				solution = this.tempValue+this.tempValue2;
				opp.text = "+";
				break;
			case 3:
				this.tempValue = (int) Random.Range(1,9);
				a.text =  this.tempValue.ToString();
				this.tempValue2 = (int) Random.Range(1,9);
				b.text =  this.tempValue2.ToString();
				solution = this.tempValue * this.tempValue2;
				opp.text = "*";
				break;
			default:
				//dead!
				Debug.Log("The boss has been defeated!!!");
				Destroy(this.gameObject);
				break;
		}
		
	}

	public void SpawnNumber()
	{
		int leftRange = (int) (this.solution.ToString()[0] - 48);
		int rightRange = (int) (this.solution.ToString()[0] - 48);
		if((int) (this.solution.ToString()[0] - 48) > 4)
		{
			leftRange -=4;
		}
		else
		{
			leftRange = 0;
		}
		if ((int) (this.solution.ToString()[0] - 48) < 9)
		{
			rightRange += 5;
		}
		else
		{
			rightRange = 10;
		}
		int rNum = (int) Random.Range(leftRange,rightRange);
		float xPos = Random.Range(0.0f,-8.0f);
		Transform inst = Instantiate(fallNumPrefab, new Vector3(xPos,4,0), Quaternion.identity);
		inst.GetComponent<FallingNumber>().SetDisplay(rNum);
		Debug.Log("rNum = " + rNum + " : string value = " + (int) (this.solution.ToString()[0] - 48));
		if(rNum == (int) (this.solution.ToString()[0] - 48))
		{
			inst.GetComponent<FallingNumber>().makeSolution();
		}
	}

	public void TakeDamage()
	{
		Debug.Log("Taking Damage!!!!");
		if(solution > 9)
		{
			solution = solution % 10;
		}
		else
		{
			this.health -= 1;
			this.currentStage += 1;
			GetRandomNumbers(currentStage);
		}
	}

	public void StartWave()
	{
		StartCoroutine("SpawnLoop");
	}

	public void DecreaseStage()
	{
		Debug.Log("Decreasing stage");
		if(currentStage > 1)
		{
			currentStage-= 1;
		}
		GetRandomNumbers(currentStage);
	}

	IEnumerator SpawnLoop()
	{
		while(health > 0)
		{
			switch(currentStage)
			{
			case 1:
				SpawnNumber();
				yield return new WaitForSeconds(1.0f);
				break;
			case 2:
				SpawnNumber();
				yield return new WaitForSeconds(1.0f);
				break;
			case 3:
				SpawnNumber();
				yield return new WaitForSeconds(1.0f);
				break;
			default:
				//Destory Self
				Destroy(this);
				break;
			}
		}
		yield return null;
	}
}




[CustomEditor(typeof(Boss_1))]
public class BossEditor : Editor 
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		Boss_1 boss = (Boss_1)target;
		if(GUILayout.Button("Randomize Numbers"))
		{
			boss.GetRandomNumbers(1);
		}
		if(GUILayout.Button("Spawn New Number"))
		{
			boss.SpawnNumber();
		}

		if(GUILayout.Button("Spawn Wave"))
		{
			boss.StartWave();
		}
	}
}
