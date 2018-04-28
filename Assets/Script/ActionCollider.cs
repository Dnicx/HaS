using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCollider : MonoBehaviour {

    private GameObject interactObj;
    private bool isAction;

	// Use this for initialization
	void Start () {
        isAction = false;
        interactObj = null;
	}
	
	// Update is called once per frame
	void Update () {
		if(interactObj != null)
        {
            InteractabltObj obj = interactObj.GetComponent<InteractabltObj>();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                isAction = true;
                obj.actionDirection = this.transform.forward.normalized;
                obj.action();
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                isAction = false;
                obj.noAction();
            }
        }
        else
        {
            isAction = false;
        }
	}

    public bool IsAction()
    {
        return isAction;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Interact Obj"))
        {
            interactObj = other.gameObject;
            
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Interact Obj"))
        {
            InteractabltObj obj = interactObj.GetComponent<InteractabltObj>();
            obj.noAction();
            interactObj = null;
        }
    }
}
