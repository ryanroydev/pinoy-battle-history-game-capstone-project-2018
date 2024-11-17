using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class SpawnUniqueId : NetworkBehaviour {
	[SyncVar]public string ObjectID;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		SetIdentity ();
	}
	void SetIdentity (){
		if (this.transform.name == "" || this.transform.name == "OnlineHealthPotion(Clone)" || this.transform.name == "OnlinePisoCoin(Clone)" || this.transform.name == "OnlineDamagePotion(Clone)") {
			this.transform.name = ObjectID;
		}
	}
}
