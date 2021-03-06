﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObj : InteractabltObj {

    [SerializeField] private bool Holy;

    public void Keep()
    {
        Collider collider = (GetComponent<Collider>());
        collider.enabled = false;
        this.gameObject.SetActive(false);
    }

    public virtual void Pick()
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
        //Debug.Log(new Vector3(transform.forward.normalized.x, transform.forward.normalized.y, transform.forward.normalized.z) * 100);
        //GetComponent<Rigidbody>().AddForce(new Vector3(transform.forward.normalized.x, transform.forward.normalized.y+1f, transform.forward.normalized.z)*100);
        Debug.Log(GetComponent<Rigidbody>().velocity);
    }

    public bool isHoly() {
        return Holy;
    }
}
