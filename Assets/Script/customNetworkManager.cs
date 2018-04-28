using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class customNetworkManager : NetworkManager {

	[SerializeField] GameObject playerPrefab2;

	private bool[] role;
	public static readonly short PREDATOR = 0;
	public static readonly short PREY = 1;

	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId) {
		if (!role[PREDATOR]){
			role[PREDATOR] = true;
			GameObject predatorPlayer = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
			NetworkServer.AddPlayerForConnection(conn, predatorPlayer, playerControllerId);
		}
		else {
			role[PREY] = true;
			GameObject preyPlayer = Instantiate(playerPrefab2, Vector3.zero, Quaternion.identity);
			NetworkServer.AddPlayerForConnection(conn, preyPlayer, playerControllerId);
		}
	}

	public override void OnStartClient(NetworkClient client) {
		ClientScene.RegisterPrefab(playerPrefab2);
	}

	public override void OnStartServer() {
		ClientScene.RegisterPrefab(playerPrefab2);
 		role = new bool[]{false, false};
	}

	

}
