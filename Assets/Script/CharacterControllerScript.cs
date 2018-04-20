using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour {

	private CharacterController characterController;
	[SerializeField] private float speed;
	[SerializeField] private float gravity;
	[SerializeField] private float crouchFactor;
	[SerializeField] private float fallingSpeed;
	private Vector3 moveDirection;
	private float x, z;
	private Animator anim;
	[SerializeField] private bool standable;

	private float height;

	// Use this for initialization
	void Start () {
		characterController = GetComponent<CharacterController>();
		anim = GetComponent<Animator>();
		height = characterController.height;
		standable = true;
	}
	
	// Update is called once per frame
	void Update () {
		x = Input.GetAxis("Horizontal");		
		z = Input.GetAxis("Vertical");
		if (x != 0 || z != 0) {
			anim.SetBool("isWalk", true);
		} else {
			anim.SetBool("isWalk", false);
		}
		if (Input.GetAxisRaw("Crouch") > 0) {
			anim.SetBool("isCrouch", true);
			characterController.center = new Vector3(characterController.center.x, height*crouchFactor/2, characterController.center.z);
			characterController.height = height*crouchFactor;
		} else {
			if (standable) {
				anim.SetBool("isCrouch", false);
				characterController.center = new Vector3(characterController.center.x, height/2, characterController.center.z);
				characterController.height = height;
			}
		}
		transform.Rotate(new Vector3(0, PlayerSetting.CAMERA_SPEED * Input.GetAxis("Mouse X"), 0));
		fallingSpeed += gravity;
		if (characterController.isGrounded) fallingSpeed = 0;
		moveDirection = new Vector3(z, 0, -x);
		moveDirection = transform.TransformDirection(moveDirection) * speed * Time.deltaTime;
		moveDirection.y -= fallingSpeed * Time.deltaTime;
		characterController.Move(moveDirection);
	}

	private void OnTriggerStay(Collider other) {
		if (other != this) standable = false;
	}
	private void OnTriggerExit(Collider other) {
		standable = true;
	}

	
}
