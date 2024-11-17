using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayer_EnemyScript : MonoBehaviour {
	//reward for player who will kill me
	public int RewardCoin;
	public int RewardExpi;
	public int RewardScore;

	private Vector3 enemyStartPos;
	public Vector3 EnemyStartPos{get{ return enemyStartPos;}set{ this.enemyStartPos = value;}}
	private SinglePlayer_EnemyStats myEnemyStats;
	public SinglePlayer_EnemyStats MyEnemyStats{get{ return myEnemyStats;}set{this.myEnemyStats = value;}}
	private  SinglePlayer_EnemyAttack myEnemyAttack;
	public delegate void EnemyDieDelegate();
	public event EnemyDieDelegate EnemyDieEvent;
	private bool isDead = false;
	public GameObject EffectsTransform;
	void OnEnable(){
		EffectsTransform.SetActive (true);
	}
	void Start(){
		enemyStartPos = this.transform.position;
		myEnemyAttack = GetComponent<SinglePlayer_EnemyAttack> ();
		myEnemyStats = GetComponent<SinglePlayer_EnemyStats> ();
	}
	private void Update(){
		checkIfDie ();
	}
	public  void Attack(){
		myEnemyAttack.Attack ();
	}
	private void checkIfDie (){
		if (!isDead && myEnemyStats.CurrentHealth <= 0) {

			if (EnemyDieEvent != null) {
				EnemyDieEvent ();
			}
			isDead = true;
		}
	}
	public bool IsEnemyDead(){
		if (myEnemyStats.CurrentHealth <= 0) {
			return true;
		}
		return false;
	}
	}
