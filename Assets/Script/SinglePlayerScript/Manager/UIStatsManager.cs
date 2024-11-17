using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIStatsManager : MonoBehaviour {
	[SerializeField]
	private Text nameText;

	private int currentAttackDmg, currentDefense,currentLevel,currentWinMatches;
	private string currentName;
	[SerializeField]private Image TrophyImage;
	[SerializeField]private Sprite[] trophySprites;
	private Sprite currentSprite;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		DisplayName ();
	}
	private void checkLevel(){
		if (PlayerPrefs.HasKey ("PlayerLevel")) {
			if (currentLevel != PlayerPrefs.GetInt ("PlayerLevel")) {
				currentLevel = PlayerPrefs.GetInt ("PlayerLevel");
			}
		} else {
			if (currentLevel != 1) {
				currentLevel = 1;
			}
		}
	}
	private  void  DisplayName(){
		checkLevel ();
		int TrophyIndex = PlayerPrefs.GetInt ("Trophy", 0);
		if (currentSprite == null || currentSprite != trophySprites [TrophyIndex]) {
			currentSprite = trophySprites [TrophyIndex];
			TrophyImage.sprite = trophySprites [TrophyIndex];
		}


		if (PlayerPrefs.HasKey ("PlayerName")) {
			if (PlayerPrefs.GetString ("PlayerName") != currentName) {
				nameText.text = PlayerPrefs.GetString ("PlayerName") + " lvl" + currentLevel.ToString ();
				currentName = PlayerPrefs.GetString ("PlayerName");
			}
		} else {
			string defaultName = "Player";
			if (currentName != defaultName) {
				nameText.text = defaultName + " lvl" + currentLevel.ToString ();
				currentName = defaultName;
			}
		}
	}
}
