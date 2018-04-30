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
                Vector2 vec1 = new Vector2(startDirection.x, startDirection.z);
                Vector2 vec2 = new Vector2(actionDirection.x, actionDirection.z);

                Vector2 vec1Rotated90 = new Vector2(-vec1.y, vec1.x);
                float sign = (Vector2.Dot(vec1Rotated90, vec2) < 0) ? -1.0f : 1.0f;
                float angle = Vector2.Angle(vec1, vec2) * sign;

                if (angle <= 90 && angle >= -90)
                {

                    changeDirection = -1;
                }
            }
            
            rotation -= Input.GetAxis("Mouse Y") * 10 * changeDirection;
            rotation -= Input.GetAxis("ZVertical") * 10 * changeDirection;
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
