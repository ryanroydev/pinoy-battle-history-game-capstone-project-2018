using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayerSpawner : MonoBehaviour {
	[SerializeField]
	private GameObject[] playerPrefabs;
	[SerializeField]
	private Transform spawnPosition;
	// Use this for initialization
	void Start () {
		SpawnPlayer ();

	}

	private void SpawnPlayer(){
		GameObject player = Instantiate (playerPrefabs[PlayerPrefs.HasKey("CharacterIndex")?PlayerPrefs.GetInt ("CharacterIndex"):0], spawnPosition.position, spawnPosition.rotation) as GameObject; 
	}
}
