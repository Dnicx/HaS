using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    private bool isAction;
    private float rotation;
    private float startRotation;

	// Use this for initialization
	void Start () {
        isAction = false;
        startRotation = transform.eulerAngles.y;
        rotation = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Return)){
            isAction = true;
        }
        if (isAction)
        {
            rotation -= Input.GetAxis("Mouse Y") * 5;
            rotation = rotation < 0 ? 0 : rotation;
            rotation = rotation > 135 ? 135 : rotation;
        }
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, startRotation-rotation,0));
    }

    public void action()
    {
        isAction = true;
    }

    public void noAction()
    {
        isAction = false;
    }
}
