using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadingTriviaScript : MonoBehaviour {
	[SerializeField]
	private Image myLoadingScreen;
	[SerializeField]
	private Sprite[] LoadingScreenSprites;
	[SerializeField]
	private List<string> englishTrivias;
	[SerializeField]
	private List<string> tagalogTrivias;
	[SerializeField]
	private Text triviaText;
	// Use this for initialization
	void OnEnable () {


		myLoadingScreen.sprite = LoadingScreenSprites [Random.Range (0, LoadingScreenSprites.Length)];

		showTrivia ();
	}
	
	void showTrivia(){
		if (PlayerPrefs.HasKey ("CurrentLanguage")) {
			if (PlayerPrefs.GetInt ("CurrentLanguage", 0) == 0) {
				//auto and english
				showEnglishTrivia ();
			} else if (PlayerPrefs.GetInt ("CurrentLanguage") == 1) {
				//tagalog
				showtagalogTrivia ();
			}
		} else {
			//default language english
			showEnglishTrivia ();
		}
	}
	private void showEnglishTrivia(){
		if (englishTrivias.Count != 0) {
			int randTriviaIndex = Random.Range (0, englishTrivias.Count);
			triviaText.text = englishTrivias [randTriviaIndex];
		}
	}
	private void showtagalogTrivia(){
		if (tagalogTrivias.Count != 0) {
			int randTriviaIndex = Random.Range (0, tagalogTrivias.Count);
			triviaText.text = tagalogTrivias [randTriviaIndex];
		}
	}
}
