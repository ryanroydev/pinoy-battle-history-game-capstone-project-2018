using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflineSpawnUniqueId : MonoBehaviour {
	public string ObjectID;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		SetIdentity ();
	}
	void SetIdentity (){
		if (this.transform.name == "" || this.transform.name == "MagellanChest(Clone)" || this.transform.name == "YamahitaTreasure(Clone)" || this.transform.name == "ArmorPotion(Clone)"|| this.transform.name == "DamagePotion(Clone)"|| this.transform.name == "HealthPotion(Clone)") {
			this.transform.name = ObjectID;
		}
	}
}
