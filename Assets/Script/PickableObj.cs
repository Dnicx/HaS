using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObj : InteractabltObj {

    public void Keep()
    {
        Collider collider = (GetComponent<Collider>());

        this.gameObject.SetActive(false);
    }

    public void Pick()
    {
        Collider collider = (GetComponent<Collider>());
        collider.enabled = false;
        this.gameObject.SetActive(true);
    }

    public void ThrowObj()
    {
        Collider collider = (GetComponent<Collider>());
        collider.enabled = true;
        this.GetComponent<Rigidbody>().velocity = new Vector3(transform.forward.normalized.x,transform.forward.normalized.y+0.1f,transform.forward.normalized.z) * 30;
        print(this.GetComponent<Rigidbody>().velocity);
    }
}
