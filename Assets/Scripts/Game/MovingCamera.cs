using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCamera : MonoBehaviour {
    public Vector3 endPoint;
    public float moveSpeed = 0.0001f;

    public bool isPaused = false;
	// Use this for initialization
	void Start () {
        endPoint = new Vector3(0.0f, 5.0f, -10f);
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (!isPaused && transform.position.y != endPoint.y)
        {
            transform.position = Vector3.LerpUnclamped(transform.position, endPoint, moveSpeed * Time.fixedDeltaTime);
        }
	}

    public void SetPosition(Vector2 newPos)
    {
        transform.position = newPos;
    }

}
