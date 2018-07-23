using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {
    public Transform target;
    public float horizontalMovement;
    public float verticalMovement;
    public Player player;
	
	// Update is called once per frame
    private void Awake() {
        target = player.transform;
    }

	void Update () {
		target = player.bodyRef.transform;
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
