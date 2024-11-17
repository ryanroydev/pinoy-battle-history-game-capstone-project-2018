using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextDotAni : MonoBehaviour {
	[SerializeField]
	private Text myText;
	private string thisText;
	private bool animating;
	void OnEnable(){
		int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);
		if (languageIndex == 0) {
			thisText = "Waiting for your opponent";
		} else {
			thisText = "Naghihintay ng iyong Kalaban";
		}

		animating = true;
		StartCoroutine (updateText ());

	}

	IEnumerator updateText(){
		int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);
		if (languageIndex == 0) {
			thisText = "Waiting for your opponent";
		} else {
			thisText = "Naghihintay ng iyong Kalaban";
		}
		while(animating) {
			myText.text = thisText + ".";
			yield return new WaitForSeconds (1f);
			myText.text = thisText + "..";
			yield return new WaitForSeconds (1f);
			myText.text = thisText + "...";
			yield return new WaitForSeconds (1f);
			myText.text = thisText + "....";
			yield return new WaitForSeconds (1f);
			myText.text = thisText + ".....";
			yield return new WaitForSeconds (1f);
		}
	}
	void OnDisable(){
		animating = false;
	}
}
