using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour {

	private CharacterController characterController;
	[SerializeField] private float speed;
	[SerializeField] private float gravity;
	private Vector3 moveDirection = Vector3.zero;

	// Use this for initialization
	void Start () {
		characterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		float x = Input.GetAxis("Horizontal");		
		float z = Input.GetAxis("Vertical");
		transform.Rotate(new Vector3(0, PlayerSetting.CAMERA_SPEED * Input.GetAxis("Mouse X"), 0));
		moveDirection = new Vector3(z, characterController.isGrounded ? 0 : moveDirection.y, -x);
		// if (characterController.isGrounded) 
		print(characterController.isGrounded);
		moveDirection = transform.TransformDirection(moveDirection);
		moveDirection *= speed;
		moveDirection.y -= gravity * Time.deltaTime;
		characterController.Move(moveDirection * Time.deltaTime);
	}
}
