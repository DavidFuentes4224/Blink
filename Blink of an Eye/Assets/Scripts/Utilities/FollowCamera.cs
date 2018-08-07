using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {
    public Transform target;
    public float horizontalMovement;
    public float verticalMovement;
    public float verticalDisplacement;

    public float horizontalBound = 2;
    public float verticalBound = 2;

    public Player player;

    float tarPosX = 0;
    float tarPosY = 0;
    public float speed;
	
	// Update is called once per frame
    private void Awake() {
        target = player.transform;
    }

	void Update () {
		target = player.GetBodyRef();
	}

    void FixedUpdate()
    {
        //  old camera stuff
        float tarPosX = Mathf.Lerp(calcTargetPosition(this.transform).x, calcTargetPosition(target).x, Time.deltaTime * horizontalMovement);
        float tarPosY = Mathf.Lerp(calcTargetPosition(this.transform).y, calcTargetPosition(target).y, Time.deltaTime * verticalMovement);
        tarPosY += verticalDisplacement;
        Vector3 destination = new Vector3(tarPosX,tarPosY,-10);
        this.transform.position = Vector3.Lerp(this.transform.position,destination,1);

    }

    private Vector2 calcTargetPosition(Transform t)
    {
        Vector2 position = new Vector2(t.position.x, t.position.y);
        return position;
    }
}
