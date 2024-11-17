using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SinglePlayer_PlayerStats : MonoBehaviour {
	private float DamageTimer;
	private float ArmorTimer;
	[SerializeField]
	private GameObject healthEffects;
	[SerializeField]
	private GameObject DamageEffects;
	[SerializeField]
	private GameObject ArmorEffects;
	[SerializeField]
	private Bar healthBar;
	private int previousHealth;

	private int currentHealth;
	private int maxHealth;
	private int attackDamage;
    private int defense;
	private int currentLevel;
	private int currentExpi;
	private Animator myAnimator;
	public int CurrentHealth{get{ return currentHealth;}set{ this.currentHealth = value;}}
	public int MaxHealth{get{ return maxHealth;}set{ this.maxHealth = value;}}
	public int AttackDamage{get{ return attackDamage;}set{ this.attackDamage = value;}}
	public int Defense{get{ return defense;}set{ this.defense = value;}}
	// Use this for initialization
	void Start () {
		if (GameObject.Find ("heathbar")) {
			healthBar = GameObject.Find ("heathbar").GetComponent<Bar> ();
		}
		initializeHealthBar ();
		StorePlayerStats ();
		myAnimator = GetComponent<Animator> ();
	}
	void Update(){
		if (GameObject.Find ("heathbar") && healthBar == null) {
			healthBar = GameObject.Find ("heathbar").GetComponent<Bar> ();
		}
		initializeHealthBar ();
		if (Input.GetKeyDown (KeyCode.A)) {

			ArmorPotion ();
		}
		if (Input.GetKeyDown (KeyCode.S)) {
			DamagePotion ();
		}
		CheckPotionTimer ();
	}
	private void initializeHealthBar(){
		if (healthBar != null && previousHealth != currentHealth) {
			healthBar.MaxValue = maxHealth;
			healthBar.Value = Mathf.Clamp (currentHealth, 0, maxHealth);
			previousHealth = currentHealth;

		}
	}
	void CheckPotionTimer(){
		
		CheckDamagePotionTimer ();
		CheckArmorPotionTimer ();
	}
	void CheckDamagePotionTimer(){
		//float DamageTimerPercent = DamageTimer / 30 + 100;
		//float imageAlphaPercent = 255f * DamageTimerPercent;


		if (DamageTimer > 0) {
			SinglePlayer_GameManager.Instance.PotionPanel.DamagePotion.gameObject.SetActive (true);
			SinglePlayer_GameManager.Instance.PotionPanel.DamageText.text = Mathf.FloorToInt (DamageTimer).ToString ();
			if (SinglePlayer_GameManager.Instance.GameStart) {
				DamageTimer -= Time.deltaTime;
				DamageEffects.SetActive (true);
			}

		} else {
			SinglePlayer_GameManager.Instance.PotionPanel.DamagePotion.gameObject.SetActive (false);

			//setDefaultDamage
			int Added = (PlayerPrefs.GetInt ("PlayerLevel", 1) - 1) * 2;
			int addedDmg = PlayerPrefs.GetInt ("AddedDamage", 0);

			if (PlayerPrefs.HasKey ("PlayerAttackDamage")) {
				attackDamage = PlayerPrefs.GetInt ("PlayerAttackDamage") + Added + addedDmg;
			} else {
				attackDamage = 10 + Added + addedDmg;
			}
			DamageEffects.SetActive (false);
			//SetDefault Damage
		}
	}
	void CheckArmorPotionTimer(){
		//float ArmorTimerPercent = ArmorTimer / 30 + 100;
		//float imageAlphaPercent = 255f * ArmorTimerPercent;

		if (ArmorTimer > 0) {
			SinglePlayer_GameManager.Instance.PotionPanel.ArmorPotion.gameObject.SetActive (true);
			SinglePlayer_GameManager.Instance.PotionPanel.ArmorText.text = Mathf.FloorToInt (ArmorTimer).ToString ();
			if (SinglePlayer_GameManager.Instance.GameStart) {
				ArmorTimer -= Time.deltaTime;
				ArmorEffects.SetActive (true);
			}
		} else {
			SinglePlayer_GameManager.Instance.PotionPanel.ArmorPotion.gameObject.SetActive (false);

			//setDefaultArmor
			int Added = (PlayerPrefs.GetInt ("PlayerLevel", 1) - 1) * 2;
			int addedArmor = PlayerPrefs.GetInt ("AddedArmor", 0);
			if (PlayerPrefs.HasKey ("PlayerDefense")) {
				defense = PlayerPrefs.GetInt ("PlayerDefense") + (PlayerPrefs.GetInt ("PlayerLevel", 1) - 1) + addedArmor;
			} else {
				defense = 1 + Added + addedArmor;
			}
			ArmorEffects.SetActive (false);
			//SetDefault Armor
		}
	}
	public void ArmorPotion(){
		if (!SinglePlayer_GameManager.Instance.IsGameOver) {
			int Added = (PlayerPrefs.GetInt ("PlayerLevel", 1) - 1) * 2;
			int addedArmor = PlayerPrefs.GetInt ("AddedArmor", 0);
			if (PlayerPrefs.HasKey ("PlayerDefense")) {
				defense = PlayerPrefs.GetInt ("PlayerDefense") + (PlayerPrefs.GetInt ("PlayerLevel", 1) - 1) + addedArmor;
			} else {
				defense = 1 + Added + addedArmor;
			}
			int addedArmorPotion = defense * 2;
			defense += addedArmorPotion;
			FloatingTextManager.Instance.CreatePoppUpArmorPotionText ("+" + addedArmorPotion + " Armor", this.transform);

			ArmorTimer = 30;
		}
	}
	public void DamagePotion(){

		if (!SinglePlayer_GameManager.Instance.IsGameOver) {
			
			int Added = (PlayerPrefs.GetInt ("PlayerLevel", 1) - 1) * 2;
			int addedDmg = PlayerPrefs.GetInt ("AddedDamage", 0);

			if (PlayerPrefs.HasKey ("PlayerAttackDamage")) {
				attackDamage = PlayerPrefs.GetInt ("PlayerAttackDamage") + Added + addedDmg;
			} else {
				attackDamage = 10 + Added + addedDmg;
			}
			int addedDmgPotion = attackDamage * 3;
			attackDamage += addedDmgPotion;
			FloatingTextManager.Instance.CreatePoppUpDamagePotionText ("+" + addedDmgPotion + " dmg", this.transform);
			DamageTimer = 30;

		}
	}
	public void HealthPotion(){
		if (!SinglePlayer_GameManager.Instance.IsGameOver) {
			float floatMaxHealth = maxHealth;
			float computedHealth = Mathf.Clamp (currentHealth + (floatMaxHealth * 0.20f), 0, MaxHealth);

			int intComputedHealth = Mathf.FloorToInt (computedHealth - currentHealth);
			currentHealth += intComputedHealth;
			FloatingTextManager.Instance.CreatePoppUpHealthPotionText ("20% +" + intComputedHealth + "hp", this.transform);
			GameObject go = Instantiate (healthEffects, this.transform.position, this.transform.rotation) as GameObject;
			go.transform.SetParent (this.transform, true);
	
		}
	}
	public void TakeDamage(int dmg,bool isCritical){
		StartCoroutine (SinglePlayer_GameManager.Instance.SinglePlayerCameraMovementScript.showDamage ());
		dmg -= defense;
		if ((CurrentHealth - dmg) < 0) {
			int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);
			string overkillString = languageIndex == 0 ? "OverKill" : "Labis na Pagpatay";
			FloatingTextManager.Instance.CreatePoppUpText (isCritical ? dmg.ToString () + " 2xCrit " + overkillString : dmg.ToString () + overkillString, this.transform);
		} else if ((CurrentHealth - dmg) == 0) {
			FloatingTextManager.Instance.CreatePoppUpText (isCritical ? dmg.ToString () + " 2xCrit" : dmg.ToString (), this.transform);
		}
		dmg = Mathf.Clamp (dmg, 0, int.MaxValue);
		currentHealth -= dmg;
	
			
		
		if (currentHealth <= 0) {
			SinglePlayer_GameManager.Instance.GameOver ();
			myAnimator.SetTrigger("Die");
		} else {
			myAnimator.SetTrigger("Damage");
			FloatingTextManager.Instance.CreatePoppUpText (isCritical ? dmg.ToString () + " 2xCrit" : dmg.ToString (), this.transform);
		}

	}

	private void StorePlayerStats(){
		int Added = (PlayerPrefs.GetInt ("PlayerLevel", 1) - 1) * 2;
		int addedDmg = PlayerPrefs.GetInt ("AddedDamage", 0);

		if (PlayerPrefs.HasKey ("PlayerAttackDamage")) {
			attackDamage = PlayerPrefs.GetInt ("PlayerAttackDamage") + Added + addedDmg;
		} else {
			attackDamage = 10 + Added + addedDmg;
		}
		int addedArmor = PlayerPrefs.GetInt ("AddedArmor", 0);
		if (PlayerPrefs.HasKey ("PlayerDefense")) {
			defense = PlayerPrefs.GetInt ("PlayerDefense") + (PlayerPrefs.GetInt ("PlayerLevel", 1) - 1) + addedArmor;
		} else {
			defense = 1 + Added + addedArmor;
		}

		int addedStr = PlayerPrefs.GetInt ("AddedStregth", 0);
		int totalStr = PlayerPrefs.GetInt ("PlayerStregth", 1) + (PlayerPrefs.GetInt ("PlayerLevel", 1) - 1) + addedStr;
		maxHealth = totalStr * 50;
		currentHealth = maxHealth;
	



	}


}
