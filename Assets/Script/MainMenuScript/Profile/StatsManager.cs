using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StatsManager : MonoBehaviour {
	private static StatsManager instance;
	public static StatsManager Instance{get{ if (instance == null) {instance = GameObject.FindObjectOfType<StatsManager> ();}return instance;}}
	private bool isTagalog = false;
	private int GoldCoins,Expi,Level;
	[SerializeField]
	private Text GoldText;
	[SerializeField]
	private AudioSource GoldSound;
	public void OnEnable(){
		SetLanguage ();
		GoldCoins = PlayerPrefs.GetInt ("GoldCoins", 0);
		GoldText.text = (!isTagalog) ? "Gold : " + GoldCoins.ToString () : "Ginto : " + GoldCoins.ToString ();
		Expi = PlayerPrefs.GetInt("PlayerExpi", 0);
		Level = PlayerPrefs.GetInt ("PlayerLevel", 1);
		//AddExpi (60);
	}
	public void SetLanguage(){
		int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);

		switch (languageIndex) {
		case 0:
			isTagalog = false;
			break;
		case 1:
			isTagalog = true;

			break;
		}
		GoldText.text = (!isTagalog) ? "Gold : " + GoldCoins.ToString () : "Ginto : " + GoldCoins.ToString ();
	}
	public int GetExpi(){
		return Expi;
	}
	public int GetGold(){
		SetLanguage ();
		return GoldCoins;
	}
	public void addGold(int gold){
		SetLanguage ();
		GoldCoins += gold;
		GoldText.text = (!isTagalog) ? "Gold : " + GoldCoins.ToString () : "Ginto : " + GoldCoins.ToString ();
		if (gold != 0) {
			GoldText.GetComponentInChildren<Animator> ().SetTrigger ("NewCoin");
		}
		PlayerPrefs.SetInt ("GoldCoins", GoldCoins);
		GoldSound.Play ();
	}
	public void AddExpi(int expi){
		Expi += expi;

		//Debug.Log (expiNeededToNextLvl);
		while (Expi >= GetMaxExpi()) {
			     LevelUp ();
			   Expi -= GetMaxExpi ();
		}
		PlayerPrefs.SetInt ("PlayerExpi", Expi);
	}
	void Update(){
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			LevelUp ();
		}
	}
	void LevelUp(){
		Level++;
		PlayerPrefs.SetInt ("PlayerLevel", Level);
	}
	public int GetMaxExpi(){
		int expiNeededToNextLvl = (Level * Level + Level + 3) * 4;
		return expiNeededToNextLvl;
	}
}
