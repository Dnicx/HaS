using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CharacterControllerScript : NetworkBehaviour {

	private CharacterController characterController;
	[SerializeField] private float walkSpeed;
	[SerializeField] private float runSpeed;
	[SerializeField] private float crouchSpeed;
	[SerializeField] private float proneSpeed;
	[SerializeField] private float gravity;
	[SerializeField] private float crouchFactor;
	[SerializeField] private float attackT;
	private float fallingSpeed;
	private Vector3 moveDirection;
	private float x, z;
	private Animator anim;
	[SerializeField] private Camera playerCam;
	private Camera mainCam;
	private Camera otherCam;
	[SerializeField] private GameObject hitBox;
	[SerializeField] private GameObject hurtBox;
	private playerStatus playerStat;

    [SerializeField] private ActionCollider actionCollider;

	private float height;
	private Vector3 camPos;
	private float look;

	private float t;

	//status part
	[SerializeField] private bool standable;
	private bool running;
	[SerializeField] private float currentSpeed;
	private bool attacking;
	private bool cooledDown = true;

	//serialize to debug
	[SerializeField] private bool localVisible = false;
	[SerializeField] private List<Renderer> model;

	// Use this for initialization
	void Start () {
		characterController = GetComponent<CharacterController>();
		anim = GetComponent<Animator>();
		height = characterController.height;
		characterController.center = new Vector3(characterController.center.x, height/2, characterController.center.z);
		standable = true;
		mainCam = Camera.main;
        actionCollider = playerCam.GetComponent<ActionCollider>();
		camPos = playerCam.transform.localPosition;
		playerStat = GetComponent<playerStatus>();

		if (mainCam != null) mainCam.gameObject.SetActive(false);
		for (int i = 0; i < transform.childCount; i++) {
			model.Add(transform.GetChild(i).GetComponent<SkinnedMeshRenderer>());
			if (isLocalPlayer && !localVisible && model[i] != null) model[i].shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
		}
		model.Clear();
		for (int i = 0; i < transform.childCount; i++) {
			model.Add(transform.GetChild(i).GetComponent<MeshRenderer>());
			if (isLocalPlayer && !localVisible && model[i] != null) model[i].shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
		}
		if (!isLocalPlayer) {	
			otherCam = GetComponentInChildren<Camera>();
			if (otherCam != null) 
				// otherCam.GetComponentInChildren<AudioListener>().enabled = false;
				otherCam.gameObject.SetActive(false);
			return;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer) return;
		if (playerStat.IsDead()) {
			anim.SetBool("isDead", true);
			return;
		}

        if(actionCollider.isItemHold() && Input.GetKeyDown(KeyCode.Mouse1))
        {
            actionCollider.ToggleHold();
        }
        if(actionCollider.isItemHold() && actionCollider.IsHold() && Input.GetKeyDown(KeyCode.Mouse0))
        {
            actionCollider.ThrowItemHold();
        }

		x = Input.GetAxis("Vertical");		
		z = Input.GetAxis("Horizontal");

		if (x != 0 || z != 0) {
			anim.SetBool("isWalk", true);
			if (Input.GetAxis("Sprint") > 0) {
				anim.SetBool("isRun", true);
				currentSpeed = runSpeed;
				running = true;
			}
			else {
				anim.SetBool("isRun", false);
				currentSpeed = walkSpeed;
				running = false;
			}
		} else {
			anim.SetBool("isWalk", false);
			anim.SetBool("isRun", false);
		}
		if (Input.GetAxisRaw("Crouch") > 0 && !attacking) {
			anim.SetBool("isCrouch", true);
			currentSpeed = crouchSpeed;
			characterController.center = new Vector3(characterController.center.x, height*crouchFactor/2, characterController.center.z);
			characterController.height = height*crouchFactor;
			if (t < 1) t += 4*Time.deltaTime;
			playerCam.transform.localPosition = Vector3.Lerp(camPos, camPos*crouchFactor,t);
		} else {
			if (standable) {
				anim.SetBool("isCrouch", false);
				characterController.center = new Vector3(characterController.center.x, height/2, characterController.center.z);
				characterController.height = height;
				if (t > 0) t -= 4*Time.deltaTime;
				playerCam.transform.localPosition = Vector3.Lerp(camPos, camPos*crouchFactor,t);
			}
		}
		if (cooledDown) {
			if (Input.GetAxis("Attack") > 0 && playerStat.isArmed()) {
				hitBox.GetComponent<hitArea>().Hit();
				anim.SetBool("isAttack", true);
				StartCoroutine(attackTime());
				StartCoroutine(cooldownTime());
				if (playerStat.IsPredator()) playerStat.unequip();
			}
		}
		else anim.SetBool("isAttack", false);
		
		fallingSpeed += gravity;
		if (characterController.isGrounded) fallingSpeed = 0;
        if (actionCollider==null || !actionCollider.IsAction())
		{
			if (!attacking) {
	            Move();
			}
			Rotate();
		}
	}

    private void Move()
    {
        moveDirection = new Vector3(z, 0, x);
        moveDirection = transform.TransformDirection(moveDirection) * currentSpeed * Time.deltaTime;
        moveDirection.y -= fallingSpeed * Time.deltaTime;
        characterController.Move(moveDirection);
    }

    private void Rotate()
    {
        transform.Rotate(new Vector3(0, PlayerSetting.CAMERA_SPEED * Input.GetAxis("Mouse X"), 0));
        look -= PlayerSetting.CAMERA_SPEED * Input.GetAxis("Mouse Y");
        look = look > 60 ? 60 : look;
        look = look < -90 ? -90 : look;
        playerCam.transform.rotation = Quaternion.Euler(new Vector3(look, transform.localEulerAngles.y, transform.rotation.eulerAngles.z));
    }

    private void OnTriggerStay(Collider other) {
		if (other.gameObject.tag != gameObject.tag) standable = false;
	}
	private void OnTriggerExit(Collider other) {
		standable = true;
	}

	private void OnDestroy() {
		if (mainCam != null) mainCam.gameObject.SetActive(true);
	}

	
	IEnumerator attackTime() {
		attacking = true;
		yield return new WaitForSeconds(attackT);
		attacking = false;
	}

	IEnumerator cooldownTime() {
		cooledDown = false;
		yield return new WaitForSeconds(attackT*2);
		cooledDown = true;
	}
}
