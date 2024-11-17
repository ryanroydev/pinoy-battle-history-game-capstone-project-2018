using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TwoPlayer_GameManager : MonoBehaviour {

	public PotionPanelScript PotionPanel;
	public RectTransform BloodPanel;
	public FloatingText FloatingText;

	public Transform FloatngTextPos;

	[SerializeField]
	private FloatingText InfoPopUpText;
	[SerializeField]
	private FloatingText WarningPopUpText;
	[SerializeField]
	private Transform floatngTextPos;
	private static TwoPlayer_GameManager instance;
	public static TwoPlayer_GameManager Instance{get{ if (instance == null) {instance = GameObject.FindObjectOfType<TwoPlayer_GameManager> ();}return instance;}}
	private TwoPlayer_QuizManager QuizManager;
	public RectTransform GameOverPanel;
	public Text GameOverPanelText;
	public Text GameOverText;
	public float countDown,Timer;
	private RectTransform countDownPanel;
	private Text countDownText;

	private RectTransform SettingPanel;
	private bool questionAnswered;
	public bool QuestionAnswered{get{ return questionAnswered;}set{ this.questionAnswered = value;}}
	void OnEnable(){


		SettingPanel = Custom_NetworkLobbyManager._LMSingleton.TwoPlayerSettingPanel;
		countDownPanel = Custom_NetworkLobbyManager._LMSingleton.TwoPlayerCountDownPanel;
		countDownText = Custom_NetworkLobbyManager._LMSingleton.TwoPlayerCountDownText;
		QuizManager = Custom_NetworkLobbyManager._LMSingleton.TwoPlayerQuizManager;


	}
	public void Surrender(){
		if (Time.timeScale == 0) {
			GameOverPanel.gameObject.SetActive (false);
			Custom_NetworkLobbyManager._LMSingleton.StopHost ();
			Custom_NetworkLobbyManager._LMSingleton.ChangeToPanel (Custom_NetworkLobbyManager._LMSingleton.StartPanel);
		}

	}
	void OnDisable(){

		//reset   panel
		Custom_NetworkLobbyManager._LMSingleton.TwoPlayerButtonSettingPanel.gameObject.SetActive (true);
		SettingPanel.gameObject.SetActive (false);
		ShowQuizPanel (false);
	}
	public void ShowQuizPanel(bool isTrue){
		//Debug.Log (isTrue + "quuz");
		if (isTrue) {
			QuizManager.gameObject.SetActive (isTrue);
			QuizManager.gameObject.GetComponent<Animator> ().SetBool ("Fadein", isTrue);
			StartCoroutine (StartCountDown ());
			//Debug.Log (isTrue + "quuz1");
			//StartCoroutine (StartCountDown ());
		} else {
			QuizManager.gameObject.GetComponent<Animator> ().SetBool ("Fadein", isTrue);
			QuizManager.gameObject.SetActive (isTrue);

		}


	}
	public void CreatePoppUpInfoText(string info){
		FloatingText ft = Instantiate(InfoPopUpText);

		ft.transform.SetParent (this.transform, false);
		//ft.transform.position = new Vector2 (538f, 312f);
		ft.transform.position = floatngTextPos.position;
		ft.SetText (info);

	}
	public void CreatePoppUpWarningText(string info){
		FloatingText ft = Instantiate(WarningPopUpText);

		ft.transform.SetParent (this.transform, false);
		//ft.transform.position = new Vector2 (538f, 312f);
		ft.transform.position = floatngTextPos.position;
		ft.SetText (info);

	}
	IEnumerator StartCountDown(){
		
		questionAnswered = false;
		countDown = 10f;
		countDownPanel.gameObject.SetActive (true);
		countDownText.text = countDown.ToString ();
		while (countDown > 0) {
			yield return new WaitForSeconds (1f);
			countDown -= 1f;
			countDownText.text = countDown.ToString ();
		     if (questionAnswered){
					break;
			 }
			if (SceneManager.GetActiveScene ().name == Custom_NetworkLobbyManager._LMSingleton.offlineScene) {
				break;
			}
		}

		if (!questionAnswered) {
			Custom_NetworkLobbyManager._LMSingleton.YourPlayer.gameObject.GetComponent<TwoPlayerBattle_PlayerAttack> ().Attack (true, true);
		   ShowQuizPanel (false);
		}
		countDownPanel.gameObject.SetActive (false);
	}
	public void DelayGameOverFunction(){
		StartCoroutine (GameOverFunction ());
	}
	IEnumerator GameOverFunction(){
		yield return new WaitForSeconds (1.2f);
		Time.timeScale = 0;

	}
}
