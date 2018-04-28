using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractabltObj : MonoBehaviour {
    protected bool isAction;
    public Vector3 actionDirection;

    public void action()
    {
        isAction = true;
    }

    public void noAction()
    {
        isAction = false;
    }
}
