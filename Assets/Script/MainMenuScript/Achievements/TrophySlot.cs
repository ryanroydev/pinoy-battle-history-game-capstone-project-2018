using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TrophySlot : MonoBehaviour {
	[SerializeField]
	private Bar trophyBar;
	[SerializeField]
	private Text volumeText;
	[SerializeField]
	private Image tropyImage;
	public int TrophyIndex;
	private int currentCrown,maxCrown;
	[SerializeField]
	private Sprite lockTrophy, unlockTrophy;

	public bool IsMultiQuest;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		initializeTrophyBar ();
	}
	private void initializeTrophyBar(){
		int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);
		AchievementsManager AM = AchievementsManager.Instance;
		switch (TrophyIndex) {
		case 1:
			
			currentCrown = AM.GetCurrentOneCrowns ();
			if (AM.GetCurrentTwoCrowns () == AM.GetTotalCrowns ()) {
				tropyImage.sprite = unlockTrophy;
				if (languageIndex == 0) {
					volumeText.text = "Completed";
				} else {
					volumeText.text = "Nakumpleto";
				}
			} else {
				tropyImage.sprite = lockTrophy;
				if (languageIndex == 0) {
					volumeText.text = "First Crown :" + AM.GetCurrentOneCrowns () + "/" + AM.GetTotalCrowns ();
				} else {
					volumeText.text = "Unang Korona :" + AM.GetCurrentOneCrowns () + "/" + AM.GetTotalCrowns ();
				}
			}
			break;
		case 2:
			
			currentCrown = AM.GetCurrentTwoCrowns ();
			if (AM.GetCurrentTwoCrowns () == AM.GetTotalCrowns ()) {
				tropyImage.sprite = unlockTrophy;
				if (languageIndex == 0) {
		
					volumeText.text = "Completed";
				} else {
					volumeText.text = "Nakumpleto";
				}
			} else {
				tropyImage.sprite = lockTrophy;
				if (languageIndex == 0) {
					volumeText.text = "Second Crowns :" + AM.GetCurrentTwoCrowns () + "/" + AM.GetTotalCrowns ();
				} else {
					volumeText.text = "Pangalawang Korona :" + AM.GetCurrentTwoCrowns () + "/" + AM.GetTotalCrowns ();
				}
			}
			break;
		case 3:
			
			currentCrown = AM.GetCurrentThreeCrowns ();
			if (AM.GetCurrentThreeCrowns() == AM.GetTotalCrowns ()) {
				tropyImage.sprite = unlockTrophy;
				if (languageIndex == 0) {
					volumeText.text = "Completed";
				} else {
					volumeText.text = "Nakumpleto";
				}
			} else {
				tropyImage.sprite = lockTrophy;
				if (languageIndex == 0) {
					volumeText.text = "Third Crowns : " + AM.GetCurrentThreeCrowns () + "/" + AM.GetTotalCrowns ();
				} else {
					volumeText.text = "Pangatlong Korona : " + AM.GetCurrentThreeCrowns () + "/" + AM.GetTotalCrowns ();
				}
			}
			break;
		case 4:

			currentCrown = GetAllTrophy ();
			if (AM.GetCurrentThreeCrowns () == AM.GetTotalCrowns () && AM.GetCurrentTwoCrowns () == AM.GetTotalCrowns () && AM.GetCurrentOneCrowns () == AM.GetTotalCrowns ()) {
				tropyImage.sprite = unlockTrophy;
				currentCrown = 3;
				if (languageIndex == 0) {
					volumeText.text = "Completed";
				} else {
					volumeText.text = "Nakumpleto";
				}
			} else {
				tropyImage.sprite = lockTrophy;
				if (languageIndex == 0) {
					volumeText.text = "Trophy : " + currentCrown + "/" + 3;
				} else {
					volumeText.text = "Tropeo : " + currentCrown + "/" + 3;
				}
			}
			break;
		}

		if (IsMultiQuest) {
			maxCrown = 3;
			if (trophyBar != null) {
				trophyBar.MaxValue = maxCrown;
				trophyBar.Value = Mathf.Clamp (currentCrown, 0, maxCrown);

			}
		} else {

			maxCrown = AM.GetTotalCrowns ();
			if (trophyBar != null) {
				trophyBar.MaxValue = maxCrown;
				trophyBar.Value = Mathf.Clamp (currentCrown, 0, maxCrown);

			}
		}
	}
	 int GetAllTrophy(){
		int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);
		AchievementsManager AM = AchievementsManager.Instance;
		if (AM.GetCurrentThreeCrowns () == AM.GetTotalCrowns () && AM.GetCurrentTwoCrowns () == AM.GetTotalCrowns () && AM.GetCurrentOneCrowns () == AM.GetTotalCrowns ()) {
			return 3;
		} else if (AM.GetCurrentTwoCrowns () == AM.GetTotalCrowns () && AM.GetCurrentOneCrowns () == AM.GetTotalCrowns ()) {
			return 2;
		} else if (AM.GetCurrentOneCrowns () == AM.GetTotalCrowns ()) {
			return 1;
		} else {
			return 0;
		}
	}
}
