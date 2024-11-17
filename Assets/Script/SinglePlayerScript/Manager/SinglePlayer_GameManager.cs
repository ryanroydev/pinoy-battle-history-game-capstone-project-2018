using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SinglePlayer_GameManager : MonoBehaviour {
	[SerializeField]private AudioSource AttackSound,OpeningSound;

	[SerializeField]private string MonsterName;
	public PotionPanelScript PotionPanel;
	public bool GameStart;
	public GameObject[] HumanEnemies;
	public RectTransform BloodPanel;
	[SerializeField]
	private FloatingText floatingText;
	[SerializeField]
	private FloatingText floatingInfoText;
	[SerializeField]
	private FloatingText floatingWarningText;
	private float countDown = 10;
	private bool questionAnswered;
	[SerializeField]
	private Transform floatngTextPos;
	private bool isGameOver = false;
	private GameObject player;
	[SerializeField]
	private  Text InfoText;

	[SerializeField]
	private Text countDownText;
	[SerializeField]
	private  RectTransform exitPanel,optionPanel,confirmationExitPanel,rewardPanel,statPanel,settingPanel,quizPanel,countdownPanel;
	[SerializeField]
	private SinglePlayer_RewardSystem myReward;
	[SerializeField]
	private GameObject enemyAttackPlayerPos;
	[SerializeField]
	private List<GameObject> playerAttackEnemyPos = new List<GameObject> ();
	[SerializeField]
	private List<GameObject> enemies = new List<GameObject> ();
	public bool isTagalog = false;
	public SinglePlayerCameraMovement SinglePlayerCameraMovementScript;
	public  Transform PlayerStartPos;
	public Transform EnemyStartPos;


	public LoadSceneScript myLoadingScene;

	private static SinglePlayer_GameManager instance;
	public static SinglePlayer_GameManager Instance{get{ if (instance == null) {instance = GameObject.FindObjectOfType<SinglePlayer_GameManager> ();}return instance;}}
	public List<GameObject> PlayerAttackEnemyPos{get{return playerAttackEnemyPos; }set{ this.playerAttackEnemyPos = value;}}
	public GameObject EnemyAttackPlayerPos{get{return enemyAttackPlayerPos; }set{ this.enemyAttackPlayerPos = value;}}
	public GameObject Player{get{ return player;}set{ this.player = value;}}
	public List<GameObject> Enemies{get{ return enemies;}set{ this.enemies = value;}} 
	public bool IsGameOver{get{ return isGameOver;}set{ this.isGameOver = value;}}
	public RectTransform QuizPanel{get{ return quizPanel;}set{ this.quizPanel = value;}}
	public SinglePlayer_RewardSystem MyReward{get{ return myReward;}set{ this.myReward = value;}}
	public bool QuestionAnswered{get{ return questionAnswered;}set{ this.questionAnswered = value;}}
	// Use this for initialization



	void Start () {

		PlayerPrefs.SetString (MonsterName, MonsterName);

		//if (player != null && player.GetComponent<SinglePlayer_PlayerScript> ()) {
			//player.GetComponent<SinglePlayer_PlayerScript> ().DieEvent += GameOver;
		//}
		if (enemies.Count != 0) {
			foreach (GameObject enemy in enemies) {
				if (enemy.GetComponent<SinglePlayer_EnemyScript> ()) {
					enemy.GetComponent<SinglePlayer_EnemyScript> ().EnemyDieEvent += checkEnemyDead;

				}
			}
		}
		//ShowQuizPanel (true);

		SetLanguage ();
	}
	void OnEnable(){
		Time.timeScale = 1;
		rewardPanel.gameObject.SetActive (false);
	}
	public void AttackSoundPlay(){
		if (AttackSound != null) {
			AttackSound.Play ();
		}
	}
	public void CreatePoppUpInfoText(string info){
		FloatingText ft = Instantiate (floatingInfoText);

		ft.transform.SetParent (this.transform, false);
		//ft.transform.position = new Vector2 (538f, 312f);
		ft.transform.position = floatngTextPos.position;
		ft.SetText (info);
	
	}
	public void CreatePoppUpWarningText(string info){
		FloatingText ft = Instantiate (floatingWarningText);

		ft.transform.SetParent (this.transform, false);
		//ft.transform.position = new Vector2 (538f, 312f);
		ft.transform.position = floatngTextPos.position;
		ft.SetText (info);

	}
	public void CreatePoppUpText(string categoryText){
		if (OpeningSound != null) {
			OpeningSound.Play ();
		}
		FloatingText ft = Instantiate (floatingText);

		ft.transform.SetParent (this.transform, false);
		//ft.transform.position = new Vector2 (538f, 312f);
		ft.transform.position = floatngTextPos.position;
		ft.SetText (categoryText);
		StartCoroutine (ShowIntro ());
	}
	public IEnumerator ShowIntro(){
		Animator myAnimator = floatingText.gameObject.GetComponentInChildren<Animator> ();
		AnimatorClipInfo[] clip = myAnimator.GetCurrentAnimatorClipInfo (0);

		yield return new WaitForSeconds (3.5f);
		SinglePlayerCameraMovementScript.SetIntroCamera (false);
		GameStart = true;
		statPanel.gameObject.SetActive (true);
		exitPanel.gameObject.SetActive (true);
		ShowQuizPanel (true);
	}
	public void DisplayTextInfo(string textInfo,bool isEnable){
		if (isEnable) {
			
			InfoText.gameObject.SetActive (isEnable);
			InfoText.text = textInfo;
		} else {
			InfoText.text = "";
			InfoText.gameObject.SetActive (isEnable);
		}

	}
	public void EnemiesAttack(){
		StartCoroutine (StartEnemiesAttack ());
	}

	IEnumerator StartEnemiesAttack(){
		
		if (enemies.Count != 0) {
			
			foreach (GameObject enemy in enemies) {
				
				if (enemy.GetComponent<SinglePlayer_EnemyAttack> () && !isGameOver) {
					
					SinglePlayer_EnemyAttack enemyAttackScript = enemy.GetComponent<SinglePlayer_EnemyAttack> ();
					enemyAttackScript.Attack ();
					while (enemyAttackScript.IsEnemyAttack ()) {

						yield return new WaitForSeconds (Time.deltaTime);
					}
					yield return new WaitForSeconds (1f);
				}
			}
		}

		if (!isGameOver) {
			ShowQuizPanel (true);

		}
	
	}
	private IEnumerator StartCountDown(){

		QuizPanelScript myQuiz = quizPanel.gameObject.GetComponent<QuizPanelScript> ();
		switch ((int)myQuiz.currentQuestion.questonLegth) {
		case 0:
			countDown = 10f;
			break;
		case 1:
			countDown = 15f;
			break;
		case 2:
			countDown = 20f;
			break;
		}
			questionAnswered = false;

			countdownPanel.gameObject.SetActive (true);
			countDownText.text = countDown.ToString ();
			while (countDown > 0) {
				yield return new WaitForSeconds (1f);
				countDown -= 1f;
				countDownText.text = countDown.ToString ();
				if (questionAnswered)
					break;
			}


	
		if (!questionAnswered) {
			ShowQuizPanel (false);
			countDownText.text = "0";
			countdownPanel.gameObject.SetActive (false);
			//DisplayTextInfo ("Enemy Turn to Attack", true);

			int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);
			if (languageIndex == 0) {
				CreatePoppUpWarningText ("TimesUp!! \n  Enemy Turn to Attack \n The Correct Answer is '" + myQuiz.currentQuestion.Choices [myQuiz.currentQuestion.AnswerIndex] + "'");
			} else {
				CreatePoppUpWarningText ("Nalalabing Oras ay Tapos Na!! \n  Ang Kalaban ang Susunod na Aattake \n Ang Tamang Sagot ay '" + myQuiz.currentQuestion.Choices [myQuiz.currentQuestion.AnswerIndex] + "'");
			}
			yield return new WaitForSeconds (0.5f);
			//DisplayTextInfo ("", false);
			//countdownPanel.gameObject.SetActive (false);
			EnemiesAttack ();
		}
			countdownPanel.gameObject.SetActive (false);

	}

	private void SetLanguage(){
		int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);

		switch (languageIndex) {
		case 0:
			isTagalog = false;
			break;
		case 1:
			isTagalog = true;

			break;
		}

	}
	public void ShowQuizPanel(bool isTrue){
		
			if (isTrue) {
				quizPanel.gameObject.SetActive (isTrue);
				quizPanel.gameObject.GetComponent<Animator> ().SetBool ("Fadein", isTrue);
				StartCoroutine (StartCountDown ());
			} else {
				quizPanel.gameObject.GetComponent<Animator> ().SetBool ("Fadein", isTrue);
				quizPanel.gameObject.SetActive (isTrue);

			}

	
	}
	private void checkEnemyDead(){
		

		for (int i = 0; i < enemies.Count; i++) {

			if (enemies[i].GetComponent<SinglePlayer_EnemyScript> ().IsEnemyDead ()) {

				//addexpi coinscore
				myReward.AddExpi(enemies[i].GetComponent<SinglePlayer_EnemyScript> ().RewardExpi);
				myReward.AddCoin (enemies [i].GetComponent<SinglePlayer_EnemyScript> ().RewardCoin);
				myReward.AddScore (enemies [i].GetComponent<SinglePlayer_EnemyScript> ().RewardScore);
				enemies.RemoveAt (i);

			}
		}
		
		if (enemies.Count == 0) {
			rewardPanel.GetComponent<SinglePlayerRewardPanelScript> ().StageClear = true;
			GameOver ();

		}
	
	}
	public void GameOver(){
		StartCoroutine (DelayGameOver ());
	}
	IEnumerator DelayGameOver(){
		isGameOver = true;
		yield return new WaitForSeconds (4f);
		YesSurrenderGame ();
	}
	public void ClearPlayer(){
		this.player = null;
	}
	public void RegisterPlayer(GameObject yourPlayer){
		this.player = yourPlayer;
	}
	public void ContinueGame(){
		statPanel.gameObject.SetActive (true);
		optionPanel.gameObject.SetActive (false);
		exitPanel.gameObject.SetActive (true);
	}
	public void ShowSettingPanel(){
		optionPanel.gameObject.SetActive (false);
		settingPanel.gameObject.SetActive (true);

	}
	public void BackSettingPanel(){
		optionPanel.gameObject.SetActive (true);
		settingPanel.gameObject.SetActive (false);
	}
	public void OpenOptionPanel(){
		statPanel.gameObject.SetActive (false);
		optionPanel.gameObject.SetActive (true);
		exitPanel.gameObject.SetActive (false);
	}
	public void NoSurrenderGame(){
		confirmationExitPanel.gameObject.SetActive (false);
		optionPanel.gameObject.SetActive (true);
	}
	public void ConfirmationSurrender(){
		confirmationExitPanel.gameObject.SetActive (true);
		optionPanel.gameObject.SetActive (false);
	
	} 
	public void YesSurrenderGame(){
		
		ShowQuizPanel (false);
		statPanel.gameObject.SetActive (false);
		rewardPanel.gameObject.SetActive (true);
		confirmationExitPanel.gameObject.SetActive (false);
		exitPanel.gameObject.SetActive (false);
		if (rewardPanel.gameObject.GetComponent<Animator> ()) {
			rewardPanel.gameObject.GetComponent<Animator> ().SetTrigger ("ShowPanel");
		} else {
			//Debug.Log ("Pls INput Animator  in " + rewardPanel.name);
		}
		StartCoroutine (GameOverFunction ());
		PotionPanel.gameObject.SetActive (false);
	}

	IEnumerator GameOverFunction(){
		yield return new WaitForSeconds (1f);

		Time.timeScale = 0;

	}
}
