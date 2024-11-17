using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class TwoPlayer_Identity : NetworkBehaviour {

	[SyncVar]public string PlayerUniqueName;
	private NetworkInstanceId playerNetId;
	private Transform myTransform;

	public override void OnStartLocalPlayer ()
	{
		GetNetIdentity ();
		SetIdentity ();
	}
	void Update(){
		for (int i = 0; i < Custom_NetworkLobbyManager._LMSingleton.spawnPrefabs.Count; i++) {
			GameObject go = Custom_NetworkLobbyManager._LMSingleton.spawnPrefabs [i];
			if (myTransform.name == "" || myTransform.name == go.name + "(Clone)") {
				SetIdentity ();
			}
		}

	}
	void Awake () {
		myTransform = this.transform;
	}
	[Client]
	private void GetNetIdentity (){
		playerNetId = GetComponent<NetworkIdentity> ().netId;
		CmdTellServerMyNetId (MakeUniqueName());
	}
	string MakeUniqueName(){
		string uniqueName = "Player" + playerNetId.ToString();
		return uniqueName;
	}
	[Command]
	private void CmdTellServerMyNetId(string name){
		PlayerUniqueName = name;
	}

	private void SetIdentity (){
		if (!isLocalPlayer) {
			myTransform.name = PlayerUniqueName;
		} else {
			myTransform.name = MakeUniqueName ();
		}
	
	}
}
