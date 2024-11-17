using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayer_PlayerScript : MonoBehaviour {

	private Vector3 playerStartPos;
	public Vector3 PlayerStartPos{get{ return playerStartPos;}set{ this.playerStartPos = value;}}
	private SinglePlayer_PlayerStats myPlayerStats;
	public SinglePlayer_PlayerStats MyPlayerStats{get{ return myPlayerStats;}set{ this.myPlayerStats = value;}}
	private SinglePlayer_PlayerAttack myPlayerAttack;
	private int targetIndex;
	public int TargetIndex{get{ return targetIndex;}set{ this.targetIndex = value;}}
	public delegate void DieDelegate();
	public event DieDelegate DieEvent;
	private bool isDead = false;
	private void OnEnable(){
		SinglePlayer_GameManager.Instance.RegisterPlayer (this.gameObject);

	}

	private void Start(){

		playerStartPos = this.transform.position;
		myPlayerAttack = GetComponent<SinglePlayer_PlayerAttack> ();
		myPlayerStats = GetComponent<SinglePlayer_PlayerStats> ();


	}
	private void Update(){
		checkIfDie ();
	}
	private void checkIfDie (){
		if (!isDead && myPlayerStats.CurrentHealth <= 0) {
			
			if (DieEvent != null) {
				DieEvent ();
			}
			isDead = true;
		}
	}

}
