using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
public class TwoPlayerBattle_PlayerScript : NetworkBehaviour {
	
    bool isGameOver = false;
	static List<TwoPlayerBattle_PlayerScript> players = new List<TwoPlayerBattle_PlayerScript> ();
	private TwoPlayerBattle_PlayerStats playersHealth;
	private TwoPlayerBattle_PlayerStats myStats;
	private TwoPlayerBattle_PlayerAttack myAttackScript;
	[SyncVar(hook="OnTurnChange")]public bool YourTurn; 
	public override void PreStartClient ()
	{
		
		StartCoroutine (InitializePlayers());
		playersHealth = GetComponent<TwoPlayerBattle_PlayerStats> ();
		UpdateHealth ();
		//InitializeTurn ();

	}
	[ClientRpc]
	public void RpcUpdateTwoPlayerTimer (string timerText){
		Custom_NetworkLobbyManager._LMSingleton.TwoPlayerTimerText.text = timerText;
	}
	[Server]
	void StartServerTimer(){
		StartCoroutine (StartTimer ());
	}
	public IEnumerator StartTimer(){

		float minute = 0f;
		float second = 0f;
		float Timer = 0f;
		Custom_NetworkLobbyManager._LMSingleton.TwoPlayerTimerText.text = Timer.ToString ();
	
		while (Timer < 180f) {


			yield return new WaitForSeconds (1f);
	
			Timer += 1f;
			second += 1f;
			if (second >= 60) {
				minute++;
				second = 0f;
			}
			string SecondString = (second < 10) ? ("0" + second.ToString ()) : second.ToString ();
			for (int i = 0; i < players.Count; i++) {
				players [i].RpcUpdateTwoPlayerTimer (minute.ToString () + ":" + SecondString);
			}

			if (SceneManager.GetActiveScene ().name == Custom_NetworkLobbyManager._LMSingleton.offlineScene) {
				break;
			}

		}
		if (isLocalPlayer) {
			GameObject target = Custom_NetworkLobbyManager._LMSingleton.TargetPlayer;
			TwoPlayerBattle_PlayerStats myplayerStat = Custom_NetworkLobbyManager._LMSingleton.YourPlayer.GetComponent<TwoPlayerBattle_PlayerStats> ();
			TwoPlayerBattle_PlayerStats myenemyStat = target.GetComponent<TwoPlayerBattle_PlayerStats> ();

			float playercurrentHlthFloat = myplayerStat.CurrentHealth;
			float playerMaxHlthFloat = myplayerStat.MaxHealth;

			float enemycurrentHlthFloat = myenemyStat.CurrentHealth;
			float enemyrMaxHlthFloat = myenemyStat.MaxHealth;
			if (((playercurrentHlthFloat / playerMaxHlthFloat) * 100f) > ((enemycurrentHlthFloat / enemyrMaxHlthFloat) * 100f)) {
				Won (true);
				//Debug.Log ("Your Player:" + playercurrentHlthFloat + "/" + playerMaxHlthFloat);
				//Debug.Log ("EnemyPlayer:" + enemycurrentHlthFloat + "/" + enemyrMaxHlthFloat);
			//	Debug.Log ("I won" + (playercurrentHlthFloat / playerMaxHlthFloat) * 100f + " vs " + ((enemycurrentHlthFloat / enemyrMaxHlthFloat) * 100f));
			} else {
				target.GetComponent<TwoPlayerBattle_PlayerScript> ().Won (true);
				//Debug.Log ("Your Player:" + playercurrentHlthFloat + "/" + playerMaxHlthFloat);
				//Debug.Log ("EnemyPlayer:" + enemycurrentHlthFloat + "/" + enemyrMaxHlthFloat);
			//	Debug.Log ("Enemy Won" + (playercurrentHlthFloat / playerMaxHlthFloat) * 100f + " vs " + ((enemycurrentHlthFloat / enemyrMaxHlthFloat) * 100f));

				//Won();
			}
			//gamerOver

		}
	}

