using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

	private float look = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		look -= Input.GetAxis("Mouse Y");
		look = look > 90 ? 90 : look;
		look = look < -90 ? -90 : look;
		transform.rotation = Quaternion.Euler(new Vector3(look, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
	}
}
