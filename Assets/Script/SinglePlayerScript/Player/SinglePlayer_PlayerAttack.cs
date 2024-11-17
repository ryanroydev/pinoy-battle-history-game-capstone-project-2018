using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayer_PlayerAttack : MonoBehaviour {
	
	[SerializeField]
	private bool canImproveAttack;
	[SerializeField]private GameObject attackParticles;
	[SerializeField]private float attackRange;
	private  float walkSpeed = 5f;
	private bool isAttack = false;
	private SinglePlayer_PlayerScript myPlayerScript;
	private bool isAttackAnimating = false;
	private Animator myAnimator;
	// Use this for initialization
	void Start () {
		myPlayerScript = GetComponent<SinglePlayer_PlayerScript> ();
		myAnimator = GetComponent<Animator> ();
	}
	public void Attack(){
		if (!isAttack && !SinglePlayer_GameManager.Instance.IsGameOver) {
			StartCoroutine (attackEnemy ());
		}
	}
	IEnumerator attackEnemy(){

		isAttack = true;
		//preparing to attack set  camera in  battle mode
		SinglePlayer_GameManager.Instance.SinglePlayerCameraMovementScript.SetCameraBattleMode (true);
		while (!SinglePlayer_GameManager.Instance.SinglePlayerCameraMovementScript.IsCameraInBattlePosition ()) {
			yield return new WaitForSeconds (Time.deltaTime);
		}
		float delayTime = 0.1f;
		//delay  1  seconds 
		yield return new WaitForSeconds (delayTime);
		myAnimator.SetFloat ("Speed", 1f);
		//maglakad hanggang  makalapit sa monster

		float maxRunTime = 5f;
		float currentRunTime = 0f;
		while (Vector3.Distance (this.transform.position, SinglePlayer_GameManager.Instance.PlayerAttackEnemyPos [myPlayerScript.TargetIndex].transform.position) >= attackRange) {
			
			this.transform.position = Vector3.MoveTowards (this.transform.position, SinglePlayer_GameManager.Instance.Enemies [myPlayerScript.TargetIndex].transform.position, walkSpeed * Time.deltaTime);

			yield return new WaitForSeconds (Time.deltaTime);
			currentRunTime += Time.deltaTime;
			//Debug.Log (currentRunTime);
			if (currentRunTime >= maxRunTime) {
				//Debug.Log ("Run out of time!!");
				break;
			}

		}

		myAnimator.SetFloat ("Speed", 0f);

		//once na makalapit aatack na
		yield return new WaitForSeconds (0.1f);

		int currentLevel = PlayerPrefs.GetInt ("PlayerLevel", 1);
		Debug.Log (currentLevel);
		if (canImproveAttack) {
			if (currentLevel >= 11 && currentLevel <= 20) {
				myAnimator.SetTrigger ("Attack2");

			} else if (currentLevel >= 21) {
				myAnimator.SetTrigger ("Attack3");

			} else {
				myAnimator.SetTrigger ("Attack");

			}
		} else {
			myAnimator.SetTrigger ("Attack");
		}
		isAttackAnimating = true;
		//wait unitil attack animation is over
		while (isAttackAnimating) {
			yield return new WaitForSeconds (Time.deltaTime);
			if (SinglePlayer_GameManager.Instance.Enemies.Count == 0) {
				yield return new WaitForSeconds (0.5f);
				OutOfEnemy ();
				DisableAttackAnimation ();
			}
		}
	

		myAnimator.SetFloat ("Speed", 1f);
		while (Vector3.Distance (this.transform.position, myPlayerScript.PlayerStartPos) >= 0.5f) {

			this.transform.position = Vector3.MoveTowards (this.transform.position, myPlayerScript.PlayerStartPos, walkSpeed * Time.deltaTime);

			yield return new WaitForSeconds (Time.deltaTime);
		}

		myAnimator.SetFloat ("Speed", 0f);

		//delay  1  seconds 
		yield return new WaitForSeconds (delayTime);
		//set camera position to normal position
		SinglePlayer_GameManager.Instance.SinglePlayerCameraMovementScript.SetCameraBattleMode (false);
		while (SinglePlayer_GameManager.Instance.SinglePlayerCameraMovementScript.IsCameraInBattlePosition ()) {
			yield return new WaitForSeconds (Time.deltaTime);
		}
		isAttack = false;
		if (!SinglePlayer_GameManager.Instance.IsGameOver) {
			SinglePlayer_GameManager.Instance.ShowQuizPanel (true);
		}
	}
	public void OutOfEnemy(){
		myAnimator.SetBool ("OutOfEnemy", true);
		//Debug.Log ("Out Of enemy");
	}
	public void AttackEventAnimation(){
		SinglePlayer_GameManager.Instance.AttackSoundPlay ();
		attackParticles.SetActive (false);
		bool CriticalDmg = (Random.Range (0, 100) >= 50);
		if (SinglePlayer_GameManager.Instance.Enemies.Count != 0) {
			SinglePlayer_GameManager.Instance.Enemies [myPlayerScript.TargetIndex].GetComponent<SinglePlayer_EnemyStats> ().TakeDamage (CriticalDmg ? myPlayerScript.MyPlayerStats.AttackDamage * 2 : myPlayerScript.MyPlayerStats.AttackDamage, CriticalDmg);
			attackParticles.SetActive (true);
		}
		//ADDeXPI
		//SinglePlayer_GameManager.Instance.MyReward.AddExpi (myPlayerScript.MyPlayerStats.AttackDamage);

	}
	public void DisableAttackAnimation(){
		attackParticles.SetActive (false);
		isAttackAnimating = false;
		//Debug.Log ("Disabled attack Animation");
	}
}