	[ServerCallback]
	public void ClearPlayerList(){
		players.Clear ();
	}
	[ServerCallback]
	void OnEnable(){
		if (!players.Contains (this))
			players.Add (this);
	}
	[ServerCallback]
	void OnDisable(){
		if (players.Contains (this))
			players.Remove (this);
	}
	[ServerCallback]
	void OnDestroy(){
		//Debug.Log ("aaaa");
		if (players.Contains (this))
			players.Remove (this);
	}
	private void InitializeTurn(){
		if (isServer) {
			CmdSetTurn (true, "");
		} else {
			CmdSetTurn (false, "");
		}

	}

	private void OnTurnChange(bool isTurn){
		
		YourTurn = isTurn;
		if (isTurn && isLocalPlayer && !isGameOver) {
			Custom_NetworkLobbyManager._LMSingleton.TwoPlayerBattle_PlayerCameraScript.SetCameraBattleMode (false);
			TwoPlayer_GameManager.Instance.ShowQuizPanel (true);
		}
	}
	IEnumerator InitializePlayers(){

		//Custom_NetworkLobbyManager._LMSingleton.TargetPlayer = GameObject.Find ("FreeGolemOnline");

		for (;;) {
			yield return new WaitForSeconds (Time.deltaTime);

			if (isLocalPlayer) {
					Custom_NetworkLobbyManager._LMSingleton.YourPlayer = this.gameObject;
				
				} else  {
					Custom_NetworkLobbyManager._LMSingleton.TargetPlayer = this.gameObject;
				}

		}
	}
	public override void OnStartLocalPlayer ()
	{
		myStats = GetComponent<TwoPlayerBattle_PlayerStats> ();
		myAttackScript = GetComponent<TwoPlayerBattle_PlayerAttack> ();
		Transform battleCameraPos = this.transform;//DefaultValue
		Transform normalCameraPos = this.transform;
		if (isServer) {
			normalCameraPos = Custom_NetworkLobbyManager._LMSingleton.HostNormalCameraPos;
			battleCameraPos = Custom_NetworkLobbyManager._LMSingleton.HostBattleCameraPos;
			//Custom_NetworkLobbyManager._LMSingleton.ServerPlayer = this.gameObject;
		} else if(isLocalPlayer) {
			normalCameraPos = Custom_NetworkLobbyManager._LMSingleton.ClientNormalCameraPos;
			battleCameraPos = Custom_NetworkLobbyManager._LMSingleton.ClientBattleCameraPos;
			//Custom_NetworkLobbyManager._LMSingleton.ClientPlayer = this.gameObject;
		}
		Custom_NetworkLobbyManager._LMSingleton.TwoPlayerBattle_PlayerCameraScript.SetCameraPositions (normalCameraPos, battleCameraPos);
		if (isServer) {
			StartCoroutine (StartPlay());
		}

	}
	public void CreatePoppUpText(string categoryText,int categoryIndex){
		for (int i = 0; i < players.Count; i++) {
			players [i].RpcCreatePoppUpText (categoryText,categoryIndex);
		}

		StartCoroutine (PopUpTextDuration ());
	}	
	[ClientRpc]
	public void RpcCreatePoppUpText(string categoryText,int categoryIndex){
		FloatingText ft = Instantiate (TwoPlayer_GameManager.Instance.FloatingText);

		ft.transform.SetParent (TwoPlayer_GameManager.Instance.transform, false);
		//ft.transform.position = new Vector2 (538f, 312f);
		ft.transform.position = TwoPlayer_GameManager.Instance.FloatngTextPos.position;
		ft.SetText (categoryText);
		Custom_NetworkLobbyManager._LMSingleton.CurrentCategoryIndex = categoryIndex;
	}
	IEnumerator PopUpTextDuration(){
		yield return new WaitForSeconds (3.5f);
		InitializeTurn ();
		StartServerTimer ();

	}
	public IEnumerator StartPlay(){

		Custom_NetworkLobbyManager._LMSingleton.WaitingPanel.gameObject.SetActive (true);
		while (players.Count <= 1) {
			yield return new WaitForSeconds (1f);
		}
		Custom_NetworkLobbyManager._LMSingleton.WaitingPanel.gameObject.SetActive (false);
		if (players.Count > 1) {
			CreatePoppUpText (Custom_NetworkLobbyManager._LMSingleton.category, Custom_NetworkLobbyManager._LMSingleton.CurrentCategoryIndex);
		}
	}

