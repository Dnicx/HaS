using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CharacterControllerScript : NetworkBehaviour {

	private CharacterController characterController;
	[SerializeField] private float speed;
	[SerializeField] private float gravity;
	[SerializeField] private float crouchFactor;
	private float fallingSpeed;
	private Vector3 moveDirection;
	private float x, z;
	private Animator anim;
	private bool standable;
	[SerializeField] private Camera playerCam;
	private Camera mainCam;
	private Camera otherCam;

    [SerializeField] private ActionCollider actionCollider;

	private float height;
	private float look;

	// Use this for initialization
	void Start () {
		characterController = GetComponent<CharacterController>();
		anim = GetComponent<Animator>();
		height = characterController.height;
		characterController.center = new Vector3(characterController.center.x, height/2, characterController.center.z);
		standable = true;
		mainCam = Camera.main;
        //actionCollider = mainCam.GetComponent<ActionCollider>();
        if (mainCam != null) {
            mainCam.gameObject.SetActive(false);
        }

	}
	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer) {
			otherCam = GetComponentInChildren<Camera>();
			if (otherCam != null) 
				otherCam.gameObject.SetActive(false);
			return;
		}

		x = Input.GetAxis("Vertical");		
		z = Input.GetAxis("Horizontal");

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
		
		fallingSpeed += gravity;
		if (characterController.isGrounded) fallingSpeed = 0;
        
		

        if (!actionCollider.IsAction())
        {
            Move();
            Rotate();
        }
	}

    private void Move()
    {
        moveDirection = new Vector3(z, 0, x);
        moveDirection = transform.TransformDirection(moveDirection) * speed * Time.deltaTime;
        moveDirection.y -= fallingSpeed * Time.deltaTime;
        characterController.Move(moveDirection);
    }

    private void Rotate()
    {
        transform.Rotate(new Vector3(0, PlayerSetting.CAMERA_SPEED * Input.GetAxis("Mouse X"), 0));
        look -= Input.GetAxis("Mouse Y");
        look = look > 90 ? 90 : look;
        look = look < -90 ? -90 : look;
        playerCam.transform.rotation = Quaternion.Euler(new Vector3(look, transform.localEulerAngles.y, transform.rotation.eulerAngles.z));
    }

    private void OnTriggerStay(Collider other) {
		if (other != this) standable = false;
	}
	private void OnTriggerExit(Collider other) {
		standable = true;
	}

	private void OnDestroy() {
		if (mainCam != null) mainCam.gameObject.SetActive(true);
	}

	
}
