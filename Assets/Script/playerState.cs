using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class playerState : NetworkBehaviour {

	public static readonly bool PREDATOR = true;
	public static readonly bool PREY = false;

	[SyncVar] private int health;
	private bool role;
	private bool dead = false;
	
	public int GetHealth() {
		return health;
	}

	public void Git() {
		if (!isServer)
			return;
		health -= 1;
		if (health <= 0) {
			dead = true;
		}
	}

	public bool IsDead() {
		return dead;
	}

	public void SelectRole(bool selectedRole) {
		role = selectedRole;
	}

	public bool IsPrey() {
		return !role;
	}
	
	public bool IsPredator() {
		return role;
	}

	public bool GetRole() {
		return role;
	}
	
}
