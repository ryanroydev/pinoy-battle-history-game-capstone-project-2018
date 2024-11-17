using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameSettings : MonoBehaviour {
	[SerializeField]
	private Slider SoundEffectsSlider;
	[SerializeField]
	private Slider BackGroundMusicSlider;
	[SerializeField]
	private Dropdown languageDropDown;
	// Use this for initialization
	void OnEnable() {
		//sounds
		BackGroundMusicSlider.onValueChanged.RemoveAllListeners ();
		BackGroundMusicSlider.onValueChanged.AddListener (delegate{BackGroundMusicVolume();});
		SoundEffectsSlider.onValueChanged.RemoveAllListeners ();
		SoundEffectsSlider.onValueChanged.AddListener (delegate {SoundEffectsVolume ();});
		//sounds

		//laguage
		languageDropDown.onValueChanged.RemoveAllListeners();
		languageDropDown.onValueChanged.AddListener (delegate {setLanguage ();});
		//laguage
		//sounds
		CheckBackGroundMusicSlider ();
		CheckSoundEffectsSlider ();
		//sounds
		//laguage
		checkLanguageDropdown ();
		//laguage

	}


	void Update () {
		//sounds
		CheckBackGroundMusicSlider ();
		CheckSoundEffectsSlider ();
		//sounds
		//laguage
		checkLanguageDropdown ();
	    //laguage
	}
	private void CheckBackGroundMusicSlider(){//Debug.Log (PlayerPrefs.GetFloat ("SoundEffects"));
		if (PlayerPrefs.HasKey ("BackGroundMusicVolume")) {
			if (BackGroundMusicSlider.value != PlayerPrefs.GetFloat ("BackGroundMusicVolume")) {
				BackGroundMusicSlider.value = PlayerPrefs.GetFloat ("BackGroundMusicVolume");

			}

		} else {
			BackGroundMusicSlider.value = 1f;
			PlayerPrefs.SetFloat ("BackGroundMusicVolume", 1f);//default  volume
		}
	}
	private void CheckSoundEffectsSlider(){//Debug.Log (PlayerPrefs.GetFloat ("SoundEffects"));
		if (PlayerPrefs.HasKey ("SoundEffectsVolume")) {
			if (SoundEffectsSlider.value != PlayerPrefs.GetFloat ("SoundEffectsVolume")) {
				SoundEffectsSlider.value = PlayerPrefs.GetFloat ("SoundEffectsVolume");

			}

		} else {
			PlayerPrefs.SetFloat ("SoundEffectsVolume", 1f);
			SoundEffectsSlider.value = 1f;//default  volume
		}
	}
	private void BackGroundMusicVolume(){
		
		PlayerPrefs.SetFloat ("BackGroundMusicVolume", BackGroundMusicSlider.value);
	}
	private void SoundEffectsVolume(){

		PlayerPrefs.SetFloat ("SoundEffectsVolume", SoundEffectsSlider.value);
	}

	private void  checkLanguageDropdown(){
	
		if (PlayerPrefs.HasKey ("CurrentLanguage")) {
			if (languageDropDown.value != PlayerPrefs.GetInt ("CurrentLanguage")) {
				languageDropDown.value = PlayerPrefs.GetInt ("CurrentLanguage");
			}

		} else {
			languageDropDown.value = 0;
			PlayerPrefs.SetInt ("CurrentLanguage", 0);//default  volume
		}
	}

	private void setLanguage(){

		PlayerPrefs.SetInt ("CurrentLanguage", languageDropDown.value);
		StatsManager.Instance.SetLanguage ();
	}
}
