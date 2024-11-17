using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRandomGeneratorManagerScript : MonoBehaviour {
	int spawnNum;
	int currentSpawnNum;
	float spawnTimer;
	float randomTime = 10f;
	[SerializeField]private Transform[] spawnPos;
	[SerializeField]private GameObject pisoCoin,pickupItem;
	[SerializeField]private GameObject[] potions;

	// Use this for initialization
	void OnEnable () {
		spawnNum = Random.Range (15, 30);

	}
	
	// Update is called once per frame
	void Update () {
		if (currentSpawnNum <= spawnNum) {
			spawnTimer += Time.deltaTime;
			if (spawnTimer >= randomTime) {
				spawnTimer = 0;
				randomTime = Random.Range (10, 25);
				if (65 >= Random.Range (1, 100)) {
					spawnCoin (Random.Range (1, 2));
					//spawnItem ();
				} else if (95 >= Random.Range (1, 100)) {
					spawnPotion ();
				} else {
					spawnItem ();
				}
			}
		}
	}
	void spawnCoin(int volume){
		

	    int randomInt = Random.Range (0, spawnPos.Length);
		GameObject piso = Instantiate (pisoCoin, spawnPos [randomInt].position, pisoCoin.transform.rotation) as GameObject;
		currentSpawnNum++;
		//Debug.Log (currentSpawnNum);
	}
	void spawnPotion(){
		int randomInt = Random.Range (0, spawnPos.Length);
		int randomPotion = Random.Range (0, potions.Length);
		GameObject piso = Instantiate (potions [randomPotion], spawnPos [randomInt].position, potions [randomPotion].transform.rotation) as GameObject;
		currentSpawnNum++;
	}
	void spawnItem(){
		
		int randomInt = Random.Range (0, spawnPos.Length);
		Debug.Log (randomInt);
		GameObject pickedItem = Instantiate (pickupItem, spawnPos [randomInt].position, pickupItem.transform.rotation) as GameObject;
		//pickedItem.transform.position = spawnPos [randomInt].position;
		currentSpawnNum++;
	}

}
