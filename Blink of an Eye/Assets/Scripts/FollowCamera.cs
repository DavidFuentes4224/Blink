using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {
    public Transform target;
    public float horizontalMovement;
    public float verticalMovement;

	// Use this for initialization
	void Start () {
        target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        float tarPosX = Mathf.Lerp(calcTargetPosition(this.transform).x, calcTargetPosition(target).x, Time.deltaTime * horizontalMovement);
        float tarPosY = Mathf.Lerp(calcTargetPosition(this.transform).y, calcTargetPosition(target).y, Time.deltaTime * verticalMovement);

        Vector3 newPosition = (new Vector3(tarPosX, tarPosY, -10));
        this.transform.position = newPosition;
    }

    private Vector2 calcTargetPosition(Transform t)
    {
        Vector2 position = new Vector2(t.position.x, t.position.y);
        return position;
    }
}
