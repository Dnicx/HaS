using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractabltObj {

    private float rotation;
    private float startRotation;
    private bool setStartDirection;
    private Vector3 startDirection;
    private int changeDirection;
    private bool CW;

	// Use this for initialization
	void Start () {
        isAction = false;
        startRotation = transform.eulerAngles.y;
        startDirection = transform.forward.normalized;
        rotation = 0;
        changeDirection = -1;
	}
	
	// Update is called once per frame
	void Update () {
        if (isAction)
        {

            if (setStartDirection)
            {
                Physics.IgnoreLayerCollision(8, 9);
                startDirection = transform.forward.normalized;
                setStartDirection = false;
                Vector2 tmp = new Vector2(startDirection.x, startDirection.z);
                changeDirection = 1;
                if (((startDirection.x >= 0 && startDirection.z > 0) && (actionDirection.x >= startDirection.x || actionDirection.z >= startDirection.z)) ||
                    ((startDirection.x > 0 && startDirection.z <= 0) && (actionDirection.x >= startDirection.x || (actionDirection.x < startDirection.x && actionDirection.y <= startDirection.z))) ||
                    ((startDirection.x <= 0 && startDirection.z > 0) && (actionDirection.z >= startDirection.z || (actionDirection.z < startDirection.z && actionDirection.x <= startDirection.x))) ||
                    ((startDirection.x < 0 && startDirection.z <= 0) && (actionDirection.z <= startDirection.z || actionDirection.x <= startDirection.x)))
                {
                    changeDirection = -1;
                }
            }
            
            rotation -= Input.GetAxis("Mouse Y") * 10 * changeDirection;
            rotation = rotation < 0 ? 0 : rotation;
            rotation = rotation > 135 ? 135 : rotation;
        }
        else
        {
            setStartDirection = true;
            Physics.IgnoreLayerCollision(8, 9, false);
        }
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, startRotation-rotation,0));
    }
}