	void Update(){

		if (!isLocalPlayer)
			return;


		if (Custom_NetworkLobbyManager._LMSingleton.CoinPicked != "") {
			
			CmdTellServerCoinPicked (Custom_NetworkLobbyManager._LMSingleton.CoinPicked);
			Custom_NetworkLobbyManager._LMSingleton.CoinPicked = "";
		}


	}
	[Command]
	void CmdTellServerCoinPicked (string coinpicked){
		if (GameObject.Find (coinpicked)) {
			GameObject.Find (coinpicked).GetComponent<Animator> ().SetTrigger ("PickUp");
			GameObject.Find (coinpicked).GetComponent<OnlinePisoScript> ().IsUse = true;
		}
	}
	[Command]
	public void CmdSetTurn(bool isTurn,string name){
		if (name == "") {
			YourTurn = isTurn;
		} else {
			GameObject go = GameObject.Find (name);
			go.GetComponent<TwoPlayerBattle_PlayerScript> ().YourTurn = isTurn;
		}
	}

	[Server]
	public void UpdateHealth(){
		for (int i = 0; i < players.Count; i++) {
			players [i].RpcUpdateHealth ();
		}

	}
	[ClientRpc]
	public void RpcUpdateHealth(){
		playersHealth.initializeHealthBar ();
	}
	public void EnemyWon(){
		if (isLocalPlayer) {
			CmdWon (netId);
		}
	}
	[Command]
	public void CmdWon(NetworkInstanceId networkId){
		for (int i = 0; i < players.Count; i++) {
			players [i].RpcSurrenderGameOver (networkId);
		}

		players.Clear ();
	}
	[Server]
	public void Won(bool isTimesUp){
		for (int i = 0; i < players.Count; i++) {
			players [i].RpcGameOver (netId, isTimesUp);
		}

		players.Clear ();
	}

	[Server]
	public void Draw(){
		for (int i = 0; i < players.Count; i++) {
			players [i].RpcDraw ();
		}
		players.Clear ();
	}

