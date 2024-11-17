using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionSpawn : MonoBehaviour {

	[SerializeField]
	private Vector3[] positions;
	// Use this for initialization
	void Start () {
		this.transform.position = positions [PlayerPrefs.GetInt ("CharacterIndex", 0)];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
