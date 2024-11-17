using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayer_EnemyAttack : MonoBehaviour {
	[SerializeField]private float attackRange;
	private  float walkSpeed = 5f;
	private bool isAttackAnimating = false;
	private Animator  myAnimator;
	private bool isAttack = false;
	private SinglePlayer_EnemyScript myEnemyScript;
	void Start(){
		myAnimator = GetComponent<Animator> ();

		myEnemyScript = GetComponent<SinglePlayer_EnemyScript> ();
	}

	public void Attack(){
		if (!isAttack && !SinglePlayer_GameManager.Instance.IsGameOver)
			StartCoroutine (attackPlayer ());
	}

	IEnumerator attackPlayer(){
		isAttack = true;

		SinglePlayer_GameManager.Instance.SinglePlayerCameraMovementScript.SetCameraBattleMode (true);
		while (!SinglePlayer_GameManager.Instance.SinglePlayerCameraMovementScript.IsCameraInBattlePosition ()) {
			yield return new WaitForSeconds (Time.deltaTime);
		}
		float delayTime = 0.1f;
		//delay  1  seconds 
		yield return new WaitForSeconds (delayTime);
		//magaakad hanggang makalapit ung monster sa player
		myAnimator.SetFloat ("Speed", 1);

		//while (SinglePlayer_GameManager.Instance.EnemyAttackPlayerPos.transform.position != this.transform.position) {
		while (Vector3.Distance (this.transform.position, SinglePlayer_GameManager.Instance.Player.transform.position) >= attackRange) {
			this.transform.position = Vector3.MoveTowards (this.transform.position, SinglePlayer_GameManager.Instance.Player.transform.position, walkSpeed * Time.deltaTime);

			yield return new WaitForSeconds (Time.deltaTime);
		}
		myAnimator.SetFloat ("Speed", 0f);

		yield return new WaitForSeconds (0.1f);


		myAnimator.SetTrigger ("Attack");
		isAttackAnimating = true;

		//wait until Attack ANimation is over
		while (isAttackAnimating)
			yield return new WaitForSeconds (Time.deltaTime);

		myAnimator.SetFloat ("Speed", 1f);
		//while (SinglePlayer_GameManager.Instance.EnemyStartPos.position != this.transform.position) {
		while (Vector3.Distance (this.transform.position, myEnemyScript.EnemyStartPos) >= 0.1f) {	
			this.transform.position = Vector3.MoveTowards (this.transform.position, myEnemyScript.EnemyStartPos, walkSpeed * Time.deltaTime);

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

	}

	public bool IsEnemyAttack(){
		if (isAttack) {
			return true;
		}
		return false;
	}
	public void AttackEventAnimation(){
		int critical = Random.Range (1, 100);
		int criticalChance = 50;//50% chance
		bool isCritical = (critical >= criticalChance);
		SinglePlayer_GameManager.Instance.Player.GetComponent<SinglePlayer_PlayerStats> ().TakeDamage (isCritical ? myEnemyScript.MyEnemyStats.AttackDamage * 2 : myEnemyScript.MyEnemyStats.AttackDamage,isCritical);
		float floatAddedDmg = myEnemyScript.MyEnemyStats.AttackDamage * 0.1f;
		int addedDmg = Mathf.FloorToInt (floatAddedDmg);

		myEnemyScript.MyEnemyStats.AttackDamage = myEnemyScript.MyEnemyStats.AttackDamage + addedDmg;
	
	}
	public void DisableAttackAnimation(){
		isAttackAnimating = false;
		//Debug.Log ("Disabled attack Animation");
	}
}
