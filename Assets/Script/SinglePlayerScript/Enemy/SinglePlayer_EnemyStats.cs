using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayer_EnemyStats : MonoBehaviour {
	public MonsterType monsterName;
	[SerializeField]
	private Bar healthBar;
	private int previousHealth;
	[SerializeField]private int currentHealth;
	[SerializeField]private int maxHealth;
	[SerializeField]private int attackDamage;
	[SerializeField]private int defense;
	private Animator myAnimator;

	public int MaxHealth{get{ return maxHealth;}set{ this.maxHealth = value;}}
	public int CurrentHealth{get{ return currentHealth;}set{ this.currentHealth = value;}}
	public int AttackDamage{get{ return attackDamage;}set{ this.attackDamage = value;}}
	private int Defense{get{ return defense;}set{ this.defense = value;}}

	// Use this for initialization
	void Start () {
		myAnimator = GetComponent<Animator> ();
		initializeHealthBar ();
	}
	void Update(){
		initializeHealthBar ();

	}
	private void initializeHealthBar(){
		if (healthBar != null && previousHealth != currentHealth) {
			healthBar.MaxValue = maxHealth;
			healthBar.Value = Mathf.Clamp (currentHealth, 0, maxHealth);
			previousHealth = currentHealth;

		}
	}
	public void TakeDamage(int dmg,bool isCritical){
		if ((CurrentHealth - dmg) < 0) {
			int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);
			string overkillString = languageIndex == 0 ? "OverKill" : "Labis na Pagpatay";
			FloatingTextManager.Instance.CreatePoppUpText (isCritical ? dmg.ToString () + " 2xCrit " + overkillString : dmg.ToString () + overkillString, this.transform);
		} else if ((CurrentHealth - dmg) == 0) {
			FloatingTextManager.Instance.CreatePoppUpText (isCritical ? dmg.ToString () + " 2xCrit" : dmg.ToString (), this.transform);
		}
		currentHealth -= dmg;
	
		if (currentHealth <= 0) {
			myAnimator.SetTrigger ("Die");
			SaveLoadKilled ();
		} else {
			myAnimator.SetTrigger ("Damage");
			FloatingTextManager.Instance.CreatePoppUpText (isCritical ? dmg.ToString () + " 2xCrit" : dmg.ToString (), this.transform);
		}
	}
	public void SaveLoadKilled(){
		
		switch (monsterName.ToString()) {
		case "Tiyanak":
			int tiyanakKilled = PlayerPrefs.GetInt ("TiyanakKilled", 0);
			tiyanakKilled++;
			PlayerPrefs.SetInt ("TiyanakKilled", tiyanakKilled);
			break;
		case "Manananggal":
			int ManananggalKilled = PlayerPrefs.GetInt ("ManananggalKilled", 0);
			ManananggalKilled++;
			PlayerPrefs.SetInt ("ManananggalKilled", ManananggalKilled);
			break;
		case "ManananggalHalfBody":
			int ManananggalHalfBodyKilled = PlayerPrefs.GetInt ("ManananggalHalfBodyKilled", 0);
			ManananggalHalfBodyKilled++;
			PlayerPrefs.SetInt ("ManananggalHalfBodyKilled", ManananggalHalfBodyKilled);
			break;
		case "Kapre":
			int KapreKilled = PlayerPrefs.GetInt ("KapreKilled", 0);
			KapreKilled++;
			PlayerPrefs.SetInt ("KapreKilled", KapreKilled);
			break;
		case "Tiktik":
			int TiktikKilled = PlayerPrefs.GetInt ("TiktikKilled", 0);
			TiktikKilled++;
			PlayerPrefs.SetInt ("TiktikKilled", TiktikKilled);
			break;
		}
	}

}
