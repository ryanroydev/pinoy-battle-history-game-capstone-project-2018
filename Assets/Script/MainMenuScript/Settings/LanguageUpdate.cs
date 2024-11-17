using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LanguageUpdate : MonoBehaviour {

	[SerializeField]
	private Text myText;

	private string currentWord;
	[SerializeField]
	private string englishWord;
	[SerializeField]
	private string tagalogWord;

	void OnEnable () {
		checkLanguage ();
	}
	
	// Update is called once per frame
	void Update () {
		checkLanguage ();
	}
	void checkLanguage(){
		if (PlayerPrefs.HasKey ("CurrentLanguage")) {
			if (PlayerPrefs.GetInt ("CurrentLanguage") == 0) {
				if (currentWord != englishWord) {
					myText.text = englishWord;
					currentWord = englishWord;
				}
			} else if(PlayerPrefs.GetInt ("CurrentLanguage") == 1) {
				if (currentWord != tagalogWord) {
					myText.text = tagalogWord;
					currentWord = tagalogWord;
				}
			}

		} else {
			//default  language is english
			if (currentWord != englishWord) {
				myText.text = englishWord;
				currentWord = englishWord;
			}
		}
	}
}
