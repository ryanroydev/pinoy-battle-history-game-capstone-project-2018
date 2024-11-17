using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BookPageScript : MonoBehaviour {
	[SerializeField]private string characterName;
	[SerializeField]private string descriptionsTagalog,descriptionEnglish;
	[SerializeField]private Image charPic;
	[SerializeField]private Sprite unLockPic, lockPic;
	[SerializeField]private Text nameText,descriptionText,countText;
	[SerializeField]private MonsterType monster;
	private string currentWord;

	void OnEnable(){
		
		if (PlayerPrefs.GetString (characterName, "") != "") {
			charPic.sprite = unLockPic;
			nameText.text = characterName;
			int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);
			switch (monster.ToString()) {
			case "Tiyanak":
				int tiyanakKilled = PlayerPrefs.GetInt ("TiyanakKilled", 0);
				if (languageIndex == 0) {
					countText.text = "You Kill " + tiyanakKilled + " of them";
				} else {
					countText.text = "Nakapatay Ka ng " + tiyanakKilled + " sa kanila";
				}
				break;
			case "Manananggal":
				int ManananggalKilled = PlayerPrefs.GetInt ("ManananggalKilled", 0);
				if (languageIndex == 0) {
					countText.text = "You Kill " + ManananggalKilled + " of them";
				} else {
					countText.text = "Nakapatay Ka ng " + ManananggalKilled + " sa kanila";
				}
				break;
			case "ManananggalHalfBody":
				int ManananggalHalfBodyKilled = PlayerPrefs.GetInt ("ManananggalHalfBodyKilled", 0);
				break;
			case "Kapre":
				int KapreKilled = PlayerPrefs.GetInt ("KapreKilled", 0);
				if (languageIndex == 0) {
					countText.text = "You Kill " + KapreKilled + " of them";
				} else {
					countText.text = "Nakapatay Ka ng " + KapreKilled + " sa kanila";
				}
				break;
			case "Tiktik":
				int TiktikKilled = PlayerPrefs.GetInt ("TiktikKilled", 0);
				if (languageIndex == 0) {
					countText.text = "You Kill " + TiktikKilled + " of them";
				} else {
					countText.text = "Nakapatay Ka ng " + TiktikKilled + " sa kanila";
				}
				break;
			}
		} else {
			charPic.sprite = lockPic;

		}
	}
	void Update(){
		if (PlayerPrefs.GetString (characterName, "") != "") {
			if (PlayerPrefs.HasKey ("CurrentLanguage")) {
				if (PlayerPrefs.GetInt ("CurrentLanguage") == 0) {
					if (currentWord != descriptionEnglish) {
						descriptionText.text = descriptionEnglish;
						currentWord = descriptionEnglish;
					}
				} else if (PlayerPrefs.GetInt ("CurrentLanguage") == 1) {
					if (currentWord != descriptionsTagalog) {
						descriptionText.text = descriptionsTagalog;
						currentWord = descriptionsTagalog;
					}
				}

			} else {
				//default  language is english
				if (currentWord != descriptionEnglish) {
					descriptionText.text = descriptionEnglish;
					currentWord = descriptionEnglish;
				}
			}
		}
	}
}
