using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SinglePlayerRewardPanelScript : MonoBehaviour {
	[SerializeField]
	private Text highScoreText,scoreText,expiText,coinText,rewarText,GameOverText;
	[SerializeField]
	private SinglePlayer_RewardSystem myReward;
	[SerializeField]private int NextLevelSceneIndex;
	[SerializeField]private Button okBtn;
	public bool StageClear = false;
	private bool isGameOver = false;
	[SerializeField]
	private AudioSource winSound,lostSound;

	// Use this for initialization

	void OnEnable(){
		scoreText.text = myReward.GetScore ().ToString ();
		checkNewHighScore ();
		int highScore = PlayerPrefs.HasKey ("SinglePlayerHighScore") ? PlayerPrefs.GetInt ("SinglePlayerHighScore") : 0;
		highScoreText.text = highScore.ToString ();

		//expi
		expiText.text = myReward.GetExpirience ().ToString ();
		StatsManager.Instance.AddExpi (myReward.GetExpirience ());
		//expi

		//coins
		coinText.text = myReward.GetCoin ().ToString ();
		StatsManager.Instance.addGold (myReward.GetCoin ());//add reward coin
		int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);
		//coins
		if (StageClear) {
			okBtn.onClick.RemoveAllListeners ();
			okBtn.onClick.AddListener (MoveUpToNextLevel);
			if (languageIndex == 0) {
				GameOverText.text = "STAGECLEAR";
			} else {
				GameOverText.text = "NATAPOS ANG YUGTO";
			}
			Inventory.Instance.GetRewardItem ();
			if (languageIndex == 0) {
				rewarText.text = Inventory.Instance.RewardItem.EnglishName;
			} else {
				rewarText.text = Inventory.Instance.RewardItem.tagalogName;
			}
			winSound.Play ();
		} else {
			okBtn.onClick.RemoveAllListeners ();
			okBtn.onClick.AddListener (LostBtn);
			if (languageIndex == 0) {
				GameOverText.text = "GAMEOVER";
			} else {
				GameOverText.text = "TAPOS NA ANG LARO";
			}
			lostSound.Play ();
		}

		//EquipmentManager.Instance.SaveInventoryItem ();
	} 
	void LostBtn(){
		if (Time.timeScale == 0) {
			SinglePlayer_GameManager.Instance.myLoadingScene.LoadScene (0, Custom_NetworkLobbyManager._LMSingleton.StartPanel);
		}
	}
	void MoveUpToNextLevel(){
		if (Time.timeScale == 0) {
			SinglePlayer_GameManager.Instance.myLoadingScene.LoadScene (NextLevelSceneIndex, Custom_NetworkLobbyManager._LMSingleton.SinglePlayerPanel);
		}
	}
	private void checkNewHighScore(){
		int highScore = PlayerPrefs.HasKey ("SinglePlayerHighScore") ? PlayerPrefs.GetInt ("SinglePlayerHighScore") : 0;
		if (myReward.GetScore () > highScore) {
			PlayerPrefs.SetInt ("SinglePlayerHighScore", myReward.GetScore ());
		}
	}
	public void GameOverFunction(){
		Time.timeScale = 0;
		isGameOver = true;
	}
}
