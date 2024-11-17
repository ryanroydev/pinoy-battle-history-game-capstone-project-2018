using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SinglePlayerIntroCameraAnimate : MonoBehaviour {
	[SerializeField]
	private string categoryEnglish,categoryTagalog;
	[SerializeField]
	private Button SkipBtn;
	public GameObject mainCamera;
	public GameObject introCamera;
	void OnEnable(){
		SkipBtn.onClick.RemoveAllListeners ();
		SkipBtn.onClick.AddListener (SkipButton);
	}
	public void TransformEnemies(){
		foreach (GameObject enemy in SinglePlayer_GameManager.Instance.HumanEnemies) {
			enemy.SetActive (false);
		}
		foreach (GameObject enemy in SinglePlayer_GameManager.Instance.Enemies) {
			enemy.SetActive (true);
		}
	}
	public void DisableIntroCamera(){
		introCamera.SetActive (false);
		mainCamera.SetActive (true);
		SinglePlayer_GameManager.Instance.CreatePoppUpText (SinglePlayer_GameManager.Instance.isTagalog ? categoryTagalog : categoryEnglish);
		SkipBtn.gameObject.SetActive (false);
	}
	public void SkipButton(){
		TransformEnemies ();
		DisableIntroCamera ();
	}
}
