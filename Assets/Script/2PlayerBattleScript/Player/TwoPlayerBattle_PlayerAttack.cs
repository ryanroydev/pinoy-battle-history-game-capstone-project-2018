using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class TwoPlayerBattle_PlayerAttack : NetworkBehaviour {
	[SerializeField]
	private bool canImproveAttack;

	[SerializeField]private GameObject attackParticles;
	private bool isAttack = false;
	private bool yourAttackMiss = false;
	private TwoPlayerBattle_PlayerStats myStats;
	private TwoPlayerBattle_PlayerScript myScript;
	[SerializeField]private float attackRange;
	private  float walkSpeed = 5f;
	private Animator myAnimator;
	private bool isAttackAnimating = false;
	public override void OnStartLocalPlayer ()
	{
		myAnimator = GetComponent<Animator> ();

		myStats = GetComponent<TwoPlayerBattle_PlayerStats> ();
	}
	 void Start ()
	{
		myScript = GetComponent<TwoPlayerBattle_PlayerScript> ();
	}

	public void Attack(bool isMiss,bool TimesUp){
		if (!isLocalPlayer)
			return;
		
		if (myScript.YourTurn && !isAttack) {
			yourAttackMiss = isMiss;
			TwoPlayer_QuizManager qm = Custom_NetworkLobbyManager._LMSingleton.TwoPlayerQuizManager;
			if (!yourAttackMiss) {
				myScript.Cmd_CorrectAnswer (true, TimesUp, qm.currentQuestion.Choices [qm.currentQuestion.AnswerIndex]);
			} else {
				myScript.Cmd_CorrectAnswer (false, TimesUp, qm.currentQuestion.Choices [qm.currentQuestion.AnswerIndex]);
			}
			StartCoroutine (attackEnemy ());
		}
	}
	IEnumerator attackEnemy(){
		isAttack = true;
		Custom_NetworkLobbyManager._LMSingleton.TwoPlayerBattle_PlayerCameraScript.SetCameraBattleMode (true);

		while (!Custom_NetworkLobbyManager._LMSingleton.TwoPlayerBattle_PlayerCameraScript.IsCameraInBattlePosition ()) {
			yield return new WaitForSeconds (Time.deltaTime);
		
		}

		float delayTime = 0.1f;
		//delay  1  seconds 
		yield return new WaitForSeconds (delayTime);
		myAnimator.SetFloat ("Speed", 1f);
		//maglakad hanggang  makalapit sa monster
		Transform TargetPos=Custom_NetworkLobbyManager._LMSingleton.TargetPlayer.transform;
		float maxRunTime = 5f;
		float currentRunTime = 0f;
		while (Vector3.Distance (this.transform.position,TargetPos.position ) >= attackRange) {
				
			this.transform.position = Vector3.MoveTowards (this.transform.position, TargetPos.position, walkSpeed * Time.deltaTime);

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

			//Debug.Log ("Attacking");
		}


		myAnimator.SetFloat ("Speed", 1f);

	
		Vector3 startPos = Custom_NetworkLobbyManager._LMSingleton.characterSpawnPos [isServer ? 0 : 1];
		while (Vector3.Distance (this.transform.position,startPos ) >= 0.5f) {

			this.transform.position = Vector3.MoveTowards (this.transform.position, startPos, walkSpeed * Time.deltaTime);

			yield return new WaitForSeconds (Time.deltaTime);
		}

		myAnimator.SetFloat ("Speed", 0f);

		//delay  1  seconds 
		yield return new WaitForSeconds (delayTime);
		//Custom_NetworkLobbyManager._LMSingleton.TwoPlayerBattle_PlayerCameraScript.SetCameraBattleMode (false);
		isAttack = false;
		myScript.CmdSetTurn (false, "");
		myScript.CmdSetTurn (true, Custom_NetworkLobbyManager._LMSingleton.TargetPlayer.name);//set other player turn

	}
	public void OutOfEnemy(){
		myAnimator.SetBool ("OutOfEnemy", true);
		//Debug.Log ("Out Of enemy");
	}
	public void AttackEventAnimation(){
		if (isLocalPlayer) {
			
			string TargetName = Custom_NetworkLobbyManager._LMSingleton.TargetPlayer.name;
			bool CriticalDmg = (Random.Range (0, 100) >= 50);
			CmdTellServerWhoWasAttack (TargetName, CriticalDmg ? myStats.AttackDamage * 2 : myStats.AttackDamage, yourAttackMiss,CriticalDmg);
			if (!yourAttackMiss) {
				float floatAddedDmg = myStats.AttackDamage * 0.1f;
				int addedDmg = Mathf.FloorToInt (floatAddedDmg);

				myStats.AttackDamage = myStats.AttackDamage + addedDmg;
			}
		}

		//ADDeXPI

	}
	[Command]
	void CmdTellServerWhoWasAttack(string targetName,int dmg,bool isMiss,bool isCrit){
		RpcAttackParticles (true);
		RpcAttackParticles (isMiss);
		GameObject go = GameObject.Find (targetName);
		go.GetComponent<TwoPlayerBattle_PlayerStats> ().TakeDamage (dmg, isMiss, isCrit);
		if (go.gameObject.GetComponent<TwoPlayerBattle_PlayerStats> ().CurrentHealth <= 0) {
			myScript.Won (false);
		}
	}
	[ClientRpc]
	void RpcAttackParticles(bool isMiss){
		attackParticles.SetActive (!isMiss);
	}
	public void DisableAttackAnimation(){
		RpcAttackParticles (true);
		isAttackAnimating = false;


	}

}
