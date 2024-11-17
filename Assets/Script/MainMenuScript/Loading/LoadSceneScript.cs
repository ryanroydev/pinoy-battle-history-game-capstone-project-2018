using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadSceneScript : MonoBehaviour {
	[SerializeField]
	private GameObject LoadPanel;
	[SerializeField]
	private Slider LoadBar;
	[SerializeField]
	private Text LoadPercentText;
	// Use this for initialization

	public void LoadScene(int SceneIndex,RectTransform newPanel){
		StartCoroutine (SyncLoadScene (SceneIndex, newPanel));
	}
	IEnumerator SyncLoadScene(int SceneIndex,RectTransform newPanel){
		AsyncOperation operation = SceneManager.LoadSceneAsync (SceneIndex);
		LoadPanel.SetActive (true);


		while (!operation.isDone) {
			float progress = Mathf.Clamp01 (operation.progress / .9f);
			LoadBar.value = progress;
			LoadPercentText.text = Mathf.Round (progress * 100f) + "%";


			yield return null;
		}
		if (operation.isDone) {
			LoadPanel.SetActive (false);
			Custom_NetworkLobbyManager._LMSingleton.ChangeToPanel (newPanel);
		}
	}
}
