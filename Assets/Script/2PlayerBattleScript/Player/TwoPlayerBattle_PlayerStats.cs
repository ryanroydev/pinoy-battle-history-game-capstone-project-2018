using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class TwoPlayerBattle_PlayerStats : NetworkBehaviour {
	private float DamageTimer;
	private float ArmorTimer;
	[SerializeField]
	private GameObject healthEffects;
	[SerializeField]
	private GameObject DamageEffects;
	[SerializeField]
	private GameObject ArmorEffects;

	private bool statStored = false;
	private int previousHealth;
	[SyncVar]
	private bool IsMiss = false;
	[SyncVar(hook="OnCurrentHealthChange")]
	private int currentHealth;
	[SyncVar(hook="OnMaxHealthChange")]
	private int maxHealth;
	[SyncVar]
	private int defense;
	[SyncVar]
	private int attackDamage;
	[SyncVar(hook="OnCurrentTropChange")]
	private int tropInt;
	private Animator myAnimator;
	public int CurrentHealth{get{ return currentHealth;}set{ this.currentHealth = value;}}
	public int MaxHealth{get{ return maxHealth;}set{ this.maxHealth = value;}}
	public int AttackDamage{get{ return attackDamage;}set{ this.attackDamage = value;}}
	public int Defense{get{ return defense;}set{ this.defense = value;}}
	//public override void PreStartClient ()
	//{
	//	base.PreStartClient ();
	
	//	StorePlayerStats ();
	//	if (!isLocalPlayer) {
	//		opponentReady = true;
		
	//		if (playerReady || opponentReady) {
	//			Debug.Log ("Client");
	//			initializeHealthBar ();
	//		}
	//	}
	//}


	public override void PreStartClient ()
	{
		base.PreStartClient ();
		StartCoroutine (InitializeBars ());
		myAnimator = GetComponent<Animator> ();

	}
	IEnumerator InitializeBars(){
		


		for (;;) {
			yield return new WaitForSeconds (Time.deltaTime);
			StorePlayerStats ();
			CheckPotionTimer ();
			//initializeHealthBar ();
			if (isLocalPlayer) {
				if (Input.GetKeyDown (KeyCode.A)) {
					HealthPotion ();
				}
				if (Input.GetKeyDown (KeyCode.S)) {
					DamagePotion ();
				}
				if (Input.GetKeyDown (KeyCode.D)) {
					ArmorPotion ();
				}
			}
		}
	}
	void OnCurrentTropChange(int curentTrp){
		tropInt = curentTrp;
		initializeHealthBar ();
	}

	void OnCurrentHealthChange(int curentHth){
		currentHealth = curentHth;
		initializeHealthBar ();
	}
	void OnMaxHealthChange(int maxtHth){
		maxHealth = maxtHth;
		initializeHealthBar ();
	}

	public void initializeHealthBar(){
		if (Custom_NetworkLobbyManager._LMSingleton.PlayerHealthBar != null) {
			if (isLocalPlayer) {
				
				Custom_NetworkLobbyManager._LMSingleton.PlayerHealthBar.MaxValue = maxHealth;
				Custom_NetworkLobbyManager._LMSingleton.PlayerHealthBar.Value = Mathf.Clamp (currentHealth, 0, maxHealth);
				Custom_NetworkLobbyManager._LMSingleton.SetPlayerTrophyImage (tropInt);

			} else {

				Custom_NetworkLobbyManager._LMSingleton.OpponentHealthBar.MaxValue = maxHealth;
				Custom_NetworkLobbyManager._LMSingleton.OpponentHealthBar.Value = Mathf.Clamp (currentHealth, 0, maxHealth);
				Custom_NetworkLobbyManager._LMSingleton.SetOpponentTrophyImage (tropInt);

			}
		}
	}
	public void TakeDamage(int dmg,bool isMiss,bool isCrit){


		IsMiss = isMiss;
		dmg -= defense;
		dmg = Mathf.Clamp (dmg, 0, int.MaxValue);
		if (!IsMiss) {
			
			currentHealth -= dmg;


			if (currentHealth <= 0) {
				//myAnimator.SetTrigger("Die");

				RpcTellClientDmgAnimations (isCrit ?  dmg.ToString () + " 2xCrit" : dmg.ToString(), "Die");
			} else {
			
				RpcTellClientDmgAnimations (isCrit ?  dmg.ToString () + " 2xCrit" : dmg.ToString(), "Damage");
			}
		} else {
			RpcTellClientMissAnimations ();
		}
	}
	[ClientRpc]
	void RpcTellClientDmgAnimations(string dmg,string animString){
		if (isLocalPlayer) {
			StartCoroutine (Custom_NetworkLobbyManager._LMSingleton.TwoPlayerBattle_PlayerCameraScript.showDamage ());
		}
		FloatingTextManager.Instance.CreatePoppUpText (dmg, this.transform);
		myAnimator.SetTrigger (animString);
	

	}
	[ClientRpc]
	void RpcTellClientMissAnimations(){
		FloatingTextManager.Instance.CreatePoppUpText ("Miss", this.transform);
	}
	public void DamagePotion(){



		int Added = (PlayerPrefs.GetInt ("PlayerLevel", 1) - 1) * 2;
		int addedDmg = PlayerPrefs.GetInt ("AddedDamage", 0);

		if (PlayerPrefs.HasKey ("PlayerAttackDamage")) {
			attackDamage = PlayerPrefs.GetInt ("PlayerAttackDamage") + Added + addedDmg;
		} else {
			attackDamage = 10 + Added + addedDmg;
		}
		int addedDmgPotion = attackDamage * 3;
		Cmd_TellServerPlayerTripleDamage (attackDamage + addedDmgPotion, addedDmgPotion);
		DamageTimer = 30;

		
	}
	public void ArmorPotion(){



		int Added = (PlayerPrefs.GetInt ("PlayerLevel", 1) - 1) * 2;
		int addedArmor = PlayerPrefs.GetInt ("AddedArmor", 0);
		if (PlayerPrefs.HasKey ("PlayerDefense")) {
			defense = PlayerPrefs.GetInt ("PlayerDefense") + (PlayerPrefs.GetInt ("PlayerLevel", 1) - 1) + addedArmor;
		} else {
			defense = 1 + Added + addedArmor;
		}
		int addedArmorPotion = defense * 2;
		Cmd_TellServerPlayerDoubleArmor (defense + addedArmorPotion, addedArmorPotion);
	

		ArmorTimer = 30;


	}
	public void HealthPotion(){
		if (isLocalPlayer) {
			float floatMaxHealth = maxHealth;
			float computedHealth = Mathf.Clamp (currentHealth + (floatMaxHealth * 0.20f), 0, MaxHealth);

			int intComputedHealth = Mathf.FloorToInt (computedHealth - currentHealth);
			Cmd_TellServerPlayerAddHealth (intComputedHealth);


		}

	}
	[ClientRpc]
	void RpcTellClientPotionEffects(string healthString,string DamageString,string ArmorString){
		if (healthString != "") {
			FloatingTextManager.Instance.CreatePoppUpHealthPotionText (healthString, this.transform);
		} 
		if (DamageString != "") {
			FloatingTextManager.Instance.CreatePoppUpDamagePotionText (DamageString, this.transform);
		}
		if (ArmorString != "") {
			FloatingTextManager.Instance.CreatePoppUpArmorPotionText (ArmorString, this.transform);
		}

	}

	private void StorePlayerStats(){
		if (isLocalPlayer) {


			if (!statStored) {
				int Added = (PlayerPrefs.GetInt ("PlayerLevel", 1) - 1) * 2;
				
				int addedDmg = PlayerPrefs.GetInt ("AddedDamage", 0);

				int	totalDamage = PlayerPrefs.GetInt ("PlayerAttackDamage", 10) + addedDmg + Added;
				
				int addedArmor = PlayerPrefs.GetInt ("AddedArmor", 0);

				int totalArmor = PlayerPrefs.GetInt ("PlayerDefense", 1) + addedArmor + (PlayerPrefs.GetInt ("PlayerLevel", 1) - 1);

                
				
				int addedStr = PlayerPrefs.GetInt ("AddedStregth",0);
				int totalStr = PlayerPrefs.GetInt ("PlayerStregth", 1) + addedStr + (PlayerPrefs.GetInt ("PlayerLevel", 1) - 1);
				int tmpmaxHealth = totalStr * 50;
				int tmpcurrentHealth = tmpmaxHealth;
				int TrophyIndex = PlayerPrefs.GetInt ("Trophy", 0);
				Cmd_TellServerPlayerStats (tmpcurrentHealth, tmpmaxHealth, totalDamage, totalArmor,TrophyIndex);
				if (currentHealth > 0) {
					statStored = true;
				}
			}
		}
	}
	[Command]
	void Cmd_TellServerPlayerStats(int tmpcurrentHealth,int tmpmaxHealth,int dmg,int armor,int trophyIndex){
		tropInt = trophyIndex;
		currentHealth = tmpcurrentHealth;
		maxHealth = tmpmaxHealth;
		attackDamage = dmg;
		defense = armor;

		//RpcUpdateHealth (tmpcurrentHealth, tmpmaxHealth);
	}
	[Command]
	void Cmd_TellServerPlayerAddHealth(int tmpcurrentHealth){
		currentHealth += tmpcurrentHealth;
		RpcTellClientPotionEffects ("20% +" + tmpcurrentHealth + "hp", "", "");
		GameObject go = Instantiate (healthEffects, this.transform.position, this.transform.rotation) as GameObject;
		go.transform.SetParent (this.transform, true);
		NetworkServer.Spawn (go);
	}
	[Command]
	void Cmd_TellServerPlayerTripleDamage(int TmpTotalDmg,int TmpAddedDmg){
		
		attackDamage = TmpTotalDmg;
		if (TmpAddedDmg != 0) {
			RpcTellClientPotionEffects ("", "+" + TmpAddedDmg + " dmg", "");
		}
	}
	[Command]
	void Cmd_TellServerPlayerDoubleArmor(int TmpTotalDef,int TmpAddedDef){

		defense = TmpTotalDef;
		if (TmpAddedDef != 0) {
			RpcTellClientPotionEffects ("", "", "+" + TmpAddedDef + " armor");
		}
	}
	[Command]
	public void CmdTellServerPotionPicked (GameObject potion){
		
		if (GameObject.Find (potion.name)) {
			GameObject.Find (potion.name).GetComponent<Animator> ().SetTrigger ("PickUp");
			GameObject.Find (potion.name).GetComponent<OnlinePotionScript> ().IsUse = true;
		}
	}
	void CheckPotionTimer(){
		if (isLocalPlayer) {
			CheckDamagePotionTimer ();
			CheckArmorPotionTimer ();
		}


		//CheckArmorPotionTimer ();
	}
	void CheckArmorPotionTimer(){
		//float DamageTimerPercent = DamageTimer / 30 + 100;
		//float imageAlphaPercent = 255f * DamageTimerPercent;


		if (ArmorTimer > 0) {
			TwoPlayer_GameManager.Instance.PotionPanel.ArmorPotion.gameObject.SetActive (true);
			TwoPlayer_GameManager.Instance.PotionPanel.ArmorText.text = Mathf.FloorToInt (ArmorTimer).ToString ();

			ArmorTimer -= Time.deltaTime;
			CmdArmorEffect (true);


		} else {
			TwoPlayer_GameManager.Instance.PotionPanel.ArmorPotion.gameObject.SetActive (false);

			//setDefaultDamage
			int Added = (PlayerPrefs.GetInt ("PlayerLevel", 1) - 1) * 2;
			int addedArmor = PlayerPrefs.GetInt ("AddedArmor", 0);
			if (PlayerPrefs.HasKey ("PlayerDefense")) {
				defense = PlayerPrefs.GetInt ("PlayerDefense") + (PlayerPrefs.GetInt ("PlayerLevel", 1) - 1) + addedArmor;
			} else {
				defense = 1 + Added + addedArmor;
			}
			Cmd_TellServerPlayerDoubleArmor (defense, 0);

			CmdArmorEffect (false);


			//SetDefault Damage
		}
	}
	void CheckDamagePotionTimer(){
		//float DamageTimerPercent = DamageTimer / 30 + 100;
		//float imageAlphaPercent = 255f * DamageTimerPercent;


		if (DamageTimer > 0) {
			TwoPlayer_GameManager.Instance.PotionPanel.DamagePotion.gameObject.SetActive (true);
			TwoPlayer_GameManager.Instance.PotionPanel.DamageText.text = Mathf.FloorToInt (DamageTimer).ToString ();
		
			DamageTimer -= Time.deltaTime;
			CmdDamageEffect (true);
		

		} else {
			TwoPlayer_GameManager.Instance.PotionPanel.DamagePotion.gameObject.SetActive (false);

			//setDefaultDamage
			int Added = (PlayerPrefs.GetInt ("PlayerLevel", 1) - 1) * 2;
			int addedDmg = PlayerPrefs.GetInt ("AddedDamage", 0);

			if (PlayerPrefs.HasKey ("PlayerAttackDamage")) {
				attackDamage = PlayerPrefs.GetInt ("PlayerAttackDamage") + Added + addedDmg;
			} else {
				attackDamage = 10 + Added + addedDmg;
			}
			Cmd_TellServerPlayerTripleDamage (attackDamage, 0);
			CmdDamageEffect (false);
			//SetDefault Damage
		}
	}
	[Command]
	void CmdArmorEffect(bool isEnabled){
		RpcArmorEffect (isEnabled);
	}
	[ClientRpc]
	void RpcArmorEffect(bool isEnabled){
		ArmorEffects.SetActive (isEnabled);

	}
	[Command]
	void CmdDamageEffect(bool isEnabled){
		RpcDamageEffect (isEnabled);
	}
	[ClientRpc]
	void RpcDamageEffect(bool isEnabled){
		DamageEffects.SetActive (isEnabled);

	}
}
