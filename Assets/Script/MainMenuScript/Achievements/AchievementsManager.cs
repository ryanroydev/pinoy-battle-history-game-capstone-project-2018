using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AchievementsManager : MonoBehaviour {
	public AudioSource AchievementSound;
	private static AchievementsManager instance;
	public static AchievementsManager Instance {
		get {
			if (instance == null) {
				instance = GameObject.FindObjectOfType<AchievementsManager> ();
			}
			return instance;
		}
	}

	public Bar AchievementBar;
	private int currentAchievementCrown,maxAchievementCrown;

	public AchievementSlot[] AchiveSlots;
	public Text PercentText;
	public Text VolumeText;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		initializeAchievmentBar ();
	}
	private void initializeAchievmentBar(){
		int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);
		currentAchievementCrown = GetCrowns();
		maxAchievementCrown = GetMaxCrowns ();
		if ( AchievementBar != null) {
			AchievementBar.MaxValue = maxAchievementCrown;
			AchievementBar.Value = Mathf.Clamp (currentAchievementCrown, 0, maxAchievementCrown);

			PercentText.text = Mathf.FloorToInt ((map (currentAchievementCrown, maxAchievementCrown, 0, 1, 0) * 100)).ToString () + "%";
			if (languageIndex == 0) {
				VolumeText.text = "Crown : " + currentAchievementCrown + "/" + maxAchievementCrown;
			} else {
				VolumeText.text = "Korona : " + currentAchievementCrown + "/" + maxAchievementCrown;
			}
		}
	}
	private float map(float value, float inMax,float inMin, float outMax, float outMin)
	{
		return (value + inMin) * (outMax - outMin) / (inMax - inMin) - inMin;
	}
	int GetCrowns(){
		int TotalCurrentCrowns = 0;
		foreach (AchievementSlot slot in AchiveSlots) {
			TotalCurrentCrowns += slot.GetCrowns ();
		}
		return TotalCurrentCrowns;
	}
	int GetMaxCrowns(){
		int TotalMaxCrowns = 0;
		foreach (AchievementSlot slot in AchiveSlots) {
			TotalMaxCrowns += 3;
		}
		return TotalMaxCrowns;
	}
	public int GetTotalCrowns(){
		int TotalCrowns = 0;
		foreach (AchievementSlot slot in AchiveSlots) {
			TotalCrowns += 1;
		}
		return TotalCrowns;
	}
	public int GetCurrentOneCrowns(){
		int TotalCurrentCrowns = 0;
		foreach (AchievementSlot slot in AchiveSlots) {
			TotalCurrentCrowns += slot.GetCurrentOneCrown ();
		}
		return TotalCurrentCrowns;
	}
	public int GetCurrentTwoCrowns(){
		int TotalCurrentCrowns = 0;
		foreach (AchievementSlot slot in AchiveSlots) {
			TotalCurrentCrowns += slot.GetCurrentTwoCrown ();
		}
		return TotalCurrentCrowns;
	}
	public int GetCurrentThreeCrowns(){
		int TotalCurrentCrowns = 0;
		foreach (AchievementSlot slot in AchiveSlots) {
			TotalCurrentCrowns += slot.GetCurrentThreeCrown ();
		}
		return TotalCurrentCrowns;
	}
	public void SetTrophy(){
		if (GetCurrentThreeCrowns () == GetTotalCrowns ()) {
			PlayerPrefs.SetInt ("Trophy", 3);
		} else if (GetCurrentTwoCrowns () == GetTotalCrowns ()) {
			PlayerPrefs.SetInt ("Trophy", 2);
		} else if (GetCurrentOneCrowns () == GetTotalCrowns ()) {
			PlayerPrefs.SetInt ("Trophy", 1);
		}
	}
	public void PlaySoundAchievement(){
		AchievementSound.Play ();
	}
}
