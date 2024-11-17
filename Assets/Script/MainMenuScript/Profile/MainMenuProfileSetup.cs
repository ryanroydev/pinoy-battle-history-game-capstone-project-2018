using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenuProfileSetup : MonoBehaviour {
	[SerializeField]
	private Sprite[] characterSprites;
	[SerializeField]
	private Image characterImage;
	[SerializeField]
	private Text NameText, DmgText, defText,strText, winText, lostText, highScoreText,addedDmgText,addedArmorText,addedStrText;
	[SerializeField]
	private ProfileExpiInfoPanelScript expiPanelScript;

	private int currentAttackDmg, currentDefense,currentLevel,currentWinMatches,currentHighScore,currentStr;
	private int previousExpi, currentExpi, maxExpi;
	private string currentName;
	void OnEnable(){
		checkCharacterSprite ();
		CheckStats ();
	}
	void Update () {
		checkCharacterSprite ();
		CheckProfile();
		initializeExpiBar ();
	}
	public void OpenExpiPanel(bool isOpen){
		expiPanelScript.gameObject.SetActive (isOpen);
	}

	void checkCharacterSprite(){
		int mycharacterImgIndex = PlayerPrefs.GetInt ("CharacterIndex", 0);
			if (characterImage.sprite != characterSprites [mycharacterImgIndex] || expiPanelScript.ChracterImage != characterSprites [mycharacterImgIndex]) {
				characterImage.sprite = characterSprites [mycharacterImgIndex];
			    expiPanelScript.ChracterImage.sprite = characterSprites [mycharacterImgIndex];
			}

	}
	private void CheckProfile(){
		
	
		checkName ();
		checkWin ();
		checkLoss ();
		checkHighScore ();
		CheckStats ();
	}
	private void CheckStats(){
		checkLevel ();
		int levelStats = (currentLevel - 1) * 2;
		checkAttackDmg (levelStats);
		checkStr (currentLevel - 1);
		checkDefense (currentLevel - 1);
	}
	private void checkAttackDmg(int added){
		int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);
		if (!PlayerPrefs.HasKey ("PlayerAttackDamage")) {
			PlayerPrefs.SetInt ("PlayerAttackDamage", 10);
		}
		currentAttackDmg = PlayerPrefs.GetInt ("PlayerAttackDamage") + added;
		if (languageIndex == 0) {
			DmgText.text = "Damage : " + currentAttackDmg.ToString ();
		} else {
			DmgText.text = "Bawas : " + currentAttackDmg.ToString ();
		}
		addedDmgText.text = " +" + PlayerPrefs.GetInt ("AddedDamage", 0).ToString ();
	}
	private void checkStr(int added){
		int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);
		if (!PlayerPrefs.HasKey ("PlayerStregth")) {
			PlayerPrefs.SetInt ("PlayerStregth", 1);
		}
		currentStr = PlayerPrefs.GetInt ("PlayerStregth", 1) + added;
		strText.text = "Str : " + currentStr.ToString ();


		addedStrText.text = " +" + PlayerPrefs.GetInt ("AddedStregth", 0).ToString ();
	}
	private void checkDefense(int added){
		int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);
		if (!PlayerPrefs.HasKey ("PlayerDefense")) {
			PlayerPrefs.SetInt ("PlayerDefense", 1);
		}
		currentDefense = PlayerPrefs.GetInt ("PlayerDefense", 1) + added;
		if (languageIndex == 0) {
			defText.text = "Defense : " + currentDefense.ToString ();
		} else {
			defText.text = "Depensa : " + currentDefense.ToString ();
		}
		addedArmorText.text = " +" + PlayerPrefs.GetInt ("AddedArmor", 0).ToString ();
	}
	private void checkName(){
		int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);
		checkLevel ();//check level first b4 display playername 
		if (PlayerPrefs.HasKey ("PlayerName")) {
			
				currentName = PlayerPrefs.GetString ("PlayerName");
				NameText.text = currentName + " lvl" + currentLevel.ToString ();
				expiPanelScript.NameText.text = currentName + " lvl" + currentLevel.ToString ();

		} else {
			if (languageIndex == 0) {
				currentName = "Player";
			} else {
				currentName = "Manlalaro";
			}
				NameText.text = currentName + " lvl" + currentLevel.ToString ();
				expiPanelScript.NameText.text = currentName + " lvl" + currentLevel.ToString ();


		}
	}
	private void checkLevel(){

		currentLevel = PlayerPrefs.GetInt ("PlayerLevel", 1);
	
	}
	private void checkLoss(){
		int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);
		if (PlayerPrefs.HasKey ("LossRecord")) {
			//if (currentWinMatches != PlayerPrefs.GetInt ("LossRecord")) {
				currentWinMatches = PlayerPrefs.GetInt ("LossRecord");
				if (languageIndex == 0) {
					lostText.text = "Loss : " + currentWinMatches.ToString ();
				} else {
					lostText.text = "Talo : " + currentWinMatches.ToString ();
				}
			//}
		} else {
			if (currentWinMatches != 0) {
				currentWinMatches = 0;
				if (languageIndex == 0) {
					lostText.text = "loss : " + currentWinMatches.ToString ();
				} else {
					lostText.text = "Talo : " + currentWinMatches.ToString ();
				}
			}
		}
	}
	private void checkWin(){
		int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);
		if (PlayerPrefs.HasKey ("WonRecord")) {
			//if (currentWinMatches != PlayerPrefs.GetInt ("WonRecord")) {
				currentWinMatches = PlayerPrefs.GetInt ("WonRecord");
				if (languageIndex == 0) {
					winText.text = "Win : " + currentWinMatches.ToString ();
				} else {
					winText.text = "Panalo : " + currentWinMatches.ToString ();
				}
			//}
		} else {
			if (currentWinMatches != 0) {
				currentWinMatches = 0;
				if (languageIndex == 0) {
					winText.text = "Win : " + currentWinMatches.ToString ();
				} else {
					winText.text = "Panalo : " + currentWinMatches.ToString ();
				}
			}
		}
	}
	private void checkHighScore(){
		int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);
		if (PlayerPrefs.HasKey ("SinglePlayerHighScore")) {
		//	if (currentHighScore != PlayerPrefs.GetInt ("SinglePlayerHighScore")) {
				currentHighScore = PlayerPrefs.GetInt ("SinglePlayerHighScore");
				if (languageIndex == 0) {
					highScoreText.text = "HighScore : " + currentHighScore.ToString ();
				} else {
					highScoreText.text = "PinakaMataas \n na Puntos : " + currentHighScore.ToString ();
				}
			//} 
		} else {
			if (currentHighScore != 0) {
				currentHighScore = 0;

				if (languageIndex == 0) {
					highScoreText.text = "HighScore : " + currentHighScore.ToString ();
				} else {
					highScoreText.text = "PinakaMataas \nna Puntos : " + currentHighScore.ToString ();
				}
			}
		}
	}

	private void initializeExpiBar(){
		currentExpi = StatsManager.Instance.GetExpi ();
		maxExpi = StatsManager.Instance.GetMaxExpi ();
		if (expiPanelScript.ExpiBar != null) {
			expiPanelScript.ExpiBar.MaxValue = maxExpi;
			expiPanelScript.ExpiBar.Value = Mathf.Clamp (currentExpi, 0, maxExpi);


		}
	}
}
