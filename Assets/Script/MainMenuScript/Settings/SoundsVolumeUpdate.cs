using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsVolumeUpdate : MonoBehaviour {
	[SerializeField]
	private AudioSource[] BackGroundMusics;

	[SerializeField]
	private AudioSource[] SoundEffects;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		CheckBackGroundMusicVolume ();

		CheckSoundEffectsVolume ();
	}
	private void CheckBackGroundMusicVolume(){
		if (BackGroundMusics.Length != 0) {
			for (int i = 0; i < BackGroundMusics.Length; i++) {

				if (PlayerPrefs.HasKey ("BackGroundMusicVolume")) {
					if (BackGroundMusics [i].volume != PlayerPrefs.GetFloat ("BackGroundMusicVolume")) {
						BackGroundMusics [i].volume = PlayerPrefs.GetFloat ("BackGroundMusicVolume");
					}
				} else {
					if (BackGroundMusics [i].volume != 1f) {
						BackGroundMusics [i].volume = 1f;
					}
				}
			}
		}
	}	

	private void CheckSoundEffectsVolume(){
	
		if (SoundEffects.Length != 0) {
			for (int i = 0; i < SoundEffects.Length; i++) {
				if (PlayerPrefs.HasKey ("SoundEffectsVolume")) {
					if (SoundEffects [i].volume != PlayerPrefs.GetFloat ("SoundEffectsVolume")) {
						SoundEffects [i].volume = PlayerPrefs.GetFloat ("SoundEffectsVolume");
					}
				}else if (PlayerPrefs.HasKey ("SoundEffectsVolume")) {
					if (SoundEffects [i].volume != 1f) {
						SoundEffects [i].volume = 1f;
					}
				}
			}
		}
	}
}
