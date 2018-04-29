using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitArea : MonoBehaviour {

	[SerializeField] private playerStatus otherPlayer;

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Prey" || other.gameObject.tag == "Predator")
		if (other.gameObject.tag != gameObject.tag) {
			otherPlayer = other.gameObject.GetComponent<playerStatus>();
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Prey" || other.gameObject.tag == "Predator")
		if (other.gameObject.tag != gameObject.tag) {
			otherPlayer = null;
		}
	}

	public void Hit() {
		Debug.Log("hitting");
		if (otherPlayer != null) 
			otherPlayer.Hit();
	}
}
