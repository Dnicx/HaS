using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class playerStatus : NetworkBehaviour {

	public static readonly bool PREDATOR = true;
	public static readonly bool PREY = false;

	[SyncVar] [SerializeField] private int health;
	[SerializeField] private bool predator; //isPredator?
	[SyncVar] [SerializeField] private bool dead = false;
	[SerializeField] private bool armed;
	private bool role;
	[SyncVar] public int end = 0;// 0 not, 1 pred, 2, prey

	private void Start() {
		role = predator;
	}
	
	public int GetHealth() {
		return health;
	}

	public void Hit() {
		if (!isServer)
			return;
		health -= 1;
		if (health <= 0) {
			dead = true;
			// if (predator) {
			// 	end = 2;
			// }
			// else {
			// 	end = 1;
			// }
		}
	}

	public bool IsDead() {
		return dead;
	}

	public void SelectRole(bool selectedRole) {
		role = selectedRole;
	}

	public bool IsPredator() {
		return role;
	}

	public void equip() {
		armed = true;
	}

	public void unequip() {
		armed = false;
	}

	public bool isArmed() {
		return armed;
	}
	
}
