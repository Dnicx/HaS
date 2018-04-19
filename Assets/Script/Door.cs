using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    private bool isAction;
    private float rotation;

	// Use this for initialization
	void Start () {
        isAction = false;
        rotation = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Return)){
            isAction = true;
        }
        if (isAction)
        {
           rotation = Input.GetAxis("Mouse Y") * 5;


        }
    }

    private void FixedUpdate()
    {
        transform.Rotate(0, rotation, 0);
        print(transform.rotation.eulerAngles);
        if(transform.eulerAngles.y < 225 && transform.eulerAngles.y > 135)
        {
           transform.eulerAngles = new Vector3(0,225,0);
        }
        else if(transform.eulerAngles.y > 0 && transform.eulerAngles.y < 90)
        {
           transform.eulerAngles = new Vector3(0, 0, 0);
        }
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
