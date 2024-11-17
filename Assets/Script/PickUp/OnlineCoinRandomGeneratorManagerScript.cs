using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class OnlineCoinRandomGeneratorManagerScript : NetworkBehaviour {

	int spawnNum;
	int currentSpawnNum;
	float spawnTimer;
	float randomTime = 10f;
	[SerializeField]private Transform[] spawnPos;
	[SerializeField]private GameObject[] potions;
	[SerializeField]private GameObject pisoCoin;
	// Use this for initialization
	void OnEnable () {
		spawnNum = Random.Range (10, 30);
		Debug.Log (spawnNum);
	}

	// Update is called once per frame

	void Update () {
		
		if (isServer) {
			if (currentSpawnNum <= spawnNum) {
				spawnTimer += Time.deltaTime;
				if (spawnTimer >= randomTime) {
					spawnTimer = 0;
					randomTime = Random.Range (10, 25);
					if (65 >= Random.Range (1, 100)) {
						spawnCoin (Random.Range (1, 2));
					} else {
						spawnPotion ();
					}
				}
			}
		}
	}
	[Server]
	void spawnCoin(int volume){


		int randomInt = Random.Range (0, spawnPos.Length);
		GameObject piso = Instantiate (pisoCoin, spawnPos [randomInt].position, pisoCoin.transform.rotation) as GameObject;
		NetworkServer.Spawn (piso);
		currentSpawnNum++;
		piso.GetComponent<SpawnUniqueId> ().ObjectID = "Coin" + currentSpawnNum;
		Debug.Log (currentSpawnNum);
	}
	[Server]
	void spawnPotion(){
		int randomInt = Random.Range (0, spawnPos.Length);
		int randomPotion = Random.Range (0, potions.Length);
		GameObject potion = Instantiate (potions [randomPotion], spawnPos [randomInt].position, potions [randomPotion].transform.rotation) as GameObject;
		NetworkServer.Spawn (potion);
		currentSpawnNum++;
		potion.GetComponent<SpawnUniqueId> ().ObjectID = "Potion" + currentSpawnNum;
	}
}