	[Command]
	public void Cmd_CorrectAnswer(bool isCorrect,bool TimesUp,string correctAnswer){
		for (int i = 0; i < players.Count; i++) {
			players [i].RpcCorrectAnswer (netId, isCorrect, TimesUp,correctAnswer);
		}

	}
	[ClientRpc]
	public void RpcCorrectAnswer(NetworkInstanceId networkId,bool isCorrect,bool TimesUp,string correctAnswer){
		int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);
		if (languageIndex == 0) {
			if (isLocalPlayer) {
			
				if (netId == networkId) {
					if (isCorrect) {
						TwoPlayer_GameManager.Instance.CreatePoppUpInfoText ("You're Correct!! \n You are Turn To Attack");
					} else {
						if (!TimesUp) {
							TwoPlayer_QuizManager qm = Custom_NetworkLobbyManager._LMSingleton.TwoPlayerQuizManager;
							TwoPlayer_GameManager.Instance.CreatePoppUpWarningText ("You're Wrong!! \n The Correct Answer is '" + correctAnswer + "'");
						} else {
							TwoPlayer_QuizManager qm = Custom_NetworkLobbyManager._LMSingleton.TwoPlayerQuizManager;
							TwoPlayer_GameManager.Instance.CreatePoppUpWarningText ("TimesUp!! \n The Correct Answer is '" + correctAnswer + "'");

						}
				
					}
				} else {
					if (isCorrect) {
						TwoPlayer_GameManager.Instance.CreatePoppUpWarningText ("You're Opponent Answer Correct!! \n Opponent Turn To Attack");
					} else {
						if (!TimesUp) {
							TwoPlayer_QuizManager qm = Custom_NetworkLobbyManager._LMSingleton.TwoPlayerQuizManager;
							TwoPlayer_GameManager.Instance.CreatePoppUpInfoText ("You're Opponent Answer Wrong!! \n The Correct Answer is '" + correctAnswer + "'");
						} else {
							TwoPlayer_QuizManager qm = Custom_NetworkLobbyManager._LMSingleton.TwoPlayerQuizManager;
							TwoPlayer_GameManager.Instance.CreatePoppUpInfoText ("TimesUp!! \n The Correct Answer is '" + correctAnswer + "'");

						}
					}
				}
			}
		} else {
		
			//kapag tagalog
			if (isLocalPlayer) {

				if (netId == networkId) {
					if (isCorrect) {
						TwoPlayer_GameManager.Instance.CreatePoppUpInfoText ("Tama ang Sagot mo!! \n Ang Manlalaro ang Susunod na Aatake");
					} else {
						if (!TimesUp) {
							TwoPlayer_QuizManager qm = Custom_NetworkLobbyManager._LMSingleton.TwoPlayerQuizManager;
							TwoPlayer_GameManager.Instance.CreatePoppUpWarningText ("Mali ang Sagot mo!! \n Ang Tamang Sagot ay '" + correctAnswer + "'");
						} else {
							TwoPlayer_QuizManager qm = Custom_NetworkLobbyManager._LMSingleton.TwoPlayerQuizManager;
							TwoPlayer_GameManager.Instance.CreatePoppUpWarningText ("Nalalabing Oras ay Tapos Na!! \n Ang Tamang Sagot ay '" + correctAnswer + "'");

						}

					}
				} else {
					if (isCorrect) {
						TwoPlayer_GameManager.Instance.CreatePoppUpWarningText ("Tama ang Sagot ng iyong Kalaban!! \n Ang iyong Kalaban ang Susunod na Aatake");
					} else {
						if (!TimesUp) {
							TwoPlayer_QuizManager qm = Custom_NetworkLobbyManager._LMSingleton.TwoPlayerQuizManager;
							TwoPlayer_GameManager.Instance.CreatePoppUpInfoText ("Mali ang Sagot ng iyong Kalaban!! \n Ang Tamang Sagot ay '" + correctAnswer + "'");
						} else {
							TwoPlayer_QuizManager qm = Custom_NetworkLobbyManager._LMSingleton.TwoPlayerQuizManager;
							TwoPlayer_GameManager.Instance.CreatePoppUpInfoText ("Nalalabing Oras ay Tapos Na!! \n Ang Tamang Sagot ay '" + correctAnswer + "'");

						}
					}
				}
			}

		}
	}
	[ClientRpc]
	public void RpcGameOver(NetworkInstanceId networkId,bool isTimesUp){
		int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);
		if (isLocalPlayer) {
			
			if (netId == networkId) {
				//Custom_NetworkLobbyManager._LMSingleton.TwoPlayerGameOverPanel.gameObject.GetComponentInChildren<Text> ().text = "You Won!!";
				TwoPlayer_GameManager.Instance.GameOverPanel.gameObject.SetActive (true);
				TwoPlayer_GameManager.Instance.GameOverPanel.gameObject.GetComponent<Animator> ().SetTrigger ("ShowPanel");
				int wonRecord = PlayerPrefs.GetInt ("WonRecord", 0);
				PlayerPrefs.SetInt ("WonRecord", wonRecord + 1);
				if (languageIndex == 0) {
					TwoPlayer_GameManager.Instance.GameOverPanelText.text = "You Won";
					if (isTimesUp) {
						TwoPlayer_GameManager.Instance.GameOverText.text = "TIMESUP!!";
					} else {
						TwoPlayer_GameManager.Instance.GameOverText.text = "GAMEOVER!!";
					}
				} else {
					TwoPlayer_GameManager.Instance.GameOverPanelText.text = "Panalo Ka";
					if (isTimesUp) {
						TwoPlayer_GameManager.Instance.GameOverText.text = "UBOS NA ANG ORAS!!";
					} else {
						TwoPlayer_GameManager.Instance.GameOverText.text = "TAPOS NA ANG LARO!!";
					}
				}

			} else {
				int lossRecord = PlayerPrefs.GetInt ("LossRecord", 0);
				PlayerPrefs.SetInt ("LossRecord", lossRecord + 1);
				//Custom_NetworkLobbyManager._LMSingleton.TwoPlayerGameOverPanel.gameObject.GetComponentInChildren<Text> ().text = "You Lost!!";
				TwoPlayer_GameManager.Instance.GameOverPanel.gameObject.SetActive (true);
				TwoPlayer_GameManager.Instance.GameOverPanel.gameObject.GetComponent<Animator> ().SetTrigger ("ShowPanel");
				if (languageIndex == 0) {
					TwoPlayer_GameManager.Instance.GameOverPanelText.text = "You Lost";
					if (isTimesUp) {
						TwoPlayer_GameManager.Instance.GameOverText.text = "TIMESUP!!";
					} else {
						TwoPlayer_GameManager.Instance.GameOverText.text = "GAMEOVER!!";
					}
				} else {
					TwoPlayer_GameManager.Instance.GameOverPanelText.text = "Talo Ka";
					if (isTimesUp) {
						TwoPlayer_GameManager.Instance.GameOverText.text = "UBOS NA ANG ORAS!!";
					} else {
						TwoPlayer_GameManager.Instance.GameOverText.text = "TAPOS NA ANG LARO!!";
					}
				}
			}
			TwoPlayer_GameManager.Instance.DelayGameOverFunction ();
			isGameOver = true;	
		}
	}
	[ClientRpc]
	public void RpcSurrenderGameOver(NetworkInstanceId networkId){
		int languageIndex = PlayerPrefs.GetInt ("CurrentLanguage", 0);
		if (isLocalPlayer) {

			if (netId != networkId) {
				//Custom_NetworkLobbyManager._LMSingleton.TwoPlayerGameOverPanel.gameObject.GetComponentInChildren<Text> ().text = "You Won!!";
				TwoPlayer_GameManager.Instance.GameOverPanel.gameObject.SetActive(true);
				if (languageIndex == 0) {
					TwoPlayer_GameManager.Instance.GameOverPanelText.text = "You Won";
				} else {
					TwoPlayer_GameManager.Instance.GameOverPanelText.text = "Panalo Ka";
				}
				TwoPlayer_GameManager.Instance.GameOverPanel.gameObject.GetComponent<Animator> ().SetTrigger ("ShowPanel");
				int wonRecord = PlayerPrefs.GetInt ("WonRecord", 0);
				PlayerPrefs.SetInt ("WonRecord", wonRecord + 1);

			} else {
				//Custom_NetworkLobbyManager._LMSingleton.TwoPlayerGameOverPanel.gameObject.GetComponentInChildren<Text> ().text = "You Lost!!";
				int lossRecord = PlayerPrefs.GetInt ("LossRecord", 0);
				PlayerPrefs.SetInt ("LossRecord", lossRecord + 1);
				TwoPlayer_GameManager.Instance.GameOverPanel.gameObject.SetActive(true);
				if (languageIndex == 0) {
					TwoPlayer_GameManager.Instance.GameOverPanelText.text = "You Lost";
				} else {
					TwoPlayer_GameManager.Instance.GameOverPanelText.text = "Talo Ka";
				}
				TwoPlayer_GameManager.Instance.GameOverPanel.gameObject.GetComponent<Animator> ().SetTrigger ("ShowPanel");
			}
			TwoPlayer_GameManager.Instance.DelayGameOverFunction ();
			isGameOver = true;	
		}
	}
	[ClientRpc]
	public void RpcDraw(){

		if (isLocalPlayer) {
			TwoPlayer_GameManager.Instance.GameOverPanel.gameObject.SetActive(true);
			TwoPlayer_GameManager.Instance.GameOverPanelText.text = "Draw";
			TwoPlayer_GameManager.Instance.GameOverPanel.gameObject.GetComponent<Animator> ().SetTrigger ("ShowPanel");
			isGameOver = true;
		}
			
	}
}
